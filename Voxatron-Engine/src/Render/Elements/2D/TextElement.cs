using System.Numerics;
using Raylib_cs;

namespace Voxatron_Engine.Render.Elements._2D;

public class TextElement : Element
{
    
    public string Text;
    public Vector2 Position;
    public Color Color;
    public int FontSize;
    
    public TextElement(string text, Vector2 position, Color color, int fontSize)
    {
        Text = text;
        Position = position;
        Color = color;
        FontSize = fontSize;
    }
    
    public override bool Update()
    {
        return true;
    }

    public override bool Render()
    {
        // the origin in in the middle of the text
        // move the text back by half the width and half of the font size up
        Raylib.DrawText(Text, (int)Position.X - Raylib.MeasureText(Text, FontSize) / 2, (int)Position.Y - FontSize / 2, FontSize, Color);
        return true;
    }
}