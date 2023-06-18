using System.Numerics;
using Raylib_cs;

namespace Voxatron_Engine.Render.Elements._2D;

public class BoxOutlineElement : Element
{
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
    public Color Color { get; set; }
    
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
        Raylib.DrawRectangleRoundedLines(new Rectangle(Position.X, Position.Y, Size.X, Size.Y), 0.1f, 5, 3, Color);
        return true;
    }
}