using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviorBase : MonoBehaviour
{
    protected string behavior;
    protected string behaviorRegex;
    protected NPCControl control;

    protected virtual void Awake()
    {
        control = GetComponent<NPCControl>();
        GetComponent<NPCControl>().AddDelegate(behavior, behaviorRegex, ExecuteBehavior);
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
    }

    protected abstract void ExecuteBehavior(string message);

}