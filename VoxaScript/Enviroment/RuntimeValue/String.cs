using System.Globalization;

namespace VoxaScript.Enviroment.RuntimeValue;

public struct String : IRuntimeValue
{
    public string Value;
    
    public override string ToString() => Value;
    
    public IRuntimeValue Add(IRuntimeValue value)
    {
        if (value is String str)
        {
            return new String { Value = Value + str.Value };
        }
        
        if (value is Int i)
        {
            return new String { Value = Value + i.Value };
        }
        
        if (value is Float f)
        {
            return new String { Value = Value + f.Value };
        }
        
        if (value is Boolean b)
        {
            return new String { Value = Value + b.Value };
        }
        
        throw new Exception("Cannot add string to " + value.GetType());
    }

    public IRuntimeValue Sub(IRuntimeValue value)
    {
        throw new Exception("Cannot sub string to " + value.GetType());
    }
    
    public IRuntimeValue Mul(IRuntimeValue value)
    {
        throw new Exception("Cannot mul string to " + value.GetType());
    }
    
    public IRuntimeValue Div(IRuntimeValue value)
    {
        throw new Exception("Cannot div string to " + value.GetType());
    }

    public IRuntimeValue Eq(IRuntimeValue value)
    {
        if (value is String str)
        {
            return new Boolean { Value = Value.Equals(str.Value) };
        }
        
        if (value is Int i)
        {
            return new Boolean { Value = Value.Equals(i.Value.ToString()) };
        }
        
        if (value is Float f)
        {
            return new Boolean { Value = Value.Equals(f.Value.ToString(CultureInfo.InvariantCulture)) };
        }
        
        if (value is Boolean b)
        {
            return new Boolean { Value = Value.Equals(b.Value.ToString()) };
        }
        
        return new Boolean { Value = false };
    }
    
    public IRuntimeValue Neq(IRuntimeValue value)
    {
        if (value is String str)
        {
            return new Boolean { Value = Value != str.Value };
        }
        
        if (value is Int i)
        {
            return new Boolean { Value = Value != i.Value.ToString() };
        }
        
        if (value is Float f)
        {
            return new Boolean { Value = Value != f.Value.ToString(CultureInfo.InvariantCulture) };
        }
        
        if (value is Boolean b)
        {
            return new Boolean { Value = Value != b.Value.ToString() };
        }
        
        return new Boolean { Value = true };
    }
}