using System.Numerics;
using Raylib_cs;
using Voxatron_Engine.Render;
using Voxatron_Engine.Render.Elements._2D;

namespace Voxatron_Engine.Scene.Entitys._2D;

public class Text : Entity
{
    
    public string Content;
    public Vector2 Position;
    public Color Color;
    public TextElement Element;
    
    public Text(string content, Vector2 position, Color color)
    {
        Content = content;
        Position = position;
        Color = color;
        Element = new(Content, Position, Color, 20);
    }
    
    public override bool Init(Renderer renderer)
    {
        renderer.Add(Element);
        return true;
    }

    public override bool Update()
    {
        return true;
    }
}