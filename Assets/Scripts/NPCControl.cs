using OpenAI_API.Chat;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UIElements;

public class NPCControl : MonoBehaviour
{
    [SerializeField] string Gender;
    [SerializeField] string GenderDescription;

    [SerializeField] string Name;
    [SerializeField] string Characteristic;
    [SerializeField] string Description;

    public delegate void BehaviorDelegate(string message);

    List<string> behaviors;
    List<string> behaviorRegex;

    string BehaviorString;

    public Dictionary<string, BehaviorDelegate> Behaviors;

    private List<ChatMessage> messages;

    private void Awake()
    {
        messages = new List<ChatMessage>();
        Behaviors = new Dictionary<string, BehaviorDelegate>();
        behaviors = new List<string>();
        behaviorRegex = new List<string>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //string prompt = GameManager.GetInstance().GenerateStartPrompt(Gender, GenderDescription, Name, Characteristic, Description);

        string prompt = GenerateStartPrompt();

        OpenAIController.GetInstance().StartPrompt(gameObject, messages, prompt);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProcessMessage(string message)
    {
        for(int i = 0; i < behaviorRegex.Count; i++)
        {
            string regex = behaviorRegex[i];
            if(Regex.Match(message, regex, RegexOptions.IgnoreCase).Success)
            {
                Behaviors[regex](message);
                return;
            }
        }

        Message("Error! You can only reply behavior in their own format! " + BehaviorString);
    }

    public void Message(string message)
    {
        OpenAIController.GetInstance().GetResponse(gameObject, messages, message);
        string response = messages[messages.Count - 1].Content;
    }

    public void AddDelegate(string name, string regex, BehaviorDelegate behavior)
    {
        Behaviors.Add(regex, behavior);
        behaviors.Add(name);
        behaviorRegex.Add(regex);
    }

    public string GenerateStartPrompt()
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


        if (Characteristic.Length != 0)
        {
            result += Characteristic + " ";
        }

        if (Description.Length != 0)
        {
            result += Description + " ";
        }

        BehaviorString = "You have following behaviors (split by comma): ";

        for (int i = 0; i < behaviors.Count; i++)
        {
            BehaviorString += behaviors[i];
            if (i < behaviors.Count - 1)
            {
                BehaviorString += ", ";
            }
        }
        BehaviorString += ". ";
        result += BehaviorString;

        List<KeyValuePair<int, Section>> Sections = new List<KeyValuePair<int, Section>>();

        foreach(Section s in GetComponents<Section>())
        {
            Sections.Add(new KeyValuePair<int, Section>(s.GetPriority(), s));
        }

        Sections.Sort((a, b) => b.Key.CompareTo(a.Key));

        foreach(var section in Sections)
        {
            result += section.Value.CollectSection() + " ";
        }

        result += "You can only reply behavior in their own format. You cannot reply anything other than the behaviors. You are currently at Home. ";

        return result;
    }
}