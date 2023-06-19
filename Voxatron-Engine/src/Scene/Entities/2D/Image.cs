using System.Numerics;
using Raylib_cs;
using Voxatron_Engine.Render;
using Voxatron_Engine.Render.Elements._2D;
using Voxatron_Engine.Tool;

namespace Voxatron_Engine.Scene.Entities._2D;

public class Image : Entity
{
    private ImageElement _element;

    public Image(Vector2 position, string imageLocation, float scale = 1.0f)
    {
        position = ScreenUtil.ScreenPercent(position);
        Raylib_cs.Image localImage = Raylib.LoadImage(imageLocation);

        Raylib.ImageResize(ref localImage, (int)(localImage.width * scale), (int)(localImage.height * scale));
        
        _element = new ImageElement(position, localImage);
        
        Raylib.UnloadImage(localImage);
    }
    
    public override bool Init(Renderer renderer)
    {
        renderer.Add(_element);
        return true;
    }

    public override bool Remove(Renderer renderer)
    {
        renderer.Remove(_element);
        return true;
    }

    public override bool Update()
    {
        
        return true;
    }
}