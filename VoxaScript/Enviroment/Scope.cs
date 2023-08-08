using VoxaScript.Enviroment.RuntimeValue;

namespace VoxaScript.Enviroment;

public class Scope
{
    public bool IsGlobal = false;
    public Scope? Parent = null;
    
    public Dictionary<string, IRuntimeValue?> Variables = new();
    // now for functions with arguments and the body
    public Dictionary<string, (Token.Token[] args, Parser.Parser.Block? body)> Functions = new();

    public Scope(Scope? parent = null)
    {
        Parent = parent;
    }
    
    public void DefineVariable(string name, IRuntimeValue? value)
    {
        Variables[name] = value;
    }
    
    public void AssignVariable(string name, IRuntimeValue? value)
    {
        if (Variables.ContainsKey(name))
        {
            Variables[name] = value;
            return;
        }
        
        if (Parent != null)
        {
            Parent.AssignVariable(name, value);
            return;
        }
        
        throw new Exception("Variable " + name + " does not exist.");
    }
    
    public void DefineFunction(string name, Token.Token[] args, Parser.Parser.Block? body)
    {
        Functions[name] = (args, body);
    }
    
    public IRuntimeValue? GetVariable(string name)
    {
        if (Variables.TryGetValue(name, out var variable))
        {
            return variable;
        }
        
        if (Parent != null)
        {
            return Parent.GetVariable(name);
        }
        
        return null;
    }
    
    public (Token.Token[] args, Parser.Parser.IAst? body)? GetFunction(string name)
    {
        if (Functions.TryGetValue(name, out var function))
        {
            return function;
        }
        
        if (Parent != null)
        {
            return Parent.GetFunction(name);
        }
        
        return null;
    }
    
    public static Scope CreateGlobalScope()
    {
        Scope scope = new Scope();
        scope.IsGlobal = true;

        return scope;
    }
}