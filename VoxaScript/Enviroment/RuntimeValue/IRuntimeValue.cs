namespace VoxaScript.Enviroment.RuntimeValue;

public interface IRuntimeValue
{
    public IRuntimeValue Add(IRuntimeValue value);
    public IRuntimeValue Sub(IRuntimeValue value);
    public IRuntimeValue Mul(IRuntimeValue value);
    public IRuntimeValue Div(IRuntimeValue value);
    public IRuntimeValue Eq(IRuntimeValue value);
    public IRuntimeValue Neq(IRuntimeValue value);
}