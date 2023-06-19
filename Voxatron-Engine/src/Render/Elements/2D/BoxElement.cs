using System.Numerics;
using Raylib_cs;

namespace Voxatron_Engine.Render.Elements._2D;

public class BoxElement : Element
{
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
    public Color Color { get; set; }
    
    public BoxElement(Vector2 position, Vector2 size, Color color)
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
        Raylib.DrawRectangleRounded(new Rectangle(Position.X, Position.Y, Size.X, Size.Y), 1f, 15, Color);
        return true;
    }
}