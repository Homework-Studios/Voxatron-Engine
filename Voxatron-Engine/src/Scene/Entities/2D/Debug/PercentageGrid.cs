using System.Numerics;
using Raylib_cs;
using Voxatron_Engine.Render;
using Voxatron_Engine.Render.Elements._2D;
using Voxatron_Engine.Render.Elements._2D.Debug;
using Voxatron_Engine.Tool;

namespace Voxatron_Engine.Scene.Entities._2D.Debug;

public class PercentageGrid : Entity
{

    private TextElement _textElement;

    public PercentageGrid()
    {
        Vector2 mousePosition = MouseToPercentageSpace();
        _textElement = new TextElement($"Mouse Position: {mousePosition}", ScreenUtil.ScreenPercent(new Vector2(50, 50)), Color.WHITE, 27);
    }

    public Vector2 MouseToPercentageSpace()
    {
        Vector2 mousePosition = Raylib.GetMousePosition();
        
        // transform it to screen percent
        mousePosition.X = (int)((mousePosition.X / Raylib.GetScreenWidth()) * 100);
        mousePosition.Y = (int)((mousePosition.Y / Raylib.GetScreenHeight()) * 100);
        
        return mousePosition;
    }
    
    public override bool Init(Renderer renderer)
    {
        renderer.Add(new PercentageGridElement());
        renderer.Add(_textElement);
        return true;
    }

    public override bool Remove(Renderer renderer)
    {
        renderer.Remove(new PercentageGridElement());
        renderer.Remove(_textElement);
        return true;
    }

    public override bool Update()
    {
        _textElement.SetText($"Mouse Position: {MouseToPercentageSpace()}");
        return true;
    }
}