using System.Numerics;
using ImGuiNET;
using Raylib_cs;
using rlImGui_cs;
using Voxatron_Engine_v2.ProjectCreator;
using static Raylib_cs.Raylib;

namespace Voxatron_Engine_v2.Editor;

public class EditorWindow : IWindow
{ 
    public bool IsOpen { get; set; }
    
    public Project ProjectData { get; set; }
    
    public EditorWindow(Project project)
    {
        ProjectData = project;
    }
    
    public void Open()
    {
        return;
        ClearWindowState(ConfigFlags.FLAG_WINDOW_UNDECORATED);
        ClearWindowState(ConfigFlags.FLAG_WINDOW_TRANSPARENT);
        
        InitWindow(1080, 720, "Voxatron Engine - " + ProjectData.ProjectName);
        
        rlImGui.Setup();
        ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 5);
        ImGui.PushStyleVar(ImGuiStyleVar.FrameRounding, 5);
        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(12, 12));
        
        IsOpen = true;
        Run();
    }

    public void Run()
    {
        while (IsOpen && !WindowShouldClose())
        {
            BeginDrawing();
            ClearBackground(Color.BLACK);
            rlImGui.Begin();
            
            
            rlImGui.End();
            EndDrawing();
        }
    }

    public void Close()
    {
        IsOpen = false;
        CloseWindow();
    }
}