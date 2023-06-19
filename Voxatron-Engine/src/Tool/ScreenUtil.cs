using System.Numerics;
using static Raylib_cs.Raylib;

namespace Voxatron_Engine.Tool;

public class ScreenUtil
{
    public static Vector2 Center(Vector2 vector2)
    {
        return new((float)(GetScreenWidth() * 0.5 - vector2.X / 2), (float)(GetScreenHeight() * 0.5 - vector2.Y / 2));
    }
    
    // x is from 0 to 100 and y is from 0 to 100
    public static Vector2 ScreenPercent(Vector2 vector2)
    {
        return new(GetScreenWidth() * vector2.X / 100, GetScreenHeight() * vector2.Y / 100);
    }
}