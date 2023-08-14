namespace Voxatron_Engine_v2.ProjectCreator.FileGenerator;

public class VXCodeGenerator
{
    public static string GenerateDefaultGameManager(Project project)
    {
        return "print(\"Welcome to your game-project " + project.ProjectName + "!\");";
    }
}