using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.AI;

public class GoToBehavior : BehaviorBase, Section
{
    public int Priority { get; set; }

    public string CollectSection()
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

    protected override void Awake()
    {
        behavior = "go to [position]";
        behaviorRegex = @"\bgo to";
        base.Awake();
    }

    protected override void ExecuteBehavior(string message)
    {
        Regex regex = new Regex(Regex.Escape("go to "));
        message = regex.Replace(message, "", 1);
        regex = new Regex(Regex.Escape("Go to "));
        message = regex.Replace(message, "", 1);
        if (message[message.Length - 1] == '.')
            message = message.Substring(0, message.Length - 1);

        Vector3 position = GameManager.GetInstance().GetPosition(message);
        GetComponent<NavMeshAgent>().SetDestination(position);
    }

}
