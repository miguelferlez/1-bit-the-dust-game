using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointData : MonoBehaviour
{
    public static CheckPointData Instance;

    public PlayerHealth health;
    float time;
    int score;
    Dictionary<string, bool> conditionByHash = new Dictionary<string, bool>();

    public void Awake()
    {
        Instance = this;     
    }

    public void SaveTime()
    {
        time = Timer.t;
    }
    public float GetSavedTime()
    {
        return time;
    }

    public void SaveScore()
    {
        score = ScoreManager.currentPoints;
        ScoreManager.Instance.SaveData();
    }
    public void GetSavedScore()
    {
        ScoreManager.Instance.LoadData();
    }

    public void SaveCondition(string hash, bool condition)
    {
        if(hash == null)
        {
            return;
        }
        if (conditionByHash.ContainsKey(hash))
        {
            conditionByHash[hash] = condition;           
        }
        else
        {
            conditionByHash.Add(hash, condition);
        }
    }
    public bool GetCondition(string hash)
    {
        if (hash == null)
        {
            return false;
        }
        if (conditionByHash.ContainsKey(hash))
        {
            return conditionByHash[hash];
        }
        return false;
    }

    public void SaveHealth()
    {
        health.SaveData();
    }
    public void GetSavedHealth()
    {
        health.LoadData();
    }
   

    public void ClearData()
    {
        time = 0;
        score = 0;
        conditionByHash.Clear();
    }
}
