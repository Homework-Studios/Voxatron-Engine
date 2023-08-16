using System.Data;
using VoxaScript.Error;

namespace VoxaScript.Parser;

public class Parser
{
    private Token.Token[] _tokens;

    private bool SkipSemicolon;

    public Parser(Token.Token[] tokens)
    {
        _tokens = tokens;
    }

    private Token.Token CurrentToken
    {
        get
        {
            if (_tokens.Length > 0) return _tokens[0];

            return new Token.Token(Token.Token.Type.Eof, "");
        }
    }

    public Token.Token Eat(string expected = "")
    {
        var token = CurrentToken;

        if (expected == "")
        {
            _tokens = _tokens[1..];
            return token;
        }

        if (_tokens[0].Value == expected)
            _tokens = _tokens[1..];
        else
        {
            // remove the faulty token
            _tokens = _tokens[1..];
            var errorToken = new Token.Token(Token.Token.Type.Error, "Unexpected token", ErrorMessages.Error.UnexpectedToken);
            errorToken.atChar = token.atChar;
            errorToken.atLine = token.atLine;
            return errorToken;
        }

        return token;
    }

    public Token.Token Peek(int offset = 1)
    {
        if (_tokens.Length > offset) return _tokens[offset];

        return new Token.Token(Token.Token.Type.Eof, "");
    }

    public IAst? Structures()
    {
        if (CurrentToken.Value == "var")
        {
            var varToken = Eat("var");
            var name = Eat();

            // If Empty Declaration
            if (CurrentToken.Value == ";")
                return new VariableDeclaration
                {
                    Name = "VariableDeclaration",
                    VariableName = name,
                    Value = null,
                    Line = varToken.atLine,
                    Char = varToken.atChar
                };

            var first = Eat("=");
            if (first.IsError) return first.AsParserError;
            
            var value = BooleanExpression();
            return new VariableDeclaration
            {
                Name = "VariableDeclaration",
                VariableName = name,
                Value = value,
                Line = varToken.atLine,
                Char = varToken.atChar,
                CanRunInGlobalScope = true
            };
        }

        if (CurrentToken.Value == "if")
        {
            var ifToken = Eat("if");
            var condition = BooleanExpression();
            
            var first = Eat("{");
            if (first.IsError) return first.AsParserError;

            var body = new List<IAst?>();

            while (CurrentToken.Value != "}")
            {
                body.Add(BooleanExpression());

                if (SkipSemicolon)
                {
                    SkipSemicolon = false;
                    continue;
                }
                
                var inner = Eat(";");
                if (inner.IsError) return inner.AsParserError;
            }

            var second = Eat("}");
            if (second.IsError) return second.AsParserError;
            
            SkipSemicolon = true;

            return new If
            {
                Name = "If",
                Condition = condition,
                Body = new Block
                {
                    Name = "Block",
                    Statements = body.ToArray()
                },
                Line = ifToken.atLine,
                Char = ifToken.atChar,
                CanRunInGlobalScope = true
            };
        }

        if (CurrentToken.Value == "function" || CurrentToken.Value == "fun")
        {
            var funToken = Eat();
            var name = Eat();
            var first = Eat("(");
            if (first.IsError) return first.AsParserError;

            var arguments = new List<Token.Token>();

            while (CurrentToken.Value != ")")
            {
                arguments.Add(Eat());

                if (CurrentToken.Value == ",") Eat(",");
            }

            var second = Eat(")");
            if (second.IsError) return second.AsParserError;

            var third = Eat("{");
            if (third.IsError) return third.AsParserError;

            var body = new List<IAst?>();

            while (CurrentToken.Value != "}")
            {
                body.Add(BooleanExpression());
                if (!SkipSemicolon)
                    Eat(";");
                else
                    SkipSemicolon = false;
            }

            var fourth = Eat("}");
            if (fourth.IsError) return fourth.AsParserError;

            SkipSemicolon = true;

            return new FunctionDeclaration
            {
                Name = "FunctionDeclaration",
                FunctionName = name,
                Arguments = arguments.ToArray(),
                Body = new Block
                {
                    Name = "Block",
                    Statements = body.ToArray()
                },
                Line = funToken.atLine,
                Char = funToken.atChar,
                CanRunInGlobalScope = true
            };
        }

        if (CurrentToken.Value == "return")
        {
            var returnToken = Eat("return");
            var expr = BooleanExpression();
            return new Return
            {
                Name = "Return",
                Expr = expr,
                Line = returnToken.atLine,
                Char = returnToken.atChar,
                CanRunInGlobalScope = false
            };
        }

        if (CurrentToken.IsIdentifier() || CurrentToken.IsScopeIdentifier())
        {
            // decide if it's a function call or a variable
            if (Peek().Value == "(")
            {
                var name = Eat();
                Eat("(");

                var arguments = new List<IAst?>();

                while (CurrentToken.Value != ")")
                {
                    arguments.Add(BooleanExpression());

                    if (CurrentToken.Value == ",")
                    {
                        Eat(",");
                    }
                    else
                    {
                        break;
                    }
                }

                var eaten = Eat(")");
                if (eaten.IsError) return eaten.AsParserError;

                return new FunctionCall
                {
                    Name = "FunctionCall",
                    FunctionName = name,
                    Arguments = arguments.ToArray(),
                    Line = name.atLine,
                    Char = name.atChar,
                    CanRunInGlobalScope = true
                };
            }

            // check if it's a variable assignment
            if (Peek().Value == "=")
            {
                var name = Eat();
                var equal = Eat("=");
                var value = BooleanExpression();

                if (value != null)
                    return new VariableAssignment
                    {
                        Name = "VariableAssignment",
                        VariableName = name,
                        Value = value,
                        Line = name.atLine,
                        Char = name.atChar,
                        CanRunInGlobalScope = true
                    };

                return new Error
                    { Name = ErrorMessages.Error.NullVariableAssignment, Line = equal.atLine, Char = equal.atChar, CanRunInGlobalScope = true};
            }

            var variable = Eat();
            return new Variable
            {
                Name = "Variable",
                Token = variable,
                Line = variable.atLine,
                Char = variable.atChar,
                CanRunInGlobalScope = false
            };
        }
        
        // Now we convert the lexer errors to parser errors for the environment to handle
        // Only one type of errors makes it easier to handle
        if(CurrentToken.IsError)
            return new Error
            {
                Name = CurrentToken.Error,
                Line = CurrentToken.atLine,
                Char = CurrentToken.atChar,
                CanRunInGlobalScope = true
            };

        
        return null;
    }

