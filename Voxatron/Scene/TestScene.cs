using Raylib_cs;
using Voxatron_Engine.Scene.Entitys._2D;

namespace Voxatron.Scene;

public class TestScene : Voxatron_Engine.Scene.Scene
{
    public override void Init()
    {
        Button btn = new Button(new(60, 30), new(50, 20), new Color(255, 0, 0, 255), new Color(200, 0, 0, 255), Color.WHITE, "Stop");
        btn.ButtonClicked += () => { Engine.Destroy(); };
        Add(btn);
    }

    public override void Update()
    {
        UpdateEntities();   
    }
}