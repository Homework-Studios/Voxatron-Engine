using System;
using System.Numerics;
using Raylib_cs;
using Voxatron_Engine.Render;
using Voxatron_Engine.Render.Elements._2D;

namespace Voxatron_Engine.Scene.Entitys._2D;

public class Button : Entity
{
    public Vector2 Position;
    public Vector2 Size;
    public float PressedShrink = 0.95f;
    public Color Color;
    public Color HoverColor;
    public Color TextColor;
    public string Text;

    public BoxElement OutlineElement;
    public TextElement TextElement;

    public event Action? ButtonClicked = null;

    public Button(Vector2 position, Vector2 size, Color color, Color hoverColor, Color textColor, string text)
    {
        Position = position;
        
        // the * 2 is because the size is the half size and we have to double it to get the full size
        Size = size * 2;
        
        Color = color;
        HoverColor = hoverColor;
        TextColor = textColor;
        Text = text;

        // the position is the origin of the button and it is in the middle of the button and the button scales around it
        OutlineElement = new BoxElement(Position - Size / 2, Size, Color);
        TextElement = new TextElement(Text, Position, TextColor, 27);
    }

    public override bool Init(Renderer renderer)
    {
        renderer.Add(OutlineElement);
        renderer.Add(TextElement);
        return true;
    }

    public override bool Update()
    {
        // check if the mouse is over the button
        bool isHovering = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(Position.X - Size.X / 2, Position.Y - Size.Y / 2, Size.X, Size.Y));

        // if the left mouse button is pressed
        bool isClicked = isHovering && Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON);
        bool isReleased = isHovering && Raylib.IsMouseButtonReleased(MouseButton.MOUSE_LEFT_BUTTON);

        if (isHovering)
        {
            OutlineElement.Color = HoverColor;
        }
        else
        {
            OutlineElement.Color = Color;
        }

        if (isClicked)
        {
            // shrink the button
            OutlineElement.Position = Position - Size / 2 * PressedShrink;
            OutlineElement.Size = Size * PressedShrink;
        }
        else
        {
            // reset the button
            OutlineElement.Position = Position - Size / 2;
            OutlineElement.Size = Size;
        }

        if (isReleased)
        {
            // Run the event when the mouse is released over the button
            ButtonClicked?.Invoke();
        }

        return true;
    }
}
