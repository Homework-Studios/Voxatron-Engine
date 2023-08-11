namespace Voxatron_Engine_v2;

public interface IWindow
{
    public bool IsOpen { get; set; }

    public void Open();
    protected void Run();
    public void Close();
}