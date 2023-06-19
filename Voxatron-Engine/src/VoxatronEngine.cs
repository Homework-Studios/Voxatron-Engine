using Raylib_cs;
using Voxatron_Engine.Render;
using static Raylib_cs.Raylib;

namespace Voxatron_Engine;

public class VoxatronEngine
{
    Renderer _renderer;
    private Scene.Scene? _scene = null;

    public VoxatronEngine()
    {
        InitWindow(GetScreenWidth(), GetScreenHeight(), "Unknown Title");
        ToggleFullscreen();
        SetTargetFPS(60);

        _renderer = new();
    }

    
    
    public void Run()
    {
        while (!WindowShouldClose())
        {
            if(_scene == null) continue;
            
            _scene.Update();
            _renderer.Update();
            
            BeginDrawing();
            ClearBackground(Color.BLACK);
            
            _renderer.Render();
            
            EndDrawing();
        }
    }
    
    public void Destroy()
    {
        CloseWindow();
        Environment.Exit(0);
    }
    
    public void AttachScene(Scene.Scene scene)
    {
        _scene = scene;
        _scene.SetEngine(this);
        _scene.SetRenderer(_renderer);
        _scene.Init();
    }
}