    public IAst? Factor()
    {
        if (CurrentToken.IsInt())
        {
            var token = Eat();
            if(token.IsError) return token.AsParserError;
            
            return new Int
            {
                Name = "Int",
                Token = token,
                Line = token.atLine,
                Char = token.atChar,
                CanRunInGlobalScope = false
            };
        }

        if (CurrentToken.IsFloat())
        {
            var token = Eat();
            if(token.IsError) return token.AsParserError;
            
            return new Float
            {
                Name = "Float",
                Token = token,
                Line = token.atLine,
                Char = token.atChar,
                CanRunInGlobalScope = false
            };
        }

        if (CurrentToken.IsBoolean())
        {
            var token = Eat();
            if(token.IsError) return token.AsParserError;
            
            return new Boolean
            {
                Name = "Boolean",
                Token = token,
                Line = token.atLine,
                Char = token.atChar,
                CanRunInGlobalScope = false
            };
        }

        if (CurrentToken.IsString())
        {
            var token = Eat();
            if(token.IsError) return token.AsParserError;
            
            
            return new String
            {
                Name = "String",
                Token = token,
                Line = token.atLine,
                Char = token.atChar,
                CanRunInGlobalScope = false
            };
        }

        if (CurrentToken.Value == "(")
        {
            // has to be valid
            Eat("(");

            var node = BooleanExpression();
            
            var eaten = Eat(")");
            if (eaten.IsError) return eaten.AsParserError;
            
            return node;
        }

        return Structures();
    }

    public IAst? Unary()
    {
        // UnaryOp: +1, -1
        while (CurrentToken.Value == "+" || CurrentToken.Value == "-")
        {
            var token = CurrentToken;

            var eaten = Eat(token.Value);
            if (eaten.IsError) return eaten.AsParserError;

            return new UnaryOp
            {
                Name = "UnaryOp",
                Op = token,
                Expr = Factor(),
                Line = token.atLine,
                Char = token.atChar,
                CanRunInGlobalScope = false
            };
        }

        return Factor();
    }

