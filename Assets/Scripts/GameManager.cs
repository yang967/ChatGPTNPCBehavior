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

    private void Awake()
    {
        instance = this;
        Positions = new Dictionary<string, Vector3>();
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
}
