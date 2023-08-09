namespace VoxaScript.Enviroment.RuntimeValue;

public struct Boolean : IRuntimeValue
{
    public bool Value;

    public override string ToString()
    {
        return Value.ToString();
    }

    public IRuntimeValue Add(IRuntimeValue value)
    {
        if (value is String str) return new String { Value = Value + str.Value };

        throw new Exception("Cannot add boolean to " + value.GetType());
    }

    public IRuntimeValue Sub(IRuntimeValue value)
    {
        throw new Exception("Cannot sub boolean to " + value.GetType());
    }

    public IRuntimeValue Mul(IRuntimeValue value)
    {
        throw new Exception("Cannot mul boolean to " + value.GetType());
    }

    public IRuntimeValue Div(IRuntimeValue value)
    {
        throw new Exception("Cannot div boolean to " + value.GetType());
    }

    public IRuntimeValue Mod(IRuntimeValue value)
    {
        throw new Exception("Cannot div boolean to " + value.GetType());
    }

    public IRuntimeValue Shr(IRuntimeValue value)
    {
        throw new Exception("Cannot div boolean to " + value.GetType());
    }

    public IRuntimeValue Shl(IRuntimeValue value)
    {
        throw new Exception("Cannot div boolean to " + value.GetType());
    }

    public Boolean Eq(IRuntimeValue value)
    {
        if (value is Boolean b) return new Boolean { Value = Value == b.Value };

        return new Boolean { Value = false };
    }

    public Boolean Neq(IRuntimeValue value)
    {
        if (value is Boolean b) return new Boolean { Value = Value != b.Value };

        return new Boolean { Value = true };
    }
}