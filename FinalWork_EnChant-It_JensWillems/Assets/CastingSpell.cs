using System;
using Meta.WitAi;
using Meta.WitAi.Json;
using UnityEngine;


public class CastingSpell : MonoBehaviour
{
    public void SetColor(Transform transform, Color color)
    {
        transform.GetComponent<Renderer>().material.color = color;
    }


    public void UpdateColor(WitResponseNode commandResult)
    {
        string[] colorNames = commandResult.GetAllEntityValues("color:color");
        string[] shapes = commandResult.GetAllEntityValues("shape:shape");
        UpdateColor(colorNames, shapes);
    }

    public void UpdateColor(string[] colorNames, string[] shapes)
    {
        if (shapes.Length != 0 && colorNames.Length != shapes.Length)
        {
            return;
        }
        if (shapes.Length == 0 || shapes[0] == "color")
        {
            UpdateColorAllShapes(colorNames);
            return;
        }

        for (var entity = 0; entity < colorNames.Length; entity++)
        {
            if (!ColorUtility.TryParseHtmlString(colorNames[entity], out var color)) return;

            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                if (String.Equals(shapes[entity], child.name,
                        StringComparison.CurrentCultureIgnoreCase))
                {
                    SetColor(child, color);
                    break;
                }
            }
        }
    }
    public void UpdateColorAllShapes(string[] colorNames)
    {
        var unspecifiedShape = 0;
        for (var entity = 0; entity < colorNames.Length; entity++)
        {
            if (!ColorUtility.TryParseHtmlString(colorNames[entity], out var color)) return;

            var splitLimit = (transform.childCount / colorNames.Length) * (entity + 1);
            while (unspecifiedShape < splitLimit)
            {
                SetColor(transform.GetChild(unspecifiedShape), color);
                unspecifiedShape++;
            }
        }
    }
}
