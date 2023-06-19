using System.Numerics;
using static Raylib_cs.Raylib;

namespace Voxatron_Engine.Tool;

public class ScreenUtil
{
    public static Vector2 Center(Vector2 vector2)
    {
        return new((float)(GetScreenWidth() * 0.5 - vector2.X / 2), (float)(GetScreenHeight() * 0.5 - vector2.Y / 2));
    }
}