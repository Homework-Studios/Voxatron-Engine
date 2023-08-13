namespace Voxatron_Engine_v2.ProjectCreator.FileGenerator;

public class FolderGenerator
{
    public static void GeneratePathTo(Project project)
    {
        // create the path to the project if it doesn't exist
        if (!Directory.Exists(project.ProjectPath))
        {
            Directory.CreateDirectory(project.ProjectPath);
        }
        
        // in there create the folder scene, assets, scripts, and models
        if (!Directory.Exists(project.ProjectPath + "\\scene"))
        {
            Directory.CreateDirectory(project.ProjectPath + "\\scene");
        }
        
        if (!Directory.Exists(project.ProjectPath + "\\assets"))
        {
            Directory.CreateDirectory(project.ProjectPath + "\\assets");
        }
        
        if (!Directory.Exists(project.ProjectPath + "\\scripts"))
        {
            Directory.CreateDirectory(project.ProjectPath + "\\scripts");
        }
        
        if (!Directory.Exists(project.ProjectPath + "\\models"))
        {
            Directory.CreateDirectory(project.ProjectPath + "\\models");
        }
    }
}