using Raylib_cs;
using Voxatron_Engine.Render;
using Voxatron_Engine.Scene.Entitys._2D;

namespace Voxatron.Scene;

public class TestScene : Voxatron_Engine.Scene.Scene
{
    public override void Init()
    {
        Add(new Outline(new(10, 10), new(100, 100), Color.BLUE));
    }

    public override void Update()
    {
        
    }
}