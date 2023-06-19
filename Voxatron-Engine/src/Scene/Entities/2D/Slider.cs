using System.Numerics;
using Raylib_cs;
using Voxatron_Engine.Render;
using Voxatron_Engine.Render.Elements._2D;
using Voxatron_Engine.Tool;

namespace Voxatron_Engine.Scene.Entities._2D;

public class Slider : Entity
{
    private readonly Color _color;
    private readonly Color _hoverColor;
    private readonly Vector2 _position;
    private readonly Vector2 _size;
    private readonly BoxElement _slider;
    private readonly BoxElement _sliderPoint;
    private readonly float _value;


    public Slider(Vector2 position, Vector2 size, Color color, Color hoverColor, float sliderThickness, float value)
    {
        _position = ScreenUtil.ScreenPercent(position);
        _size = size * 2;
        _color = color;
        _hoverColor = hoverColor;
        _value = value;

        var pointSize = new Vector2(_size.X / 5, _size.Y / 3);
        _slider = new BoxElement(_position - size / 2, _size with { Y = sliderThickness }, _color, false);
        _sliderPoint = new BoxElement(_position - size / 2 - pointSize / 3, pointSize, _color);
    }

    public override bool Init(Renderer renderer)
    {
        renderer.Add(_slider);
        renderer.Add(_sliderPoint);
        return true;
    }

    public override bool Remove(Renderer renderer)
    {
        return true;
    }

    public override bool Update()
    {
        return true;
    }
}