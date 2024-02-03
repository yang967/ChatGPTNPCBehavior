using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamedPosition : MonoBehaviour
{
    private bool CanRespond;

    private void Awake()
    {
        CanRespond = false;
        GameManager.GetInstance().AddPosition(name, transform.position);
        StartCoroutine(NotRespondTime());
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

        OpenAIController.GetInstance().Message("Reached " + name);
    }


    IEnumerator NotRespondTime()
    {
        yield return new WaitForSeconds(1);

        CanRespond = true;
    }
}
