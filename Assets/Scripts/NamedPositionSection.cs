using OpenAI_API.Moderation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NamedPositionSection : Section
{

    public override string CollectSection()
    {
        string result = "There are following locations you can go to (split by comma): ";
        int i = 0;
        Dictionary<string, Vector3> Positions = NamedPosition.GetNamedPosisioned();
        foreach (var position in Positions)
        {
            result += position.Key;
            if (i < Positions.Count - 1)
            {
                result += ", ";
            }
            i++;
        }
        result += ". ";

        return result;
    }
}
