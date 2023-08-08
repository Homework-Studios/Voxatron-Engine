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

    public IRuntimeValue Eq(IRuntimeValue value)
    {
        return value;
    }

    public IRuntimeValue Neq(IRuntimeValue value)
    {
        return value;
    }
}