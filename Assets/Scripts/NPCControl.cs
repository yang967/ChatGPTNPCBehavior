using OpenAI_API.Chat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCControl : MonoBehaviour
{
    [SerializeField] string Gender;
    [SerializeField] string GenderDescription;

    [SerializeField] string Name;
    [SerializeField] string Characteristic;
    [SerializeField] string Description;

    public delegate void BehaviorDelegate();

    public Dictionary<string, BehaviorDelegate> Behaviors;

    private List<ChatMessage> messages;

    private void Awake()
    {
        messages = new List<ChatMessage>();
        Behaviors = new Dictionary<string, BehaviorDelegate>();
    }

    // Start is called before the first frame update
    void Start()
    {
        string prompt = GameManager.GetInstance().GenerateStartPrompt(Gender, GenderDescription, Name, Characteristic, Description);

        OpenAIController.GetInstance().StartPrompt(gameObject, messages, prompt);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Message(string message)
    {
        OpenAIController.GetInstance().GetResponse(gameObject, messages, message);
    }

    public void AddDelegate(string name, string regex, BehaviorDelegate behavior)
    {
        Behaviors.Add(name, behavior);
    }
}
