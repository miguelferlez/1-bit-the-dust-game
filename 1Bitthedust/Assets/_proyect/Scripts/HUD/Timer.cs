using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum DifficultyLevel : int
{
   NONE = 0, EASY = 1, MEDIUM = 2, HARD = 3
}

public class Timer : MonoBehaviour
{
    public static Timer Instance;
    public DifficultyLevel currentDifficulty;

    TextMeshProUGUI timeText;
    float startTime;


    public float easyMin;
    public float easySec;
    public Color easyColor;

    [UnityEngine.Serialization.FormerlySerializedAs("normalTime")]
    public float mediumMin;
    public float mediumSec;
    public Color mediumColor;


    public float hardMin;
    public float hardSec;
    public Color hardColor;

    public static float t;
    public static float resetTime = 0f;
    float hour;
    float min;
    float sec;

    private Coroutine mediumCor;
    private Coroutine hardCor;


    public void LoadTime()
    {
        if (CheckPointData.Instance.GetCondition("MustLoadTime"))
        {
            t = CheckPointData.Instance.GetSavedTime();
            CheckPointData.Instance.SaveCondition("MustLoadTime", false);
        }
    }

    public void Awake()
    {
        timeText = GetComponent<TextMeshProUGUI>();
        currentDifficulty = DifficultyLevel.EASY;
        t = resetTime;
        timeText.color = easyColor;

        Instance = this;
    }

    public void Update()
    {
        t += Time.deltaTime * Time.timeScale;
        hour = (t / 3600) % 24;
        min = (t / 60) % 60;
        sec = t % 60;

        string hours = ((int)hour).ToString();
        string minutes = ((int)min).ToString();
        string seconds = sec.ToString("f2");

        timeText.text = hours + ":" + minutes + ":" + seconds;

        if(sec >= mediumSec && min >= mediumMin && mediumCor == null)
        {
            currentDifficulty = DifficultyLevel.MEDIUM;
            mediumCor = StartCoroutine(ChangeColor(mediumColor));
        }

        else if(sec >= hardSec && min >= hardMin && hardCor == null)
        {
            currentDifficulty = DifficultyLevel.HARD;
            StopCoroutine(mediumCor);
            hardCor = StartCoroutine(ChangeColor(hardColor));
        }
    }

    IEnumerator ChangeColor(Color targetColor)
    {
        while(timeText.color != targetColor)
        {
            timeText.color = Color.Lerp(timeText.color, targetColor, 0.025f);
            yield return null;
        }
    }

}
