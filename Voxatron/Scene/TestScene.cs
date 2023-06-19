using System.Numerics;
using Raylib_cs;
using Voxatron_Engine.Scene.Entities._2D;

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
            Add(new Popup("This is a popup!"));
        };
        Add(popupButton);
    }

    public override void Update()
    {
        UpdateEntities();   
    }
}