    public IAst? Term()
    {
        var node = Unary();

        while (CurrentToken.Value == "*" || CurrentToken.Value == "/" || CurrentToken.Value == "%" ||
               CurrentToken.Value == "<<" ||
               CurrentToken.Value == ">>")
        {
            var token = CurrentToken;

            var eaten = Eat(token.Value);
            if (eaten.IsError) return eaten.AsParserError;

            node = new BinaryOp
            {
                Name = "BinaryOp",
                Left = node,
                Op = token,
                Right = Unary(),
                Line = token.atLine,
                Char = token.atChar,
                CanRunInGlobalScope = false
            };
        }

        return node;
    }

    public IAst? Expression()
    {
        var node = Term();

        while (CurrentToken.Value == "+" || CurrentToken.Value == "-")
        {
            var token = CurrentToken;

            var eaten = Eat(token.Value);
            if (eaten.IsError) return eaten.AsParserError;

            node = new BinaryOp
            {
                Name = "BinaryOp",
                Left = node,
                Op = token,
                Right = Term(),
                Line = token.atLine,
                Char = token.atChar,
                CanRunInGlobalScope = false
            };
        }

        return node;
    }

    public IAst? BooleanExpression()
    {
        var node = Expression();

        string[] booleanOperators = { "==", "!=", ">=", "<=", ">", "<", "&&", "||" };

        while (booleanOperators.Contains(CurrentToken.Value))
        {
            var token = CurrentToken;

            var eaten = Eat(token.Value);
            if (eaten.IsError) return eaten.AsParserError;

            node = new BinaryOp
            {
                Name = "BinaryOp",
                Left = node,
                Op = token,
                Right = Expression(),
                Line = token.atLine,
                Char = token.atChar,
                CanRunInGlobalScope = false
            };
        }

        return node;
    }

    public Block? Parse()
    {
        var statements = new List<IAst>();
        var token = CurrentToken;

        while (CurrentToken.TokenType != Token.Token.Type.Eof)
        {
            var node = BooleanExpression();

            //if (node is Error)
            //{
            //    // DO NOTHING
            //}

            if (node != null) statements.Add(node);

            if (SkipSemicolon)
            {
                SkipSemicolon = false;
                continue;
            }

            if (CurrentToken.TokenType == Token.Token.Type.Eof)
            {
                statements.Add(new Error {Char = token.atChar, Line = token.atLine, Name = ErrorMessages.Error.UnexpectedEndOfFile, CanRunInGlobalScope = true});
                break;
            }
            
            var eaten = Eat(";");
            if(eaten.IsError)
                statements.Add(eaten.AsParserError);
        }
        
        // check if the block contains errors
        bool containsErrors = false;
        foreach (IAst statement in statements)
        {
            if (statement is Error error)
            {
                containsErrors = true;
                ErrorMessages.Instance.PrintError(error);
            }
        }

        if (containsErrors) return null;

        return new Block
        {
            Name = "Block",
            Statements = statements.ToArray(),
            Line = token.atLine,
            Char = token.atChar,
            CanRunInGlobalScope = true
        };
    }

    public interface IAst
    {
        public int Line { get; set; }
        public int Char { get; set; }
        
        public bool CanRunInGlobalScope { get; set; }
        
        public bool Valid()
        {
            return true;
        }
    }
    
    public struct Error : IAst
    {
        // Error Name ID
        public ErrorMessages.Error Name;
        
        // Location of the error
        public int Line { get; set; }
        public int Char { get; set; }
        
        public bool CanRunInGlobalScope { get; set; }
    }

    public struct Block : IAst
    {
        public string Name;
        public IAst?[] Statements;

        public bool ContainsErrors()
        {
            foreach (var statement in Statements)
            {
                if (statement is Error)
                    return true;
                if (statement is Block)
                    return ((Block) statement).ContainsErrors();
                if (statement is If)
                    return (bool)((If) statement).Body?.ContainsErrors();
                if (statement is FunctionDeclaration)
                    return (bool)((FunctionDeclaration) statement).Body?.ContainsErrors();
                
                // IMPORTANT: When using blocks inside of a statement, make sure to check if the block contains errors
                // Needed for recursion checking
            }

            return false;
        }
        
