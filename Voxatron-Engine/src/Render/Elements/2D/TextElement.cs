using System.Numerics;
using Raylib_cs;

namespace Voxatron_Engine.Render.Elements._2D;

public class TextElement : Element
{
    private readonly string _text;
    private readonly Vector2 _position;
    private readonly Color _color;
    private readonly int _fontSize;
    
    public TextElement(string text, Vector2 position, Color color, int fontSize)
    {
        _text = text;
        _position = position;
        _color = color;
        _fontSize = fontSize;
    }
    
    public override bool Update()
    {
        return true;
    }

    public override bool Render()
    {
        // the origin in in the middle of the text
        // move the text back by half the width and half of the font size up
        Raylib.DrawText(_text, (int)_position.X - Raylib.MeasureText(_text, _fontSize) / 2, (int)_position.Y - _fontSize / 2, _fontSize, _color);
        return true;
    }
}