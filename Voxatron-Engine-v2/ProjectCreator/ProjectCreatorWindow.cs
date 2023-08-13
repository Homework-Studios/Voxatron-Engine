using System.IO.Pipes;
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
    
    public string[] recentProjects = {};
    
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
                Console.WriteLine("Selected " + recentProject);
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
    
    public string projectName = "";
    public string projectLocation = "";
    public bool validPath = false;
    
    public void NewProjectUiView()
    {
        ReturnToMainButton();

        // Input name
        // input location
        
        ImGui.Spacing();
        if (ImGui.InputText("Project Name", ref projectName, 100))
        {
            // change the folder location at C:\Users\{user}\Documents\Voxatron-Engine\Projects\{projectName}
            projectLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Voxatron-Engine\\Projects\\" + projectName;
            
            if(projectName == "")
                projectLocation = "";
        }

        ImGui.Spacing();
        ImGui.InputText("Project Location", ref projectLocation, 100);
        
        ImGui.SameLine();
        if (ImGui.Button("Choose"))
        {
            // Open file dialog
            var result = Dialog.FolderPicker();
            
            // Set location to result
            projectLocation = result.Path;
            // Set the Name to the last folder in the path
            projectName = result.Path.Split('\\')[result.Path.Split('\\').Length - 1];
        }
        
        ImGui.Spacing();
        
        // check if the path is valid if not add a error message
        if (projectLocation == "")
        {
            // red
            ImGui.PushStyleColor(ImGuiCol.Text, new Vector4(1, 0, 0, 1));
            ImGui.Text("Please choose a location for your project");
            validPath = false;
        }
        else if (Directory.Exists(projectLocation))
        {
            // check if the folder is empty
            if (Directory.GetFiles(projectLocation).Length != 0)
            {
                // red
                ImGui.PushStyleColor(ImGuiCol.Text, new Vector4(1, 0, 0, 1));
                ImGui.Text("The folder is not empty");
                validPath = false;
            }
            else
            {
                // set the project name to the name of the last folder in the path
                projectName = projectLocation.Split('\\')[projectLocation.Split('\\').Length - 1];
                validPath = true;
            }
        }
        else
        {
            validPath = true;
        }
        
        ImGui.PopStyleColor();
        
        ImGui.Spacing();
        
        // Create project button
        if (validPath)
        {
            if (ImGui.Button("Create project"))
            {
                FileGenerator.FileGenerator.GenerateProjectDefault(new Project { ProjectName = projectName, ProjectPath = projectLocation });
            }
        }
    }
    
    public string LoadLocation = "";
    public bool ValidLoadPath = false;

    public bool TestValidLoadPath()
    {
        if (Directory.Exists(LoadLocation) && Directory.GetFiles(LoadLocation, "*.voxatron.yml").Length != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public void LoadProjectUiView()
    {
        ReturnToMainButton();
        
        // Input location
        if (ImGui.InputText("Location", ref LoadLocation, 100))
        {
            // check if the path contains a *.voxatron.yml file
            // star means it can be anything
            ValidLoadPath = TestValidLoadPath();
        }
        
        // Choose button
        ImGui.SameLine();
        if (ImGui.Button("Choose"))
        {
            // Open file dialog
            var result = Dialog.FolderPicker();
            
            // Set location to result
            LoadLocation = result.Path;
            
            // check if the path contains a *.voxatron.yml file
            // star means it can be anything
            ValidLoadPath = TestValidLoadPath();
        }
        
        ImGui.Spacing();
        // check if the path is valid if not add a error message
        if (LoadLocation == "")
        {
            // red
            ImGui.PushStyleColor(ImGuiCol.Text, new Vector4(1, 0, 0, 1));
            ImGui.Text("Please choose a location for your project");
            ImGui.PopStyleColor();
            ValidLoadPath = false;
        }
        else if (Directory.Exists(LoadLocation))
        {
            // check if the folder is empty
            if (Directory.GetFiles(LoadLocation).Length == 0)
            {
                // red
                ImGui.PushStyleColor(ImGuiCol.Text, new Vector4(1, 0, 0, 1));
                ImGui.Text("The folder is empty");
                ImGui.PopStyleColor();
                ValidLoadPath = false;
            }
            else
            {
                // check if the path contains a *.voxatron.yml file
                // star means it can be anything
                ValidLoadPath = TestValidLoadPath();
            }
        }
        else
        {
            ValidLoadPath = false;
        }

        // Load project button
        if (ValidLoadPath)
        {
            if (ImGui.Button("Load project"))
            {
                // Open project
                // Close window
                // Open editor
            }
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