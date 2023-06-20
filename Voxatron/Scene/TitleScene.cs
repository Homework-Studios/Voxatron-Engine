using System.Numerics;
using Raylib_cs;
using Voxatron_Engine.Scene.Entities._2D;
using Voxatron_Engine.Scene.Entities._2D.Debug;
using Image = Voxatron_Engine.Scene.Entities._2D.Image;

namespace Voxatron.Scene;

public class TitleScene : Voxatron_Engine.Scene.Scene
{
    public override void Init()
    {
        var workingDirectory = Environment.CurrentDirectory;
        workingDirectory += "\\..\\..\\..\\";

        Add(new Image(new(50, 50), workingDirectory + "Assets/background.png", 1, true));
        
        Add(new Box(new (50, 50), new (Raylib.GetScreenWidth() * 2, Raylib.GetScreenHeight() * 2), new (0,0,0,50)));

        Add(new Box(new (50, 90), new(Raylib.GetScreenWidth() * 0.9f, 75), new (0,0,0, 50), true));
        
        Button resume = new Button(new (15, 90), new (175, 25), Color.RED, Color.BLUE, Color.GOLD, "Resume");
        Button newGame = new Button(new (35, 90), new (175, 25), Color.RED, Color.BLUE, Color.GOLD, "New Game");
        Button settings = new Button(new (65, 90), new (175, 25), Color.RED, Color.BLUE, Color.GOLD, "Settings");
        Button credits = new Button(new (85, 90), new (175, 25), Color.RED, Color.BLUE, Color.GOLD, "Credits");

        credits.ButtonClicked += () => Add(new Popup("This game was made by Homework Studios, founded by Jonas Windmann and Timon Richter. \nHomework studios is a small indie game studio from Germany. We are currently working on our first game, which is this one. We hope you enjoy it!"));

        Add(resume);
        Add(newGame);
        Add(settings);
        Add(credits);
        
        //Add(new PercentageGrid());
    }

    public override void Update()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_P))
        {
            Add(new Popup(Raylib.GetClipboardText_()));
        }
        
        UpdateEntities();
    }
}