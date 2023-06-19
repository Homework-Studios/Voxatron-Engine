using System.Numerics;
using Raylib_cs;
using Voxatron_Engine.Render;
using Voxatron_Engine.Render.Elements._2D;
using Voxatron_Engine.Tool;

namespace Voxatron_Engine.Scene.Entities._2D;

public class Box : Entity
{
    
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
    public Color Color { get; set; }
    
    private BoxElement _element;
    
    public Box(Vector2 position, Vector2 size, Color color)
    {
        Position = ScreenUtil.ScreenPercent(position) - size / 2;
        Size = size;
        Color = color;
        
        _element = new BoxElement(Position, Size, Color, false);
    }
    
    public override bool Init(Renderer renderer)
    {
        renderer.Add(_element);
        return true;
    }

    public override bool Remove(Renderer renderer)
    {
        renderer.Remove(_element);
        return true;
    }

    public override bool Update()
    {
        return true;
    }
}