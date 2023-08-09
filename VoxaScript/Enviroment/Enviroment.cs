using VoxaScript.Enviroment.RuntimeValue;
using VoxaScript.Token;
using Boolean = VoxaScript.Enviroment.RuntimeValue.Boolean;
using String = VoxaScript.Enviroment.RuntimeValue.String;
using Void = VoxaScript.Enviroment.RuntimeValue.Void;

namespace VoxaScript.Enviroment;

public class Enviroment
{
    public Scope GlobalScope = Scope.CreateGlobalScope();
    public Dictionary<string, Func<IRuntimeValue[], IRuntimeValue>> LanguageFunctions = new();
    public Dictionary<string, object> LanguageVariables = new();

    public Enviroment()
    {
        LoadLanguageFunction("print", Print);

        LoadLanguageFunction("input", Input);
    }

    private IRuntimeValue Print(IRuntimeValue[] values)
    {
        if (values.Length != 1) throw new Exception("Print() takes exactly 1 argument.");

        Console.WriteLine(values[0].ToString());

        return new Void();
    }

    private IRuntimeValue Input(IRuntimeValue[] values)
    {
        if (values.Length != 1) throw new Exception("input() takes exactly 1 argument.");

        Console.Write(values[0].ToString());
        var input = Console.ReadLine();

        return new String { Value = input };
    }

    public void LoadLanguageVariable(string name, object value)
    {
        LanguageVariables[name] = value;
    }

    public void LoadLanguageFunction(string name, Func<IRuntimeValue[], IRuntimeValue> function)
    {
        LanguageFunctions[name] = function;
    }

    public void Load(string source)
    {
        var tokens = Lexer.Lex(source);
        var parser = new Parser.Parser(tokens);
        Parser.Parser.IAst? ast = parser.Parse();

        if (ast != null)
            Evaluate(GlobalScope, ast);
        else
            throw new Exception("Failed to parse.");
    }

    public IRuntimeValue Evaluate(Scope scope, Parser.Parser.IAst ast)
    {
        if (ast is Parser.Parser.Block) return EvaluateBlock(scope, (Parser.Parser.Block)ast);

        if (ast is Parser.Parser.BinaryOp) return EvaluateBinaryOp(scope, (Parser.Parser.BinaryOp)ast);

        if (ast is Parser.Parser.VariableDeclaration)
            return EvaluateVariableDeclaration(scope, (Parser.Parser.VariableDeclaration)ast);

        if (ast is Parser.Parser.Variable) return EvaluateVariable(scope, (Parser.Parser.Variable)ast);

        if (ast is Parser.Parser.VariableAssignment)
            return EvaluateVariableAssignment(scope, (Parser.Parser.VariableAssignment)ast);

        if (ast is Parser.Parser.If) return EvaluateIf(scope, (Parser.Parser.If)ast);

        if (ast is Parser.Parser.FunctionDeclaration)
            return EvaluateFunctionDeclaration(scope, (Parser.Parser.FunctionDeclaration)ast);

        if (ast is Parser.Parser.FunctionCall) return EvaluateFunctionCall(scope, (Parser.Parser.FunctionCall)ast);

        if (ast is Parser.Parser.Return) return EvaluateReturn(scope, (Parser.Parser.Return)ast);

        if (ast is Parser.Parser.String) return new String { Value = ((Parser.Parser.String)ast).Token.Value };

        if (ast is Parser.Parser.Int) return new Int { Value = ((Parser.Parser.Int)ast).Token.AsInt() };

        if (ast is Parser.Parser.Float) return new Float { Value = ((Parser.Parser.Float)ast).Token.AsFloat() };

        if (ast is Parser.Parser.Boolean) return new Boolean { Value = ((Parser.Parser.Boolean)ast).Token.AsBoolean() };

        return new Void();
    }

    private IRuntimeValue EvaluateFunctionDeclaration(Scope scope, Parser.Parser.FunctionDeclaration ast)
    {
        scope.DefineFunction(ast.FunctionName.Value, ast.Arguments, ast.Body);
        return new Void();
    }

    private IRuntimeValue EvaluateVariableAssignment(Scope scope, Parser.Parser.VariableAssignment ast)
    {
        var variableEvaluation = Evaluate(scope, ast.Value);
        scope.AssignVariable(ast.VariableName.Value, variableEvaluation);
        return variableEvaluation;
    }

    private IRuntimeValue EvaluateVariable(Scope scope, Parser.Parser.Variable ast)
    {
        var variable = scope.GetVariable(ast.Token.Value);

        if (variable != null) return variable;

        throw new Exception("Variable " + ast.Token.Value + " not found");
    }

