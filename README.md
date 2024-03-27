This project is depend on https://github.com/OkGoDoIt/OpenAI-API-dotnet?tab=readme-ov-file 

Project is based on **Unity 2022.2.12f1**

# Description

This Project let ChatGPT to control NPC behavior. It gives ChatGPT a list of behaviors, and let ChatGPT which behavior to perform by the given scenario. This project aims to gives NPC more natural reaction under unpredicted scenario.

# Documentation

## Basic Setup

In a new scene, attach an **OpenAIController** Class to a GameObject to perform communication to ChatGPT. OpenAIController is programmed based on *OpenAI-API-dotnet* by **OkGoDoIt** (link is attached at the start of the file). 

Currently the project will read the OpenAI API key from your **System Environment Variable**. Make sure you add a System Environment Variable named **OPENAI_API_KEY** with a value of your OpenAI API key.

After create an NPC GameObject, attach an **NPCControl** Class to it. NPCControl class handles the behavior processing of the NPC. You can define NPC's name and gender in NPCControl class.

In the *Script Execution Order*, make sure *NPCControl* is running after *OpenAIController*.

Initially, NPC does not contain any behavior. If you want to add a behavior to a certain NPC, you need to attach a class that inherit **Behavior** class. There is a demo behavior class named GoToBehavior.

You can extend the content of ChatGPT NPC using **Behavior**, **Notifier**, and **Section**

### Behavior

If you want to define a new behavior, you can inherit **Behavior** class. There are 3 major fields in the Behavior class.
+ behavior string: this field should contain a natural language that clearly state your behavior, including formatting. For example, go to [position]. ChatGPT will read your behavior string, and reply formatted behavior string when it decide to do this behavior.
+ behavior regex: this field should contain a regular expression that can match the returned behavior from ChatGPT. For the example above, the word position will be replaced by any given position when returned from ChatGPT. Your regular expression should be able to identify the returned behavior even  if part of it is replaced by other words by formatting.
+ ExecuteBehavior: this is a class that you should define how to execute this behavior. When the returned behavior is identified by your regular expression, this method will be called with the parameter of the full returned text. Your method should analyze returned text if necessary, and perform the behavior. For go to [position] example, ExecuteBehavior method should extract position from the returned text, and then map the position word to actual Vector3 variable, and then run a pathfinding algorithm to that Vector3 variable.

**Always define your behavior string and behavior regex before the base awake method**

In the *Script Execution Order*, you should always run *Behavior* after *NPCControl*

### Notifier

When your NPC is running, you should let your NPC know what happens in the world. Notifier is designed to do this job. 

After Inheriting the Notifier class, there would be a protected method named **Notify** in the Notifier class. You can pass the NPCControl class of the NPC that you want to notify, and the message you want to send to that NPC to the Notify method. This class is designed for management issue that may appear in the later development.

### Section

If you want to add extra message to the Start Prompt (First Prompt send to ChatGPT that define ChatGPT instance as an NPC), you can inherit Section interface. There are 2 major fields in Section interface.
+ Priority: this field define the order of all messages from Section classes. The message of the section will appear earlier in the Start Prompt when given a higher Priority value. Since C# does not allow vairable in interface, define Priority as a field is the best approach I could get. You should define a int priority value if you want to make it a SerializeField
+ CollectSection: CollectionSection should return the message that you want to add to the Start Prompt.

GoToBehavior is also a demo Section class. It not only add the go to [position] behavior to the NPC, but also add the position that an NPC can go to in the start prompt. You can use it as a reference when you feel confused.

