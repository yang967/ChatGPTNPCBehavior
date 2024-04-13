using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class PersonalityStartPrompt : MonoBehaviour, OverrideSection
{
    [SerializeField]
    string Name;

    [SerializeField]
    string Gender;

    [SerializeField]
    string GenderDescription;

    [SerializeField]
    string Personality;

    StartPromptSection OverrideStartPromptsection = StartPromptSection.Start;

    public StartPromptSection OverrideStartPromptSection { get => OverrideStartPromptsection; set => OverrideStartPromptsection = value; }

    public string CollectOverrideSection()
    {
        string result = "You need to play as an NPC of a game. ";

        if (name.Length > 0)
        {
            result += "Your name is " + Name + ". ";
        }

        if (Gender.Length > 0)
        {
            result += "Your gender is " + Gender;
            if (GenderDescription.Length != 0)
            {
                result += "(" + GenderDescription + "). ";
            }
            else
            {
                result += ". ";
            }
        }

        result += Personality;

        return result;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
