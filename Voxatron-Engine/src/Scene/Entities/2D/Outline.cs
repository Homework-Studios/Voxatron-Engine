using System.Numerics;
using Raylib_cs;
using Voxatron_Engine.Render;
using Voxatron_Engine.Render.Elements._2D;
using Voxatron_Engine.Tool;

namespace Voxatron_Engine.Scene.Entities._2D;

public class Outline : Entity
{
    public Vector2 Position;
    public Vector2 Size;
    public Color Color;
    public BoxOutlineElement Element { get; }

    public Outline(Vector2 position, Vector2 size, Color color)
    {
        Position = ScreenUtil.ScreenPercent(position);
        Size = size;
        Color = color;
        Element = new(position, size, color);
    }

    public override bool Init(Renderer renderer)
    {
        renderer.Add(Element);
        return true;
    }

    public override bool Remove(Renderer renderer)
    {
        renderer.Remove(Element);
        return true;
    }

    public override bool Update()
    {
        return true;
    }
}