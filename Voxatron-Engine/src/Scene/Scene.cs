using Voxatron_Engine.Render;

namespace Voxatron_Engine.Scene;

public abstract class Scene
{
    protected VoxatronEngine Engine;
    protected Renderer Renderer;
    public List<Entity> Entities = new();

    public abstract void Init();
    public abstract void Update();
    
    public void UpdateEntities()
    {
        foreach (Entity entity in Entities)
        {
            if (!entity.Update())
            {
                throw new Exception("Entity failed to update. Entity: " + entity.GetType().Name + "");
            }
        }
    }
    
    public void Add(Entity entity)
    {
        Entities.Add(entity);
        entity.SetScene(this);
        entity.Init(Renderer);
    }
    
    public void Remove(Entity entity)
    {
        Entities.Remove(entity);
    }
    
    public void Clear()
    {
        Entities.Clear();
    }
    
    public void SetEngine(VoxatronEngine engine)
    {
        Engine = engine;
    }
    
    public void SetRenderer(Renderer renderer)
    {
        Renderer = renderer;
    }
}