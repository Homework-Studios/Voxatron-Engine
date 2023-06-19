using System.Numerics;
using Raylib_cs;
using Voxatron_Engine.Render;
using Voxatron_Engine.Render.Elements._2D;

namespace Voxatron_Engine.Scene.Entities._2D;

public class Text : Entity
{
    private readonly TextElement _element;
    
    public Text(string content, Vector2 position, Color color)
    {
        _element = new(content, position, color, 20);
    }
    
    public override bool Init(Renderer renderer)
    {
        renderer.Add(_element);
        return true;
    }

    public override bool Update()
    {
        return true;
    }
}