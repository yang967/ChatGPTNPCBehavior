using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI_API;
using OpenAI_API.Chat;
using System;
using OpenAI_API.Models;

public class OpenAIController : MonoBehaviour
{
    private OpenAIAPI api;
    private List<ChatMessage> messages;

    private static OpenAIController instance;

    private void Awake()
    {
        instance = this;
        api = new OpenAIAPI(Environment.GetEnvironmentVariable("OPENAI_API_KEY", EnvironmentVariableTarget.Machine));
    }

    // Start is called before the first frame update
    void Start()
    {
        StartPrompt();
    }

    public static OpenAIController GetInstance()
    {
        return instance;
    }

    public void Message(string message)
    {
        GetResponse(message);
    }

    private async void StartPrompt()
    {
        string prompt = GameManager.GetInstance().GenerateStartPrompt();

        messages = new List<ChatMessage> {
            new ChatMessage(ChatMessageRole.System, GameManager.GetInstance().GenerateStartPrompt())
        };

        Debug.Log("System: " + prompt);

        var chatResult = await api.Chat.CreateChatCompletionAsync(new ChatRequest()
        {
            Model = Model.ChatGPTTurbo,
            Temperature = 0.1,
            MaxTokens = 50,
            Messages = messages
        });

        ChatMessage responseMessage = new ChatMessage();
        responseMessage.Role = chatResult.Choices[0].Message.Role;
        responseMessage.Content = chatResult.Choices[0].Message.Content;

        messages.Add(responseMessage);

        Debug.Log("ChatGPT: " + responseMessage.Content);

        GameManager.GetInstance().ProcessMessage(responseMessage.Content);
    }

    public async void StartPrompt(GameObject obj, List<ChatMessage> NPCMessage)
    {
        string prompt = GameManager.GetInstance().GenerateStartPrompt();

        NPCMessage = new List<ChatMessage> {
            new ChatMessage(ChatMessageRole.System, GameManager.GetInstance().GenerateStartPrompt())
        };

        Debug.Log("System: " + prompt);

        var chatResult = await api.Chat.CreateChatCompletionAsync(new ChatRequest() {
            Model = Model.ChatGPTTurbo,
            Temperature = 0.1,
            MaxTokens = 50,
            Messages = NPCMessage
        });

        ChatMessage responseMessage = new ChatMessage();
        responseMessage.Role = chatResult.Choices[0].Message.Role;
        responseMessage.Content = chatResult.Choices[0].Message.Content;

        NPCMessage.Add(responseMessage);

        Debug.Log("ChatGPT: " + responseMessage.Content);

        GameManager.GetInstance().ProcessMessage(obj, responseMessage.Content);
    }

    public async void GetResponse(GameObject obj, List<ChatMessage> NPCMessage, string UserMessage)
    {
        ChatMessage userMessage = new ChatMessage();
        userMessage.Role = ChatMessageRole.User;
        userMessage.Content = UserMessage;

        Debug.Log("System: " + UserMessage);

        NPCMessage.Add(userMessage);

        var chatResult = await api.Chat.CreateChatCompletionAsync(new ChatRequest() {
            Model = Model.ChatGPTTurbo,
            Temperature = 0.1,
            MaxTokens = 50,
            Messages = NPCMessage
        });

        ChatMessage responseMessage = new ChatMessage();
        responseMessage.Role = chatResult.Choices[0].Message.Role;
        responseMessage.Content = chatResult.Choices[0].Message.Content;

        NPCMessage.Add(responseMessage);

        Debug.Log("ChatGPT: " + responseMessage.Content);


        GameManager.GetInstance().ProcessMessage(obj, responseMessage.Content);
    }

    private async void GetResponse(string UserMessage)
    {
        ChatMessage userMessage = new ChatMessage();
        userMessage.Role = ChatMessageRole.User;
        userMessage.Content = UserMessage;

        Debug.Log("System: " + UserMessage);

        messages.Add(userMessage);

        var chatResult = await api.Chat.CreateChatCompletionAsync(new ChatRequest()
        {
            Model = Model.ChatGPTTurbo,
            Temperature = 0.1,
            MaxTokens = 50,
            Messages = messages
        });

        ChatMessage responseMessage = new ChatMessage();
        responseMessage.Role = chatResult.Choices[0].Message.Role;
        responseMessage.Content = chatResult.Choices[0].Message.Content;

        messages.Add(responseMessage);

        Debug.Log("ChatGPT: " +  responseMessage.Content);


        GameManager.GetInstance().ProcessMessage(responseMessage.Content);
    }
}
