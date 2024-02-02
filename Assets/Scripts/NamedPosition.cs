using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamedPosition : MonoBehaviour
{
    private void Awake()
    {
        GameManager.GetInstance().AddPosition(name, transform.position);
    }
}
