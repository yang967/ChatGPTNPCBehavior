using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Notifier : MonoBehaviour
{

    protected void Notify(NPCControl control, string message)
    {
        control.Message(message);
    }
}
