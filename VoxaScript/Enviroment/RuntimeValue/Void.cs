namespace VoxaScript.Enviroment.RuntimeValue;

public struct Void : IRuntimeValue
{
    public bool Return;

    public override string ToString()
    {
        return "void";
    }

    public IRuntimeValue Add(IRuntimeValue value)
    {
        throw new Exception("Cannot add void to " + value.GetType());
    }

    public IRuntimeValue Sub(IRuntimeValue value)
    {
        throw new Exception("Cannot sub void to " + value.GetType());
    }

    public IRuntimeValue Mul(IRuntimeValue value)
    {
        throw new Exception("Cannot mul void to " + value.GetType());
    }

    public IRuntimeValue Div(IRuntimeValue value)
    {
        throw new Exception("Cannot div void to " + value.GetType());
    }

    public IRuntimeValue Mod(IRuntimeValue value)
    {
        throw new Exception("Cannot mod void to " + value.GetType());
    }

    public IRuntimeValue Shr(IRuntimeValue value)
    {
        throw new Exception("Cannot shr void to " + value.GetType());
    }

    public IRuntimeValue Shl(IRuntimeValue value)
    {
        throw new Exception("Cannot shl void to " + value.GetType());
    }

    public Boolean Eq(IRuntimeValue value)
    {
        if (value is Void) return new Boolean { Value = true };

        return new Boolean { Value = false };
    }

    public Boolean Neq(IRuntimeValue value)
    {
        if (value is Void) return new Boolean { Value = false };

        return new Boolean { Value = true };
    }
}