namespace VoxaScript.Enviroment.RuntimeValue;

public struct Return : IRuntimeValue
{
    public IRuntimeValue ReturnValue;

    public IRuntimeValue Add(IRuntimeValue value)
    {
        return value;
    }

    public IRuntimeValue Sub(IRuntimeValue value)
    {
        return value;
    }

    public IRuntimeValue Mul(IRuntimeValue value)
    {
        return value;
    }

    public IRuntimeValue Div(IRuntimeValue value)
    {
        return value;
    }

    public IRuntimeValue Mod(IRuntimeValue value)
    {
        return value;
    }

    public IRuntimeValue Shr(IRuntimeValue value)
    {
        return value;
    }

    public IRuntimeValue Shl(IRuntimeValue value)
    {
        return value;
    }
    
    public Boolean Gt(IRuntimeValue value)
    {
        if (value is Boolean b)
            return b;
        return new Boolean { Value = false };
    }

    public Boolean Lt(IRuntimeValue value)
    {
        if (value is Boolean b)
            return b;
        return new Boolean { Value = false };
    }

    public Boolean Eq(IRuntimeValue value)
    {
        if (value is Boolean b)
            return b;
        return new Boolean { Value = false };
    }

    public Boolean Neq(IRuntimeValue value)
    {
        if (value is Boolean b)
            return b;
        return new Boolean { Value = false };
    }
}