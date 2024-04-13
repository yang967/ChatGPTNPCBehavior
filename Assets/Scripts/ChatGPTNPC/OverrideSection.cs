using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface OverrideSection
{
    StartPromptSection OverrideStartPromptSection { get; set; }
    string CollectOverrideSection();
}
