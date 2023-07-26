namespace Voxatron_Engine.Render;

public class Renderer
{
    private readonly Dictionary<int, List<Element>> _elements = new();
    private List<List<Element>> _layers = new ();

    public void Update()
    {
        // Clear the existing _layers
        _layers.Clear();

        // Get the maximum key value, assuming that the keys start from 0 and are consecutive
        int maxKey = _elements.Keys.Max();

        // Initialize _layers with empty lists up to the maximum key value
        for (int i = 0; i <= maxKey; i++)
        {
            _layers.Add(new List<Element>());
        }

        // Copy the elements from _elements to the corresponding _layers indices
        foreach (KeyValuePair<int, List<Element>> layer in _elements)
        {
            _layers[layer.Key] = layer.Value;
        }

        foreach (List<Element> layerElements in _layers)
        {
            foreach (Element element in layerElements)
            {
                if (!element.Update())
                {
                    throw new Exception("Element failed to update. Element: " + element.GetType().Name);
                }
            }
        }
    }

    public void Render()
    {
        foreach (List<Element> layerElements in _layers)
        {
            foreach (Element element in layerElements)
            {
                if (!element.Render())
                {
                    throw new Exception("Element failed to render. Element: " + element.GetType().Name);
                }
            }
        }

        // Optionally, you can add rendering logic for layering here.
        // 0 is the bottom layer, 1 is the next layer up, etc.
    }
    
    public void Add(Element element, int layer = 0)
    {
        // check if the layer exists
        if (!_elements.ContainsKey(layer))
        {
            _elements.Add(layer, new List<Element>());
        }
        
        element.Layer = layer;
        _elements[layer].Add(element);
    }
    
    public void Remove(Element element)
    {
        _elements[element.Layer].Remove(element);
    }
    
    public void Clear()
    {
        _elements.Clear();
    }
}