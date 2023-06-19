using System.Numerics;
using Raylib_cs;
using Voxatron_Engine.Render;
using Voxatron_Engine.Render.Elements._2D;
using Voxatron_Engine.Tool;

namespace Voxatron_Engine.Scene.Entities._2D;

public class Button : Entity
{
    private readonly Vector2 _position;
    private readonly Vector2 _size;
    private const float PressedShrink = 0.95f;
    private readonly Color _color;
    private readonly Color _hoverColor;

    private readonly BoxElement _outlineElement;
    private readonly TextElement _textElement;

    public event Action? ButtonClicked;

    public Button(Vector2 position, Vector2 size, Color color, Color hoverColor, Color textColor, string text)
    {
        _position = ScreenUtil.ScreenPercent(position);
        
        // the * 2 is because the size is the half size and we have to double it to get the full size
        _size = size * 2;
        
        _color = color;
        _hoverColor = hoverColor;

        // the position is the origin of the button and it is in the middle of the button and the button scales around it
        _outlineElement = new BoxElement(_position - _size / 2, _size, _color);
        _textElement = new TextElement(text, _position, textColor, 27);
    }

    public override bool Init(Renderer renderer)
    {
        renderer.Add(_outlineElement);
        renderer.Add(_textElement);
        return true;
    }

    public override bool Remove(Renderer renderer)
    {
        renderer.Remove(_outlineElement);
        renderer.Remove(_textElement);
        return true;
    }

    public override bool Update()
    {
        // check if the mouse is over the button
        bool isHovering = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(_position.X - _size.X / 2, _position.Y - _size.Y / 2, _size.X, _size.Y));

        // if the left mouse button is pressed
        bool isClicked = isHovering && Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON);
        bool isReleased = isHovering && Raylib.IsMouseButtonReleased(MouseButton.MOUSE_LEFT_BUTTON);

        if (isHovering)
        {
            _outlineElement.Color = _hoverColor;
        }
        else
        {
            _outlineElement.Color = _color;
        }

        if (isClicked)
        {
            // shrink the button
            _outlineElement.Position = _position - _size / 2 * PressedShrink;
            _outlineElement.Size = _size * PressedShrink;
        }
        else
        {
            // reset the button
            _outlineElement.Position = _position - _size / 2;
            _outlineElement.Size = _size;
        }

        if (isReleased)
        {
            // Run the event when the mouse is released over the button
            ButtonClicked?.Invoke();
        }

        return true;
    }
}
