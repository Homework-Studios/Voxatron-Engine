using System.Numerics;
using Raylib_cs;

namespace Voxatron_Engine.Scene.Entities._2D.Simple;

public class SizeButton : Button
{
    public SizeButton(Vector2 position, Vector2 size, string text) 
        : base(position, 
            size, 
            new Color(255, 0, 0, 255), 
            new Color(255, 150, 150, 255), 
            Color.WHITE,
            text) 
    {
    }
}