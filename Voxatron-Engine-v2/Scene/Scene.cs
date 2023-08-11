using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Scene;

public class Scene
{
    public static Scene? Current = null;

    public Scene()
    {
        if(Current == null) Current = this;
    }
    
    public static void ChangeScene(Scene scene)
    {
        Current = scene;
    }
    
    public void Update()
    {
        
    }
    
    public void Draw()
    {
        DrawLine(0,0,GetScreenWidth(), GetScreenHeight(), Color.RED);
    }
    
    public static Scene Create() => new Scene();
}