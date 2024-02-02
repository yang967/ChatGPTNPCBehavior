using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : MonoBehaviour
{
    public void GotoPosition(string pos)
    {
        GameManager.GetInstance().PlayerToPosition(pos);
    }
}
