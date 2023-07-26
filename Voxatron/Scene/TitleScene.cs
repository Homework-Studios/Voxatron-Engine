using Raylib_cs;
using Voxatron_Engine.Scene.Entities._2D;
using Voxatron_Engine.Scene.Entities._2D.Debug;
using Voxatron_Engine.Scene.Entities._2D.Simple;
using Image = Voxatron_Engine.Scene.Entities._2D.Image;

namespace Voxatron.Scene;

public class TitleScene : Voxatron_Engine.Scene.Scene
{
    public override void Init()
    {
        Add(new DebugLog());
        
        Button newGame = new SizeButton(new(10, 10), new (120, 20), "New Game");
        Button continueGame = new SizeButton(new(10, 20), new (120, 20), "Continue Game");
        Button options = new SizeButton(new(10, 30), new (120, 20), "Options");
        Button exit = new SizeButton(new(10, 40), new (120, 20), "Exit");
        
        newGame.OnClick += () => { Engine.AttachScene(new TestScene()); };
        continueGame.OnClick += () => { Engine.AttachScene(new Test3dScene()); };
        exit.OnClick += () => { Engine.Shutdown(); };
        
        Add(newGame);
        Add(continueGame);
        Add(options);
        Add(exit);
    }

    public override void Update()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_P))
        {
            Add(new Popup(Raylib.GetClipboardText_()));
        }
        
        // log time when t is pressed
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_T))
        {
            DebugLog.Instance?.Log("Time: " + DateTime.Now);
        }
        
        UpdateEntities();
    }

    public override void Unload()
    {
        
    }
}