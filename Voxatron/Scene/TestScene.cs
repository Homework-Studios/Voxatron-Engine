using System.Numerics;
using Raylib_cs;
using Voxatron_Engine.Scene.Entities._2D;

namespace Voxatron.Scene;

public class TestScene : Voxatron_Engine.Scene.Scene
{
    public override void Init()
    {
        Button button = new Button(new Vector2(60, 30), new Vector2(50, 20), new Color(255, 0, 0, 255), new Color(200, 0, 0, 255), Color.WHITE, "Stop");
        button.ButtonClicked += () => { Engine.Shutdown(); };
        Add(button);
    }

    public override void Update()
    {
        UpdateEntities();   
    }
}