using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Section
{
    int Priority { get; set; }

    string CollectSection();
}
