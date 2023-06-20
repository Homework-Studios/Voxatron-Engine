using Raylib_cs;

namespace Voxatron_Engine.Render.Elements._2D.Debug;

public class PercentageGridElement : Element
{
    public override bool Update()
    {
        return true;
    }

    public override bool Render()
    {
        int oneWidth = (int)(Raylib.GetScreenWidth() * 0.01f);
        int oneHeight = (int)(Raylib.GetScreenHeight() * 0.01f);
        
        for (int i = 0; i < Raylib.GetScreenWidth(); i += oneWidth)
        {
            Raylib.DrawLine(i, 0, i, Raylib.GetScreenHeight(), new (255, 0, 0, 50));
        }
        
        for (int i = 0; i < Raylib.GetScreenHeight(); i += oneHeight)
        {
            Raylib.DrawLine(0, i, Raylib.GetScreenWidth(), i, new (255, 0, 0, 50));
        }
        return true;
    }
}