using System.Numerics;
using Raylib_cs;
using Voxatron_Engine.Render;
using Voxatron_Engine.Render.Elements._2D;
using Voxatron_Engine.Tool;

namespace Voxatron_Engine.Scene.Entities._2D;

public class Popup : Entity
{

    const int FontSize = 20;

    private BoxElement _blend;
    private BoxElement _box;
    private TextElement _textElement;

    public Popup(string text)
    {
        Vector2 center = ScreenUtil.Center(new());
        
        // Top and bottom FontSize + 20 and sides MeasureText + 20
        Vector2 size = new Vector2(Raylib.MeasureText(text, FontSize) + 40, FontSize + 40);
        
        // min size of 100x100
        size.X = Math.Max(size.X, 200);
        size.Y = Math.Max(size.Y, 100);

        _blend = new BoxElement(new (-100, -100), new (Raylib.GetScreenWidth() * 2, Raylib.GetScreenHeight() * 2), new(0, 0, 0, 100), false);
        _box = new BoxElement(center - size / 2, size, new(30, 30, 30, 255), false);
        // center the text
        _textElement = new TextElement(text, center, Color.WHITE, FontSize);
    }
    
    public override bool Init(Renderer renderer)
    {
        renderer.Add(_blend);
        renderer.Add(_box);
        renderer.Add(_textElement);
        return true;
    }

    public override bool Remove(Renderer renderer)
    {
        renderer.Remove(_blend);
        renderer.Remove(_box);
        renderer.Remove(_textElement);
        return true;
    }

    public override bool Update()
    {
        if(Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT) || Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER) || Raylib.IsKeyPressed(KeyboardKey.KEY_ESCAPE))
        {
            Scene.Remove(this);
        }
        return true;
    }
}