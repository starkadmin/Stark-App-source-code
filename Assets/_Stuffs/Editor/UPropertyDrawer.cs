using System;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(UPropertyAttribute))]
public class UPropertyDrawer : DecoratorDrawer
{
    UPropertyAttribute uproperty => (UPropertyAttribute)attribute;
    
    public override float GetHeight()
    {
        if(uproperty.style == UPropertyAttribute.EPropertyStyle.Header)
        {
            return base.GetHeight() + uproperty.size;
        }
        else
        {
            return uproperty.size + 5;
        }
    }

    public override void OnGUI(Rect position)
    {
        var targetWidth = EditorGUIUtility.currentViewWidth;
        var lineY = position.y + (uproperty.size / 2);
        var myStyle = new GUIStyle();
        myStyle.richText = true;
        var oldGUIColor = GUI.color;

        switch (uproperty.style)
        {
            case UPropertyAttribute.EPropertyStyle.Header:
                EditorGUI.LabelField(position, "<b><color=#ffffffff><size=" + uproperty.size + ">" + uproperty.text.ToUpper() + "</size></color></b>", myStyle);
                GUI.color = Color.white;
                EditorGUI.DrawPreviewTexture(new Rect(0, lineY + 15, targetWidth, 1), Texture2D.whiteTexture);
                GUI.color = oldGUIColor;
                break;
            case UPropertyAttribute.EPropertyStyle.Label:
                EditorGUI.LabelField(position, "<b><color=#add8e6ff><size=" + uproperty.size + ">" + uproperty.text.ToUpper() + "</size></color></b>", myStyle);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}