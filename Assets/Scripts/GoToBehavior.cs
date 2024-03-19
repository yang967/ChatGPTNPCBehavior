using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.AI;

public class GoToBehavior : BehaviorBase
{

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

        Vector3 position = GameManager.GetInstance().GetPosition(message);
        GetComponent<NavMeshAgent>().SetDestination(position);
    }

}
