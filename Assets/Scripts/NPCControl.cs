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

    private List<ChatMessage> messages;

    private void Awake()
    {
        messages = new List<ChatMessage>();
    }

    // Start is called before the first frame update
    void Start()
    {
        string prompt = GameManager.GetInstance().GenerateStartPrompt(Gender, GenderDescription, Name, Characteristic, Description);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
