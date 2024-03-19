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

    public HashSet<string> BehaviorHash;
    public HashSet<string> BehaviorReplacement;

    List<string> BehaviorList;
    List<string> BehaviorReplacementList;

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

    public void AddBehavior(string NL)
    {
        BehaviorHash.Add(NL);
    }

    public static OpenAIController GetInstance()
    {
        return instance;
    }

    public async void StartPrompt(GameObject obj, List<ChatMessage> NPCMessage, string prompt)
    {
        NPCMessage.Clear();
        NPCMessage.Add(new ChatMessage(ChatMessageRole.System, prompt));

        Debug.Log("System: " + prompt);

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

        obj.GetComponent<NPCControl>().ProcessMessage(responseMessage.Content);

        //GameManager.GetInstance().ProcessMessage(obj, responseMessage.Content);
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

        obj.GetComponent<NPCControl>().ProcessMessage(responseMessage.Content);
        //GameManager.GetInstance().ProcessMessage(obj, responseMessage.Content);
    }
}
