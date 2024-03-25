using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreBehavior : Behavior
{
    protected override void Awake()
    {
        behavior = "[Continue]";
        behaviorRegex = @"\b([)?Continue(])?\b";
        base.Awake();
    }

    protected override void ExecuteBehavior(string message)
    {
        return;
    }
}
