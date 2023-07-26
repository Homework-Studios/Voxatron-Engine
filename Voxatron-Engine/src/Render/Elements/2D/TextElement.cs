using System.Numerics;
using Raylib_cs;

namespace Voxatron_Engine.Render.Elements._2D;

public class TextElement : Element
{
    private string _text;
    private readonly Vector2 _position;
    private readonly Color _color;
    private readonly int _fontSize;
    private readonly bool _centerText;

    public TextElement(string text, Vector2 position, Color color, int fontSize, bool centerText = true)
    {
        _text = text;
        _position = position;
        _color = color;
        _fontSize = fontSize;
        _centerText = centerText;
    }
    
    public override bool Update()
    {
        return true;
    }

    public override bool Render()
    {
        // the origin in in the middle of the text
        // move the text back by half the width and half of the font size up
        if (_centerText)
        {
            Raylib.DrawText(_text, (int)_position.X - Raylib.MeasureText(_text, _fontSize) / 2, (int)_position.Y - _fontSize / 2, _fontSize, _color);
            return true;
        }
        
        Raylib.DrawText(_text, (int)_position.X, (int)_position.Y, _fontSize, _color);
        
        return true;
    }
    
    public void SetText(string text)
    {
        _text = text;
    }
}