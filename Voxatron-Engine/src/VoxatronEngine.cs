using Raylib_cs;
using Voxatron_Engine.Render;
using static Raylib_cs.Raylib;

namespace Voxatron_Engine;

public class VoxatronEngine
{
    private readonly Renderer _renderer;
    private Scene.Scene? _scene;
    private bool _shouldClose;
    
    public Random Random = new();

    public VoxatronEngine(string windowTitle)
    {
        SetConfigFlags(ConfigFlags.FLAG_MSAA_4X_HINT);
        SetConfigFlags(ConfigFlags.FLAG_VSYNC_HINT);
        
        InitWindow(GetScreenWidth(), GetScreenHeight(), windowTitle);
        ToggleFullscreen();
        SetTargetFPS(60);

        _renderer = new Renderer();
    }
    
    public void Run()
    {
        while (!_shouldClose)
        {
            if(_scene == null) continue;
            if(IsKeyPressed(KeyboardKey.KEY_RIGHT_SHIFT)) _shouldClose = true;
            
            _scene.Update();
            _renderer.Update();
            
            BeginDrawing();
            ClearBackground(Color.BLACK);
            
            _renderer.Render();
            
            EndDrawing();
        }
    }
    
    public void Shutdown()
    {
        _shouldClose = true;
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