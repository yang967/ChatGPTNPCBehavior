using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    [System.NonSerialized]
    private Dictionary<string, Vector3> Positions;

    [SerializeField]
    private GameObject Player;

    private List<string> behaviors;

    private void Awake()
    {
        instance = this;
        Positions = new Dictionary<string, Vector3>();
        behaviors = new List<string>();
        behaviors.Add("go to [position]");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    public List<string> GetPositions()
    {
        List<string> result = new List<string>();

        foreach(var position in Positions) 
        {
            result.Add(position.Key);
        }

        return result;
    }

    public Vector3 GetPosition(string key)
    {
        if(Positions.ContainsKey(key)) {
            return Positions[key];
        }
        return Vector3.zero;
    }

    public void AddPosition(string key, Vector3 value)
    {
        if (Positions.ContainsKey(key)) {
            Positions[key] = value;
        }
        else {
            Positions.Add(key, value);
        }
    }

    public string GenerateStartPrompt()
    {
        string result = "You need to play as an NPC of a game. You have following behaviors(split by comma): ";

        for(int i = 0; i < behaviors.Count; i++) {
            result += behaviors[i];
            if(i < behaviors.Count - 1) {
                result += ", ";
            }
        }

        result += ". ";

        if(Positions.Count > 0) {
            result += "There are following locations you can go to: ";
            int i = 0;
            foreach(var position in Positions) {
                result += position.Key;
                if(i < Positions.Count - 1) {
                    result += ", ";
                }
                i++;
            }
        }

        result += ". You can only reply behavior in their own format. You are currently at Home";

        return result;
    }

    public void AddBehavior(string behavior)
    {
        behaviors.Add(behavior);
    }

    public List<string> GetBehaviors()
    {
        return behaviors;
    }

    public void PlayerToPosition(string position)
    {   
        Player.GetComponent<NavMeshAgent>().SetDestination(Positions[position]);
    }
}
