using System.Numerics;
using Raylib_cs;
using Voxatron_Engine.Scene.Entities._2D;
using Voxatron_Engine.Scene.Entities._2D.Debug;
using Image = Voxatron_Engine.Scene.Entities._2D.Image;

namespace Voxatron.Scene;

public class TestScene : Voxatron_Engine.Scene.Scene
{
    public override void Init()
    {
        Add(new DebugLog());
        
        var button = new Button(new Vector2(5, 5), new Vector2(50, 20), new Color(255, 0, 0, 255),
            new Color(200, 0, 0, 255), Color.WHITE, "Stop");
        button.OnClick += () => { Engine.Shutdown(); };
        Add(button);

        var popupButton = new Button(new Vector2(5, 10), new Vector2(50, 20), new Color(0, 255, 0, 255),
            new Color(0, 200, 0, 255), Color.WHITE, "Popup");
        popupButton.OnClick += () =>
        {
            Add(new Popup("This is a popup! I like to eat monkeys! Monkey flesh is delicious! Yum!"));
        };
        Add(popupButton);

        var workingDirectory = Environment.CurrentDirectory;
        // go up 3 dirs
        workingDirectory += "\\..\\..\\..\\";

        Add(new Image(new Vector2(50, 50), workingDirectory + "Assets\\monkey.png", 2.0f));

        Add(new Toggle(new Vector2(5, 15), new Vector2(50, 20), new Color(255, 0, 0, 255), new Color(0, 255, 0, 255), new Color(50, 50, 50, 0),
            Color.WHITE, "Toggle"));

        Add(new Slider(new Vector2(5, 20), new Vector2(50, 20), new Color(255, 255, 0, 255),
            new Color(200, 200, 0, 255), 5, 0.5f));
    }

    public override void Update()
    {
        UpdateEntities();
    }

    public override void Unload()
    {
        
    }
}