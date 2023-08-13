namespace VoxaScript.Enviroment.RuntimeValue;

public struct Float : IRuntimeValue
{
    public float Value;

    public override string ToString()
    {
        return Value.ToString();
    }

    public IRuntimeValue Add(IRuntimeValue value)
    {
        if (value is String str) return new String { Value = Value + str.Value };

        if (value is Int i) return new Float { Value = Value + i.Value };

        if (value is Float f) return new Float { Value = Value + f.Value };

        throw new Exception("Cannot add float to " + value.GetType());
    }

    public IRuntimeValue Sub(IRuntimeValue value)
    {
        if (value is Int i) return new Float { Value = Value - i.Value };

        if (value is Float f) return new Float { Value = Value - f.Value };

        throw new Exception("Cannot sub float to " + value.GetType());
    }

    public IRuntimeValue Mul(IRuntimeValue value)
    {
        if (value is Int i) return new Float { Value = Value * i.Value };

        if (value is Float f) return new Float { Value = Value * f.Value };

        throw new Exception("Cannot mul float to " + value.GetType());
    }

    public IRuntimeValue Div(IRuntimeValue value)
    {
        if (value is Int i) return new Float { Value = Value / i.Value };

        if (value is Float f) return new Float { Value = Value / f.Value };

        throw new Exception("Cannot div float to " + value.GetType());
    }

    public IRuntimeValue Mod(IRuntimeValue value)
    {
        if (value is Int i) return new Int { Value = (int)(Value % i.Value) };

        if (value is Float f) return new Float { Value = Value % f.Value };

        throw new Exception("Cannot mod int to " + value.GetType());
    }

    public IRuntimeValue Shr(IRuntimeValue value)
    {
        throw new Exception("Cannot shr int to " + value.GetType());
    }

    public IRuntimeValue Shl(IRuntimeValue value)
    {
        throw new Exception("Cannot shl int to " + value.GetType());
    }

    public Boolean Eq(IRuntimeValue value)
    {
        if (value is Int i) return new Boolean { Value = Math.Abs(Value - i.Value) < 0.0001f };

        if (value is Float f) return new Boolean { Value = Math.Abs(Value - f.Value) < 0.0001f };

        return new Boolean { Value = false };
    }

    public Boolean Neq(IRuntimeValue value)
    {
        if (value is Int i) return new Boolean { Value = Math.Abs(Value - i.Value) > 0.0001f };

        if (value is Float f) return new Boolean { Value = Math.Abs(Value - f.Value) > 0.0001f };

        return new Boolean { Value = true };
    }

    public Boolean Gt(IRuntimeValue value)
    {
        if (value is Int i) return new Boolean { Value = Value > i.Value };

        if (value is Float f) return new Boolean { Value = Value > f.Value };

        throw new Exception("Cannot gt float to " + value.GetType());
    }

    public Boolean Lt(IRuntimeValue value)
    {
        if (value is Int i) return new Boolean { Value = Value < i.Value };

        if (value is Float f) return new Boolean { Value = Value < f.Value };

        throw new Exception("Cannot lt float to " + value.GetType());
    }
}