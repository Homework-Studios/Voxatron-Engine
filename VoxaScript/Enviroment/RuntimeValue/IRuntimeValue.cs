namespace VoxaScript.Enviroment.RuntimeValue;

public interface IRuntimeValue
{
    // Binary operations
    public IRuntimeValue Add(IRuntimeValue value);
    public IRuntimeValue Sub(IRuntimeValue value);
    public IRuntimeValue Mul(IRuntimeValue value);
    public IRuntimeValue Div(IRuntimeValue value);
    public IRuntimeValue Mod(IRuntimeValue value);
    public IRuntimeValue Shr(IRuntimeValue value);
    public IRuntimeValue Shl(IRuntimeValue value);

    // Boolean operations
    public Boolean Eq(IRuntimeValue value);
    public Boolean Neq(IRuntimeValue value);
}