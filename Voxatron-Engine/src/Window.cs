using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Voxatron_Engine;

public class Window
{
    public void Create()
    {
        InitWindow(GetScreenWidth(), GetScreenHeight(), "Unknown Title");
        ToggleFullscreen();
        SetTargetFPS(60);
    }

    public void Run()
    {
        while (!WindowShouldClose())
        {
            BeginDrawing();
            ClearBackground(Color.BLACK);
            
            
            
            EndDrawing();
        }
    }
    
    public void Destroy()
    {
        CloseWindow();
    }
}