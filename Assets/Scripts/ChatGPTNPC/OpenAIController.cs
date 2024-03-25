using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI_API;
using OpenAI_API.Chat;
using System;
using OpenAI_API.Models;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

public class OpenAIController : MonoBehaviour
{
    private OpenAIAPI api;

    private static OpenAIController instance;

    private void Awake()
    {
        instance = this;
        api = new OpenAIAPI(Environment.GetEnvironmentVariable("OPENAI_API_KEY", EnvironmentVariableTarget.Machine));
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartPrompt();
    }

    public static OpenAIController GetInstance()
    {
        return instance;
    }

    public async Task<ChatMessage> GetResponse(GameObject obj, List<ChatMessage> NPCMessage, string UserMessage, int role = 1)
    {
        ChatMessage userMessage = new ChatMessage();
        if(role == 0)
        {
            userMessage.Role = ChatMessageRole.System;
        }
        else
        {
            userMessage.Role = ChatMessageRole.User;
        }
        userMessage.Content = UserMessage;

        Debug.Log("System: " + UserMessage);

        NPCMessage.Add(userMessage);

        var chatResult = await api.Chat.CreateChatCompletionAsync(new ChatRequest() {
            Model = Model.ChatGPTTurbo,
            Temperature = 1,
            MaxTokens = 50,
            Messages = NPCMessage
        });

        ChatMessage responseMessage = new ChatMessage();
        responseMessage.Role = chatResult.Choices[0].Message.Role;
        responseMessage.Content = chatResult.Choices[0].Message.Content;

        NPCMessage.Add(responseMessage);

        Debug.Log("ChatGPT: " + responseMessage.Content);

        return responseMessage;
    }
}
