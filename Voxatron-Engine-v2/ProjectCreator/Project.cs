namespace Voxatron_Engine_v2.ProjectCreator;

public struct Project
{
    public string ProjectName;
    public string ProjectPath;

    public static Project Load(string path)
    {
        return new Project
        {
            ProjectName = path.Split('\\')[path.Split('\\').Length - 1],
            ProjectPath = path
        };
    }
}