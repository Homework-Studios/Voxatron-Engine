using Voxatron_Engine.Render;

namespace Voxatron_Engine.Scene;

public abstract class Scene
{
    protected VoxatronEngine Engine = null!;
    private Renderer _renderer = null!;
    private readonly List<Entity> _entities = new();

    public abstract void Init();
    public abstract void Update();
    public abstract void Unload();

    protected void UpdateEntities()
    {
        foreach (var entity in _entities.ToArray().Where(entity => !entity.Update()))
        {
            throw new Exception("Entity failed to update. Entity: " + entity.GetType().Name + "");
        }
    }

    protected void Add(Entity entity)
    {
        _entities.Add(entity);
        entity.SetScene(this);
        entity.Init(_renderer);
    }
    
    public void Remove(Entity entity, int layer = 0)
    {
        _entities.Remove(entity);
        entity.SetScene(this);
        entity.Remove(_renderer);
    }
    
    public void Clear()
    {
        _entities.Clear();
    }
    
    public void SetEngine(VoxatronEngine engine)
    {
        Engine = engine;
    }
    
    public void SetRenderer(Renderer renderer)
    {
        _renderer = renderer;
    }
}