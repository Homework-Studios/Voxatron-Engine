using System.Numerics;
using Raylib_cs;
using Voxatron_Engine.Scene.Entities._2D;

namespace Voxatron.Scene;

public class TitleScene : Voxatron_Engine.Scene.Scene
{
    public override void Init()
    {
        Vector2 screenSize = new(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
        screenSize -= new Vector2(50, 50);
        
        Add(new Box(new(50, 50), screenSize, new (20, 20, 20, 255)));
        
        Add(new Text("Voxatron", new(50, 10), Color.WHITE, 75));
        
        Add(new Box(new (50, 50), new (400, 75), new (50, 50, 50, 255), true));
        Button play = new Button(new(46.8f, 50), new (125, 25), new (0, 200, 0, 255), new (0, 175, 0, 255), Color.WHITE,  "PLAY");
        Button off = new Button(new(57, 50), new (50, 25), new (200, 0, 0, 255), new (175, 0, 0, 255), Color.WHITE,  "OFF");
        
        play.ButtonClicked += () => Engine.AttachScene(new TestScene());
        off.ButtonClicked += () => Engine.Shutdown();
        
        Add(play);
        Add(off);
    }

    public override void Update()
    {
        UpdateEntities();
    }
}