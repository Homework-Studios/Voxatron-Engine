namespace Voxatron_Engine.Render;

public class Renderer
{
    private List<Element> Elements = new();
    
    public void Update()
    {
        foreach (Element element in Elements)
        {
            if (!element.Update())
            {
                throw new Exception("Element failed to update. Element: " + element.GetType().Name + "");
            }
        }
    }
    
    public void Render()
    {
        foreach (Element element in Elements)
        {
            if (!element.Render())
            {
                throw new Exception("Element failed to render. Element: " + element.GetType().Name + "");
            }
        }
    }
    
    public void Add(Element element)
    {
        Elements.Add(element);
    }
    
    public void Remove(Element element)
    {
        Elements.Remove(element);
    }
    
    public void Clear()
    {
        Elements.Clear();
    }
}