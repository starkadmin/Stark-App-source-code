using UnityEngine;

public class UPropertyAttribute : PropertyAttribute
{
    public enum EPropertyStyle
    {
        Header = 0,
        Label = 1
    }
    
    public readonly float size;
    public readonly string text;
    public readonly EPropertyStyle style;

    public UPropertyAttribute(float size, EPropertyStyle style, string text)
    {
        this.size = size;
        this.style = style;
        this.text = text;
    }
}