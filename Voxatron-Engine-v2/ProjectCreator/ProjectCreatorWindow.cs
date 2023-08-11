using System.Numerics;
using ImGuiNET;
using Raylib_cs;
using rlImGui_cs;
using NativeFileDialogSharp;

using static Raylib_cs.Raylib;

namespace Voxatron_Engine_v2.ProjectCreator;

public class ProjectCreatorWindow : IWindow
{
    public bool IsOpen { get; set; }

    public string WindowTitle = "Voxatron Engine - Project Creator";
    public ImGuiWindowFlags WindowFlags = ImGuiWindowFlags.NoDocking | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoBringToFrontOnFocus | ImGuiWindowFlags.NoNavFocus;

    public enum UiView { Main, NewProject, LoadProject }
    public UiView CurrentUiView = UiView.Main;
    
    public string[] recentProjects =
    {
        "Space Shooter",
        "Voxatron TD",
        "GTA 7",
        "Destiny -1",
        "World of Warcraft: Reallife",
        "Minecraft 2",
        "Teardown: The Movie",
        "Forza Horizon: Just Stop Already"
    };
    
    public void Open()
    {
        IsOpen = true;
        
        SetConfigFlags(ConfigFlags.FLAG_VSYNC_HINT);
        SetConfigFlags(ConfigFlags.FLAG_WINDOW_UNDECORATED);
        SetConfigFlags(ConfigFlags.FLAG_WINDOW_TRANSPARENT);
        InitWindow(720, 470, WindowTitle);
        
        rlImGui.Setup();
        ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 5);
        ImGui.PushStyleVar(ImGuiStyleVar.FrameRounding, 5);
        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(12, 12));
        Run();
    }

    public void MainUiView()
    {
        // Create new project button
        if (ImGui.Button("Create new project"))
        {
            CurrentUiView = UiView.NewProject;
        }
            
        ImGui.SameLine();
            
        // Load project button
        if (ImGui.Button("Load project"))
        {
            CurrentUiView = UiView.LoadProject;
        }
            
        // Clickable recent projects
        ImGui.Text("Recent projects:");
            
        // List of recent projects
        ImGui.BeginChild("Recent projects", new Vector2(0, 0), true);
        foreach (string recentProject in recentProjects)
        {
            if (ImGui.Selectable(recentProject))
            {
                    
            }
        }
        ImGui.EndChild();
    }
    
    public void ReturnToMainButton()
    {
        if (ImGui.Button("Return to Main Menu"))
        {
            CurrentUiView = UiView.Main;
        }
    }
    
    public string name = "";
    public string author = "";
    public string location = "";
    
    public void NewProjectUiView()
    {
        ReturnToMainButton();

        // Input name
        // Input author
        // input location
        
        ImGui.InputText("Name", ref name, 100);
        ImGui.InputText("Author", ref author, 100);
        ImGui.InputText("Location", ref location, 100);
        ImGui.SameLine();
        if (ImGui.Button("Choose"))
        {
            // Open file dialog
            var result = Dialog.FolderPicker();
        }
        
        // Create project button
        if (ImGui.Button("Create project"))
        {
            // Create project
            // Open project
            // Close window
            
        }
    }
    
    public string loadLocation = "";
    
    public void LoadProjectUiView()
    {
        ReturnToMainButton();
        
        // Input location
        ImGui.InputText("Location", ref loadLocation, 100);
        
        // Load project button
        if (ImGui.Button("Load project"))
        {
            // Open project
            // Close window
        }
    }

    public void UI()
    {
        rlImGui.Begin();
        
        // fullscreen
        ImGui.SetNextWindowPos(new Vector2(0, 0));
        ImGui.SetNextWindowSize(new Vector2(GetScreenWidth(), GetScreenHeight()));
        
        bool isOpen = IsOpen;
        
        if (ImGui.Begin(WindowTitle, ref isOpen, WindowFlags))
        {
            switch (CurrentUiView)
            {
                case UiView.Main:
                    MainUiView();
                    break;
                case UiView.NewProject:
                    NewProjectUiView();
                    break;
                case UiView.LoadProject:
                    LoadProjectUiView();
                    break;    
            }
        }
        else
        {
            IsOpen = false;
        }

        IsOpen = isOpen;
        
        rlImGui.End();
    }

    public void Run()
    {
        while (IsOpen)
        {
            BeginDrawing();
            ClearBackground(Color.BLANK);
            
            UI();
            
            EndDrawing();
        }
        
        CloseWindow();
    }

    public void Close()
    {
        IsOpen = false;
    }
}