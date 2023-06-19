using System.Numerics;
using Raylib_cs;

namespace Voxatron_Engine.Render.Elements._2D;

public class BoxOutlineElement : Element
{
    private Vector2 Position { get; set; }
    private Vector2 Size { get; set; }
    private Color Color { get; set; }
    
    public BoxOutlineElement(Vector2 position, Vector2 size, Color color)
    {
        Position = position;
        Size = size;
        Color = color;
    }


    public override bool Update()
    {
        return true;
    }

    public override bool Render()
    {
        Raylib.DrawRectangleRoundedLines(new Rectangle(Position.X, Position.Y, Size.X, Size.Y), 1f, 15, 3, Color);
        return true;
    }
}