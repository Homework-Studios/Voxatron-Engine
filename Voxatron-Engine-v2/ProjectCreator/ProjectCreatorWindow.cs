using System.Diagnostics;
using System.Numerics;
using System.Timers;
using ImGuiNET;
using NativeFileDialogSharp;
using Raylib_cs;
using rlImGui_cs;
using Voxatron_Engine_v2.Editor;
using static Raylib_cs.Raylib;
using Timer = System.Timers.Timer;

namespace Voxatron_Engine_v2.ProjectCreator;

public class ProjectCreatorWindow : IWindow
{
    public enum UiView
    {
        Main,
        NewProject,
        LoadProject
    }

    public UiView CurrentUiView = UiView.Main;

    public string LoadLocation = "";
    public string projectLocation = "";

    public string projectName = "";

    public string?[] recentProjects = { };

    public string recentProjectsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                                       "\\Voxatron-Engine\\Projects\\";

    public bool ValidLoadPath;
    public bool validPath;

    public ImGuiWindowFlags WindowFlags = ImGuiWindowFlags.NoDocking | ImGuiWindowFlags.NoCollapse |
                                          ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove |
                                          ImGuiWindowFlags.NoBringToFrontOnFocus | ImGuiWindowFlags.NoNavFocus;

    public string WindowTitle = "Voxatron Engine - Project Creator";
    public bool IsOpen { get; set; }
    
    private unsafe Image[] _icons = new Image[40];
    private int _index = 0;
    
    public void IconTimerTick(object? sender, ElapsedEventArgs elapsedEventArgs)
    {
        
        SetWindowIcon(_icons[_index]);
        _index++;
        _index %= _icons.Length;
    }
    
    public void Open()
    {
        IsOpen = true;

        SetConfigFlags(ConfigFlags.FLAG_VSYNC_HINT);
        SetConfigFlags(ConfigFlags.FLAG_WINDOW_UNDECORATED);
        SetConfigFlags(ConfigFlags.FLAG_WINDOW_TRANSPARENT);
        InitWindow(720, 470, WindowTitle);
        // file format is 0000.png 0001.png 0002.png ... 0010.png ... 9999.png
        for (int i = 0; i < _icons.Length; i++)
        {
            var a = i*3+1;
            var framecount = a.ToString().PadLeft(4, '0');
            Console.WriteLine(framecount);
            _icons[i]=LoadImage(
                "../../../res/icon/images/"+framecount+".png");
        }

        Timer timer = new(150);timer.Elapsed += IconTimerTick;
        timer.AutoReset = true;
       timer.Enabled = true;

        // load all the folder names in the recent projects folder but just the last folders name should be shown
        recentProjects = Directory.GetDirectories(recentProjectsPath);
        // loop over
        for (var i = 0; i < recentProjects.Length; i++)
            // get the last folder name
            recentProjects[i] = recentProjects[i].Split('\\')[recentProjects[i].Split('\\').Length - 1];

        // loop over them and remove those who do not have a *.voxatron.yml 
        // * could be anything, but it should be the project name
        for (var i = 0; i < recentProjects.Length; i++)
            if (!File.Exists(recentProjectsPath + recentProjects[i] + "\\" + recentProjects[i] + ".voxatron.yml"))
                recentProjects[i] = null;

        rlImGui.Setup();
        ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 5);
        ImGui.PushStyleVar(ImGuiStyleVar.FrameRounding, 5);
        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(12, 12));
        Run();
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

        rlImGui.End();
    }

    public void Close()
    {
        IsOpen = false;
        CloseWindow();
    }

    public void MainUiView()
    {
        // Create new project button
        if (ImGui.Button("Create new project")) CurrentUiView = UiView.NewProject;

        ImGui.SameLine();

        // Load project button
        if (ImGui.Button("Load project")) CurrentUiView = UiView.LoadProject;

        // Clickable recent projects
        ImGui.Text("Recent projects:");

        // List of recent projects
        ImGui.BeginChild("Recent projects", new Vector2(0, 0), true);
        foreach (var recentProject in recentProjects)
        {
            if (recentProject == null) continue;
            if (ImGui.Selectable(recentProject))
            {
                ImGui.EndChild();
                var project = Project.Load(recentProjectsPath + recentProject);
                Close();
                var editorWindow = new EditorWindow(project);
                editorWindow.Open();
                return;
            }
        }

        ImGui.EndChild();
    }

    public void ReturnToMainButton()
    {
        if (ImGui.Button("Return to Main Menu")) CurrentUiView = UiView.Main;
    }

    public void NewProjectUiView()
    {
        ReturnToMainButton();

        // Input name
        // input location

        ImGui.Spacing();
        if (ImGui.InputText("Project Name", ref projectName, 100))
        {
            // change the folder location at C:\Users\{user}\Documents\Voxatron-Engine\Projects\{projectName}
            projectLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                              "\\Voxatron-Engine\\Projects\\" + projectName;

            if (projectName == "")
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
            if (ImGui.Button("Create project"))
            {
                var project = new Project { ProjectName = projectName, ProjectPath = projectLocation };
                FileGenerator.FileGenerator.GenerateProjectDefault(project);
                Close();
                var editorWindow = new EditorWindow(project);
                editorWindow.Open();
            }
    }

    public bool TestValidLoadPath()
    {
        if (Directory.Exists(LoadLocation) && Directory.GetFiles(LoadLocation, "*.voxatron.yml").Length != 0)
            return true;
        return false;
    }

    public void LoadProjectUiView()
    {
        ReturnToMainButton();

        // Input location
        if (ImGui.InputText("Location", ref LoadLocation, 100))
            // check if the path contains a *.voxatron.yml file
            // star means it can be anything
            ValidLoadPath = TestValidLoadPath();

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
            if (ImGui.Button("Load project"))
            {
                // Open project
                // Close window
                // Open editor
                var project = Project.Load(LoadLocation);
                Close();
                var editorWindow = new EditorWindow(project);
                editorWindow.Open();
            }
    }

    public void UI()
    {
        rlImGui.Begin();

        // fullscreen
        ImGui.SetNextWindowPos(new Vector2(0, 0));
        ImGui.SetNextWindowSize(new Vector2(GetScreenWidth(), GetScreenHeight()));

        var isOpen = IsOpen;

        if (ImGui.Begin(WindowTitle, ref isOpen, WindowFlags))
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
        else
            IsOpen = false;

        IsOpen = isOpen;

        rlImGui.End();
    }
}