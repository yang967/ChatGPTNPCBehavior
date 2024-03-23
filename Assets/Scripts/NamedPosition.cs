using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamedPosition : Notifier
{
    static Dictionary<string, Vector3> NamedPosisions = new Dictionary<string, Vector3>();

    private bool CanRespond;

    private void Awake()
    {
        CanRespond = false;
        NamedPosisions.Add(name, transform.position);
        GameManager.GetInstance().AddPosition(name, transform.position);
        StartCoroutine(NotRespondTime());
    }

    public static Dictionary<string, Vector3> GetNamedPosisioned()
    {
        return NamedPosisions;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer != 6)
        {
            return;
        }

        if (!CanRespond)
        {
            return;
        }

        if(other.TryGetComponent<NPCControl>(out NPCControl control))
        {
            Notify(control, ("Reached " + name));
        }
    }


    IEnumerator NotRespondTime()
    {
        yield return new WaitForSeconds(1);

        CanRespond = true;
    }
}
