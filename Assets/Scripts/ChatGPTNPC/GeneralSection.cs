using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralSection : MonoBehaviour, Section
{

    [SerializeField]
    protected int priority;
    public int Priority { get { return priority; } set { priority = value; } }
    [SerializeField]
    string PromptContent;

    public string CollectSection()
    {
        return PromptContent;
    }
}
