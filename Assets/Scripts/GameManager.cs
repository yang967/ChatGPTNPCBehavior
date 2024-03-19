using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor.VersionControl;
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

    private string BehaviorString;

    private string PositionString;

    private void Awake()
    {
        instance = this;
        Positions = new Dictionary<string, Vector3>();
        behaviors = new List<string>();
        behaviors.Add("go to [position]");
        PositionString = string.Empty;
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
        return GenerateStartPrompt("", "", "", "", "");
        
    }

    public string GenerateStartPrompt(string gender, string genderDescription, string name,  string characteristic, string description) 
    {
        string result = "You need to play as an NPC of a game. ";
        
        if(name.Length > 0) {
            result += "Your name is " + name + ". ";
        }
        
        if(gender.Length > 0) {
            result += "Your gender is " + gender;
            if (genderDescription.Length != 0) {
                result += "(" + genderDescription + "). ";
            }
            else {
                result += ". ";
            }
        }
        

        if(characteristic.Length != 0) {
            result += characteristic + ". ";
        }

        if(description.Length != 0) {
            result += description + ". ";
        }

        BehaviorString = "You have following behaviors (split by comma): ";

        for (int i = 0; i < behaviors.Count; i++) {
            BehaviorString += behaviors[i];
            if (i < behaviors.Count - 1) {
                BehaviorString += ", ";
            }
        }
        BehaviorString += ". ";

        result += BehaviorString;

        if (Positions.Count > 0) {
            PositionString += "There are following locations you can go to: ";
            int i = 0;
            foreach (var position in Positions) {
                PositionString += position.Key;
                if (i < Positions.Count - 1) {
                    PositionString += ", ";
                }
                i++;
            }
            PositionString += ". ";
            result += PositionString;
        }

        result += "You can only reply behavior in their own format. You cannot reply anything other than the behaviors. You are currently at Home. ";

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

    public void ProcessMessage(GameObject obj, string message)
    {
        if(message.Contains("ignore")) {
            return;
        }

        if (Regex.Match(message, @"\bgo to", RegexOptions.IgnoreCase).Success) {
            message = message.Replace("go to ", "");
            message = message.Replace("Go to ", "");

            obj.GetComponent<NavMeshAgent>().destination = Positions[message];
            return;
        }

        foreach (string str in behaviors) {
            if (message.Contains(str)) {

                return;
            }
        }

        obj.GetComponent<NPCControl>().Message("Error! You can only reply behavior in their own format! " + BehaviorString + PositionString);
    }
}
