using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Section : MonoBehaviour
{
    [SerializeField]
    protected int Priority = 0;

    public abstract string CollectSection();

    public int GetPriority()
    {
        return Priority;
    }
}