        public Error[] GetErrors()
        {
            var errors = new List<Error>();
            
            foreach (var statement in Statements)
            {
                if (statement is Error)
                    errors.Add((Error) statement);
                if (statement is Block)
                    errors.AddRange(((Block) statement).GetErrors());
                if (statement is If)
                    errors.AddRange(((If) statement).Body?.GetErrors() ?? new Error[0]);
                if (statement is FunctionDeclaration)
                    errors.AddRange(((FunctionDeclaration) statement).Body?.GetErrors() ?? new Error[0]);
                
                // IMPORTANT: When using blocks inside of a statement, make sure to check if the block contains errors
                // Needed for recursion checking
            }

            return errors.ToArray();
        }
        
        public int Line { get; set; }
        public int Char { get; set; }
        public bool CanRunInGlobalScope { get; set; }

        public bool Valid()
        {
            return Statements.Length > 0;
        }
    }

    public struct Int : IAst
    {
        public string Name;
        public Token.Token Token;
        public int Line { get; set; }
        public int Char { get; set; }
        public bool CanRunInGlobalScope { get; set; }
    }

    public struct Float : IAst
    {
        public string Name;
        public Token.Token Token;
        public int Line { get; set; }
        public int Char { get; set; }
        public bool CanRunInGlobalScope { get; set; }
    }

    public struct Boolean : IAst
    {
        public string Name;
        public Token.Token Token;
        public int Line { get; set; }
        public int Char { get; set; }
        public bool CanRunInGlobalScope { get; set; }
    }

    public struct String : IAst
    {
        public string Name;
        public Token.Token Token;
        public int Line { get; set; }
        public int Char { get; set; }
        public bool CanRunInGlobalScope { get; set; }
    }

    public struct Variable : IAst
    {
        public string Name;
        public Token.Token Token;
        public int Line { get; set; }
        public int Char { get; set; }
        public bool CanRunInGlobalScope { get; set; }
    }

    public struct BinaryOp : IAst
    {
        public string Name;
        public IAst? Left;
        public Token.Token Op;
        public IAst? Right;

        public int Line { get; set; }
        public int Char { get; set; }
        public bool CanRunInGlobalScope { get; set; }

        public bool Valid()
        {
            return Left != null && Right != null;
        }
    }

    public struct VariableDeclaration : IAst
    {
        public string Name;
        public Token.Token VariableName;
        public IAst? Value;

        public int Line { get; set; }
        public int Char { get; set; }
        public bool CanRunInGlobalScope { get; set; }

        public bool Valid()
        {
            return VariableName != null && Value != null;
        }
    }

    public struct VariableAssignment : IAst
    {
        public string Name;
        public Token.Token VariableName;
        public Token.Token Op;
        public IAst Value;

        public int Line { get; set; }
        public int Char { get; set; }
        public bool CanRunInGlobalScope { get; set; }

        public bool Valid()
        {
            return VariableName != null && Value != null;
        }
    }

    public struct UnaryOp : IAst
    {
        public string Name;
        public Token.Token Op;
        public IAst? Expr;

        public int Line { get; set; }
        public int Char { get; set; }
        public bool CanRunInGlobalScope { get; set; }

        public bool Valid()
        {
            return Expr != null;
        }
    }

    public struct If : IAst
    {
        public string Name;
        public IAst? Condition;
        public Block? Body;

        public int Line { get; set; }
        public int Char { get; set; }
        public bool CanRunInGlobalScope { get; set; }

        public bool Valid()
        {
            return Condition != null && Body != null;
        }
    }

    public struct FunctionDeclaration : IAst
    {
        public string Name;
        public Token.Token FunctionName;
        public Token.Token[] Arguments;
        public Block? Body;

        public int Line { get; set; }
        public int Char { get; set; }
        public bool CanRunInGlobalScope { get; set; }

        public bool Valid()
        {
            return FunctionName != null && Body != null;
        }
    }

    public struct FunctionCall : IAst
    {
        public string Name;
        public Token.Token FunctionName;
        public IAst?[] Arguments;

        public int Line { get; set; }
        public int Char { get; set; }
        public bool CanRunInGlobalScope { get; set; }

        public bool Valid()
        {
            return FunctionName != null;
        }
    }

    public struct Return : IAst
    {
        public string Name;
        public IAst? Expr;

        public int Line { get; set; }
        public int Char { get; set; }
        public bool CanRunInGlobalScope { get; set; }

        public bool Valid()
        {
            return Expr != null;
        }
    }
}