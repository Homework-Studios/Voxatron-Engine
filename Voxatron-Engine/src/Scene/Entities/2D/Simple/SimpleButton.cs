using System.Numerics;
using Raylib_cs;

namespace Voxatron_Engine.Scene.Entities._2D.Simple;

public class SimpleButton : Button
{
    public SimpleButton(Vector2 position, string text) 
        : base(position, 
            new(Raylib.MeasureText(text, 20), 20), 
            new Color(255, 0, 0, 255), 
            new Color(255, 150, 150, 255), 
            Color.WHITE,
            text) 
    {
    }
}