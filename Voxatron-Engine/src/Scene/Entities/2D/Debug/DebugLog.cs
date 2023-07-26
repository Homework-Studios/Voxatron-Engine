using System.Numerics;
using Raylib_cs;
using Voxatron_Engine.Render;
using Voxatron_Engine.Render.Elements._2D;

namespace Voxatron_Engine.Scene.Entities._2D.Debug;

public class DebugLog : Entity
{
    private const int MaxLines = 20;
    private double _removeAfter = 0.5; // 100ms
    private double _clock = 0;
    
    public static DebugLog? Instance;
    
    private string _log = "";
    TextElement _textElement;
    
    public DebugLog()
    {
        Instance = this;
    }
    
    public override bool Init(Renderer renderer)
    {
        _textElement = new TextElement(_log, new Vector2(200, 50), Color.SKYBLUE, 27, false);
        renderer.Add(_textElement, 1);
        return true;   
    }

    public override bool Remove(Renderer renderer)
    {
        renderer.Remove(_textElement);
        return true;
    }

    public override bool Update()
    {
        // after 100ms, remove the last line
        _clock += Raylib.GetFrameTime();

        if (_clock > _removeAfter)
        {
            // split into lines and then remove the last line
            string[] lines = _log.Split('\n');
            
            // reverse the array
            Array.Reverse(lines);
            
            // remove the first element
            lines = lines.Skip(1).ToArray();
            
            // reverse the array again
            Array.Reverse(lines);
            
            // join the array back together with new lines
            _log = string.Join("\n", lines);
            
            _clock = 0;
            _removeAfter = 0.5;
        }

        _textElement.SetText(_log);
        return true;
    }
    
    public void Log(string message)
    {
        // add the message to the start
        _log = message + "\n" + _log;
        
        // set remove after to 3 seconds
        _removeAfter = 3;
        
        // check if the log is too long
        if (_log.Count(x => x == '\n') > MaxLines)
        {
            // split into lines and then remove the last line
            string[] lines = _log.Split('\n');
            
            // reverse the array
            Array.Reverse(lines);
            
            // remove the first element
            lines = lines.Skip(1).ToArray();
            
            // reverse the array again
            Array.Reverse(lines);
            
            // join the array back together with new lines
            _log = string.Join("\n", lines);
        }
    }
}