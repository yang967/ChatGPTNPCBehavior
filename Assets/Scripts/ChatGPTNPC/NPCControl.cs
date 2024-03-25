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

    int SummarizeCounter;
    [SerializeField]
    int SummarizePerPrompts = 10;

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
        SummarizeCounter = 0;
    }

    // Start is called before the first frame update
    async void Start()
    {
        string prompt = GenerateStartPrompt();

        await OpenAIController.GetInstance().GetResponse(gameObject, messages, prompt, 0);

        string result = messages[messages.Count - 1].Content;
        ProcessMessage(result);
    }

    public bool ProcessMessage(string message)
    {
        for(int i = 0; i < behaviorRegex.Count; i++)
        {
            string regex = behaviorRegex[i];
            if(Regex.Match(message, regex, RegexOptions.IgnoreCase).Success)
            {
                //Debug.Log(regex);
                Behaviors[regex](message);
                return true;
            }
        }

        //Debug.Log("Match non");

        Message("Error! You can only reply behavior in their own format! " + BehaviorString);
        return false;
    }

    public async void Message(string message)
    {
        await OpenAIController.GetInstance().GetResponse(gameObject, messages, message);
        string response = messages[messages.Count - 1].Content;
        bool Success = ProcessMessage(response);
        /*if(Success)
        {
            SummarizeCounter++;
        }*/
        
        if(SummarizeCounter >= SummarizePerPrompts)
        {
            await OpenAIController.GetInstance().GetResponse(gameObject, messages, "Summarize all previous prompts except the first one in natural language as short as possible.");
            ChatMessage startPrompt = messages[0];
            startPrompt.Content += "Previous Action Summary: " + messages[messages.Count - 1].Content;
            //ChatMessage Summarized = messages[messages.Count - 1];
            //Summarized.Content = "Previous Action Summary: " + Summarized.Content;
            messages.Clear();
            messages.Add(startPrompt);
            //Debug.Log(messages[0].Content);
            //messages.Add(Summarized);
            SummarizeCounter = 0;
        }
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
            Sections.Add(new KeyValuePair<int, Section>(s.Priority, s));
        }

        Sections.Sort((a, b) => b.Key.CompareTo(a.Key));

        foreach(var section in Sections)
        {
            result += section.Value.CollectSection() + " ";
        }

        result += "You can only reply 1 behavior at a time in their own format. You cannot reply anything other than the behaviors unless you are asked to. You are currently at Home. ";

        return result;
    }
}

enum StartPromptSection {
    Start,
    Behavior,
    BehaviorRule,
    CurrentStatus
}