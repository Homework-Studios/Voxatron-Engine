using Voxatron_Engine.Render;

namespace Voxatron_Engine.Scene;

public abstract class Entity
{
    protected Scene Scene = null!;
    public abstract bool Init(Renderer renderer);
    public abstract bool Update();
    
    public void SetScene(Scene scene)
    {
        Scene = scene;
    }
}