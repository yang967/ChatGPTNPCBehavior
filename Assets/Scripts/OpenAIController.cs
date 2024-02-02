using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI_API;
using OpenAI_API.Chat;
using System;

public class OpenAIController : MonoBehaviour
{
    private OpenAIAPI api;
    private List<ChatMessage> messages;

    private void Awake()
    {
        api = new OpenAIAPI(Environment.GetEnvironmentVariable("OPENAI_API_KEY"));
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GameManager.GetInstance().GenerateStartPrompt());
    }

    private void StartPrompt()
    {
        messages = new List<ChatMessage> {
            new ChatMessage(ChatMessageRole.System, GameManager.GetInstance().GenerateStartPrompt())
        };
    }
}
