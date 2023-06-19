using System.Numerics;
using Raylib_cs;
using Voxatron_Engine.Scene.Entities._2D;

namespace Voxatron.Scene;

public class TitleScene : Voxatron_Engine.Scene.Scene
{
    public override void Init()
    {
        Vector2 screenSize = new(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
        screenSize -= new Vector2(10, 10);
        
        Add(new Box(new(50, 50), screenSize, new (40, 40, 40, 255)));
    }

    public override void Update()
    {
        UpdateEntities();
    }
}