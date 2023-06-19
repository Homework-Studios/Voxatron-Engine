using System.Numerics;
using Raylib_cs;

namespace Voxatron_Engine.Render.Elements._2D;

public class ImageElement : Element
{
    public Vector2 Position { get; set; }

    private Texture2D _texture;
    
    public ImageElement(Vector2 position, Image image)
    {
        Position = position;
        _texture = Raylib.LoadTextureFromImage(image);
    }
    
    public override bool Update()
    {
        return true;
    }

    public override bool Render()
    {
        Vector2 imageSize = new Vector2(_texture.width, _texture.height);
        Vector2 imagePosition = new Vector2(Position.X - (imageSize.X / 2), Position.Y - (imageSize.Y / 2));
        
        Raylib.DrawTexture(_texture, (int)imagePosition.X, (int)imagePosition.Y, Color.WHITE);
        return true;
    }
}