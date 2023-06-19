using System.Numerics;
using Raylib_cs;
using Voxatron_Engine.Scene.Entities._2D;
using Image = Voxatron_Engine.Scene.Entities._2D.Image;

namespace Voxatron.Scene;

public class TestScene : Voxatron_Engine.Scene.Scene
{
    public override void Init()
    {
        Button button = new Button(new Vector2(5, 5), new Vector2(50, 20), new Color(255, 0, 0, 255), new Color(200, 0, 0, 255), Color.WHITE, "Stop");
        button.ButtonClicked += () => { Engine.Shutdown(); };
        Add(button);
        
        Button popupButton = new Button(new Vector2(5, 10), new Vector2(50, 20), new Color(0, 255, 0, 255), new Color(0, 200, 0, 255), Color.WHITE, "Popup");
        popupButton.ButtonClicked += () =>
        {
            Add(new Popup("This is a popup! I like to eat monkeys! Monkey flesh is delicious! Yum!"));
        };
        Add(popupButton);
        
        Add(new Image(new Vector2(50, 50), "C:\\Users\\j.windmann\\Desktop\\Voxatron-Engine\\Voxatron\\Assets\\monkey.png", 2.0f));
        
        Add(new Toggle(new Vector2(5, 15), new Vector2(50, 20), new Color(0, 0, 255, 255), new Color(0, 0, 200, 255), Color.WHITE, "Toggle"));
    }

    public override void Update()
    {
        UpdateEntities();   
    }
}