    private IRuntimeValue EvaluateVariableDeclaration(Scope scope, Parser.Parser.VariableDeclaration ast)
    {
        if (ast.Value != null)
        {
            var variableEvaluation = Evaluate(scope, ast.Value);
            scope.DefineVariable(ast.VariableName.Value, variableEvaluation);
            return variableEvaluation;
        }

        scope.DefineVariable(ast.VariableName.Value, new Void());
        return new Void();
    }

    private IRuntimeValue EvaluateReturn(Scope scope, Parser.Parser.Return ast)
    {
        if (ast.Expr != null)
        {
            var returnValue = Evaluate(scope, ast.Expr);
            return new Return { ReturnValue = returnValue };
        }

        return new Return { ReturnValue = new Void() };
    }

    private IRuntimeValue EvaluateIf(Scope scope, Parser.Parser.If ast)
    {
        if (ast.Condition == null) return new Void();
        var condition = Evaluate(scope, ast.Condition);

        if (condition is Return returnValue && returnValue.ReturnValue is Boolean)
            condition = returnValue.ReturnValue;

        if (condition is Boolean boolean)
        {
            if (boolean.Value && ast.Body != null) return Evaluate(scope, ast.Body);
        }
        else
        {
            throw new Exception("Condition must be a boolean");
        }

        return new Void();
    }

    private IRuntimeValue EvaluateBinaryOp(Scope scope, Parser.Parser.BinaryOp ast)
    {
        switch (ast.Op.Value)
        {
            case "+":
                if (ast.Left != null)
                    if (ast.Right != null)
                        return Evaluate(scope, ast.Left).Add(Evaluate(scope, ast.Right));
                break;
            case "-":
                if (ast.Left != null)
                    if (ast.Right != null)
                        return Evaluate(scope, ast.Left).Sub(Evaluate(scope, ast.Right));
                break;
            case "*":
                if (ast.Left != null)
                    if (ast.Right != null)
                        return Evaluate(scope, ast.Left).Mul(Evaluate(scope, ast.Right));
                break;
            case "/":
                if (ast.Left != null)
                    if (ast.Right != null)
                        return Evaluate(scope, ast.Left).Div(Evaluate(scope, ast.Right));
                break;
            case "==":
                if (ast.Left != null)
                    if (ast.Right != null)
                        return Evaluate(scope, ast.Left).Eq(Evaluate(scope, ast.Right));
                break;
            case "!=":
                if (ast.Left != null)
                    if (ast.Right != null)
                        return Evaluate(scope, ast.Left).Neq(Evaluate(scope, ast.Right));
                break;
            case "%":
                if (ast.Left != null)
                    if (ast.Right != null)
                        return Evaluate(scope, ast.Left).Mod(Evaluate(scope, ast.Right));
                break;
            case ">>":
                if (ast.Left != null)
                    if (ast.Right != null)
                        return Evaluate(scope, ast.Left).Shr(Evaluate(scope, ast.Right));
                break;
            case "<<":
                if (ast.Left != null)
                    if (ast.Right != null)
                        return Evaluate(scope, ast.Left).Shl(Evaluate(scope, ast.Right));
                break;
        }

        throw new Exception("Unknown binary operator " + ast.Op.Value);
    }

    private IRuntimeValue EvaluateFunctionCall(Scope scope, Parser.Parser.FunctionCall ast)
    {
        var function = scope.GetFunction(ast.FunctionName.Value);
        if (function != null)
        {
            var functionScope = new Scope(scope);
            var arguments = ast.Arguments;
            var functionArguments = function.Value.args;
            if (arguments.Length == functionArguments.Length)
                for (var i = 0; i < arguments.Length; i++)
                {
                    var argument = arguments[i];
                    var functionArgument = functionArguments[i];
                    if (argument != null)
                    {
                        var argumentEvaluation = Evaluate(scope, argument);
                        functionScope.DefineVariable(functionArgument.Value, argumentEvaluation);
                    }
                    else
                    {
                        throw new Exception("Argument " + functionArgument.Value + " is not optional");
                    }
                }
            else
                throw new Exception("Function " + ast.FunctionName.Value + " takes " + functionArguments.Length +
                                    " arguments, but " + arguments.Length + " were given");

            if (function.Value.body != null) return Evaluate(functionScope, function.Value.body);
        }
        else if (LanguageFunctions.TryGetValue(ast.FunctionName.Value, out var languageFunction))
        {
            return languageFunction(ast.Arguments.Select(arg =>
            {
                if (arg != null) return Evaluate(scope, arg);
                return new Void();
            }).ToArray());
        }
        else
        {
            throw new Exception($"Undefined function '{ast.FunctionName.Value}'.");
        }

        return new Void();
    }

    public IRuntimeValue EvaluateBlock(Scope scope, Parser.Parser.Block ast)
    {
        foreach (var statement in ast.Statements)
            if (statement != null)
            {
                var returned = Evaluate(scope, statement);

                if (returned is Return returnValue) return returnValue;
            }

        return new Void();
    }
}