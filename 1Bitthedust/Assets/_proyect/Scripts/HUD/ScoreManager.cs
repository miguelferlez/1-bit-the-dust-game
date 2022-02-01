using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI pointText;
    public static ScoreManager Instance;
    public static int currentPoints;

    public void Awake()
    {
        Instance = this;
        currentPoints = 0;
    }

    public static void AddScore(int value)
    {
        currentPoints += value;
        RefreshScore();
    }

    public static void RemoveScore(int value)
    {
        currentPoints -= value;
        RefreshScore();
    }

    private static void RefreshScore()
    {
        Instance.pointText.text = currentPoints.ToString();
    }

    public void SaveData()
    {
        ScoreData data = new ScoreData(currentPoints);
        JsonSaver.SaveObject(data);
    }
    public void LoadData()
    {
        ScoreData data = JsonSaver.GetFile<ScoreData>();
        currentPoints = data.currentPoints;
        RefreshScore();
    }
}
