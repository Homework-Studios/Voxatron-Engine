using System.Numerics;
using ImGuiNET;
using Raylib_cs;
using rlImGui_cs;
using static Raylib_cs.Raylib;

// TODO: Make proper initializer!

SetConfigFlags(ConfigFlags.FLAG_WINDOW_RESIZABLE);
InitWindow(1080, 720, "Voxatron Engine v2");
SetTargetFPS(60);

rlImGui.Setup();
ImGui.GetIO().ConfigFlags |= ImGuiConfigFlags.DockingEnable;
ImGui.GetIO().ConfigFlags |= ImGuiConfigFlags.NavEnableGamepad;


ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 5);
ImGui.PushStyleVar(ImGuiStyleVar.FrameRounding, 5);
ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(12, 12));

var windowFlags = ImGuiWindowFlags.NoDocking | ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoBringToFrontOnFocus | ImGuiWindowFlags.NoNavFocus | ImGuiWindowFlags.NoBackground;
var dockspaceFlags = ImGuiDockNodeFlags.PassthruCentralNode;

RenderTexture2D viewport = LoadRenderTexture(1080, 720);

while (!WindowShouldClose())
{
    Vector2 screen = new(GetScreenWidth(), GetScreenHeight());
    
    // Drawing to render texture
    BeginTextureMode(viewport);
    ClearBackground(Color.DARKGRAY);
    
    DrawFPS(10, 10);
    
    EndTextureMode();
    
    BeginDrawing();
    ClearBackground(Color.BLACK);
    rlImGui.Begin();

    ImGui.BeginMainMenuBar();
    ImGui.MenuItem("File");
    ImGui.MenuItem("Edit");
    ImGui.MenuItem("View");
    ImGui.MenuItem("Help");
    ImGui.EndMenuBar();
    
    
    ImGui.SetNextWindowPos(new Vector2(0, 20));
    ImGui.SetNextWindowSize(screen - new Vector2(0, 20));
    ImGui.Begin("DockSpace", windowFlags);
    ImGui.PopStyleVar(4);
    
    ImGui.DockSpace(ImGui.GetID("DockSpace"), new Vector2(0, 0), dockspaceFlags);
    
    ImGui.Begin("Viewport");
    ImGui.PopStyleVar(4);

    rlImGui.ImageRect(viewport.texture, ImGui.GetContentRegionAvail().X, ImGui.GetContentRegionAvail().Y, new Rectangle(0, 0, viewport.texture.width, -viewport.texture.height));
    
    ImGui.End();
    
    ImGui.Begin("Workspace");
    
    ImGui.End();
    
    ImGui.Begin("Console");
    
    ImGui.End();
    
    ImGui.Begin("Properties");
    
    ImGui.End();

    rlImGui.End();
    EndDrawing();
}

rlImGui.Shutdown();