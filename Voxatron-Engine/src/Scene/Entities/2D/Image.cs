using System.Numerics;
using Raylib_cs;
using Voxatron_Engine.Render;
using Voxatron_Engine.Render.Elements._2D;
using Voxatron_Engine.Tool;

namespace Voxatron_Engine.Scene.Entities._2D;

public class Image : Entity
{
    private ImageElement _element;

    public Image(Vector2 position, string imageLocation, float scale = 1.0f, bool fillScreen = false)
    {
        position = ScreenUtil.ScreenPercent(position);
        Raylib_cs.Image localImage = Raylib.LoadImage(imageLocation);
        
        if (fillScreen)
        {
            // fill the screen without leaving black bars and without stretching the image
            // the image is allowed to overflow the screen border if its needed to fill the screen without having black bars
            float screenRatio = (float)Raylib.GetScreenWidth() / Raylib.GetScreenHeight();
            float imageRatio = (float)localImage.width / localImage.height;
            
            // scale the width to the screen width and accordingly scale the height to match the image ratio
            Raylib.ImageResize(ref localImage, (int)(Raylib.GetScreenWidth() * scale), (int)((Raylib.GetScreenWidth() * scale) / imageRatio));
        }

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