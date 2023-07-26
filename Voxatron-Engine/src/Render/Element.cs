namespace Voxatron_Engine.Render;

public abstract class Element
{
    public int Layer = 0;
    public abstract bool Update();

    public abstract bool Render();
}