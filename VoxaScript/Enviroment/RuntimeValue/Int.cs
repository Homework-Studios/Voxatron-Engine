namespace VoxaScript.Enviroment.RuntimeValue;

public struct Int : IRuntimeValue
{
    public int Value;

    public override string ToString()
    {
        return Value.ToString();
    }

    public IRuntimeValue Add(IRuntimeValue value)
    {
        if (value is String str) return new String { Value = Value + str.Value };

        if (value is Int i) return new Int { Value = Value + i.Value };

        if (value is Float) return new Float { Value = Value + ((Float)value).Value };

        throw new Exception("Cannot add int to " + value.GetType());
    }

    public IRuntimeValue Sub(IRuntimeValue value)
    {
        if (value is Int i) return new Int { Value = Value - i.Value };

        if (value is Float f) return new Float { Value = Value - f.Value };

        throw new Exception("Cannot sub int to " + value.GetType());
    }

    public IRuntimeValue Mul(IRuntimeValue value)
    {
        if (value is Int i) return new Int { Value = Value * i.Value };

        if (value is Float f) return new Float { Value = Value * f.Value };

        throw new Exception("Cannot mul int to " + value.GetType());
    }

    public IRuntimeValue Div(IRuntimeValue value)
    {
        if (value is Int i) return new Int { Value = Value / i.Value };

        if (value is Float f) return new Float { Value = Value / f.Value };

        throw new Exception("Cannot div int to " + value.GetType());
    }

    public IRuntimeValue Mod(IRuntimeValue value)
    {
        if (value is Int i) return new Int { Value = Value % i.Value };

        if (value is Float f) return new Float { Value = Value % f.Value };

        throw new Exception("Cannot mod int to " + value.GetType());
    }

    public IRuntimeValue Shr(IRuntimeValue value)
    {
        if (value is Int i) return new Int { Value = Value >> i.Value };

        throw new Exception("Cannot shift int using " + value.GetType());
    }

    public IRuntimeValue Shl(IRuntimeValue value)
    {
        if (value is Int i) return new Int { Value = Value << i.Value };

        throw new Exception("Cannot shift int using " + value.GetType());
    }

    public Boolean Eq(IRuntimeValue value)
    {
        if (value is Int i) return new Boolean { Value = Value == i.Value };

        if (value is Float f) return new Boolean { Value = Math.Abs(Value - f.Value) < 0.0001f };

        return new Boolean { Value = false };
    }

    public Boolean Neq(IRuntimeValue value)
    {
        if (value is Int i) return new Boolean { Value = Value != i.Value };

        if (value is Float f) return new Boolean { Value = Math.Abs(Value - f.Value) > 0.0001f };

        return new Boolean { Value = true };
    }
    
    public Boolean Gt(IRuntimeValue value)
    {
        if (value is Int i) return new Boolean { Value = Value > i.Value };

        if (value is Float f) return new Boolean { Value = Value > f.Value };

        throw new Exception("Cannot gt int to " + value.GetType());
    }
    
    public Boolean Lt(IRuntimeValue value)
    {
        if (value is Int i) return new Boolean { Value = Value < i.Value };

        if (value is Float f) return new Boolean { Value = Value < f.Value };

        throw new Exception("Cannot lt int to " + value.GetType());
    }
}