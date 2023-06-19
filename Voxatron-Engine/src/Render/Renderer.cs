namespace Voxatron_Engine.Render;

public class Renderer
{
    private readonly List<Element> _elements = new();
    
    public void Update()
    {
        foreach (Element element in _elements)
        {
            if (!element.Update())
            {
                throw new Exception("Element failed to update. Element: " + element.GetType().Name + "");
            }
        }
    }
    
    public void Render()
    {
        foreach (Element element in _elements)
        {
            if (!element.Render())
            {
                throw new Exception("Element failed to render. Element: " + element.GetType().Name + "");
            }
        }
    }
    
    public void Add(Element element)
    {
        _elements.Add(element);
    }
    
    public void Remove(Element element)
    {
        _elements.Remove(element);
    }
    
    public void Clear()
    {
        _elements.Clear();
    }
}