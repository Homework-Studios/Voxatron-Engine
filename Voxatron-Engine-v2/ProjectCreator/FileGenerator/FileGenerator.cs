namespace Voxatron_Engine_v2.ProjectCreator.FileGenerator;

public class FileGenerator
{
    public static void GenerateProjectDefault(Project project)
    {
        FolderGenerator.GeneratePathTo(project);
        GenerateFiles(project);
    }

    public static void GenerateFiles(Project project)
    {
        var lowerCaseName = project.ProjectName.ToLower();
        
        // create the project file name.voxatron.yml
        File.WriteAllText(project.ProjectPath + "\\" + lowerCaseName + ".voxatron.yml", "");
        
        // create the main scene file
        File.WriteAllText(project.ProjectPath + "\\scene\\" + lowerCaseName + ".voxatron.scene.json", "");
        
        // create the main script file
        File.WriteAllText(project.ProjectPath + "\\scripts\\" + "gameManager.vx", VXCodeGenerator.GenerateDefaultGameManager(project));
    }
}