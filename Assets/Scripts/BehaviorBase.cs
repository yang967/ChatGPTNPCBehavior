using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviorBase : MonoBehaviour
{
    string behavior;
    string behaviorRegex;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<NPCControl>().AddDelegate(behavior, behaviorRegex, ExecuteBehavior);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected abstract void ExecuteBehavior();

}
