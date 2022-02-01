using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    private struct FadeData
    {
        public float amountToSet;
        public float time;

        public FadeData(float amountToSet, float time)
        {
            this.amountToSet = amountToSet;
            this.time = time;
        }
    }


    private static Fade Instance;
    private static Coroutine currentCor;
    public Image fadeImage;

    private void Awake()
    {
        Instance = this;
    }

    public static void SetCurrentFade(float alphaAmount)
    {
        Color imageCol = Instance.fadeImage.color;
        imageCol.a = alphaAmount;
        Instance.fadeImage.color = imageCol;
    }

    public static Coroutine SetFadeAmount(float alphaAmount, float time)
    {
        if (currentCor != null)
            Instance.StopCoroutine(currentCor);
        currentCor = Instance.StartCoroutine(SetFade(new FadeData(alphaAmount, time)));
        return currentCor;
    }

    private static IEnumerator SetFade(FadeData data)
    {
        float timeToStopFade = Time.unscaledTime + data.time;
        float percentageFade = 0;
        float startingAlpha = Instance.fadeImage.color.a;
        while (percentageFade < 1)
        {
            percentageFade = (data.time - (timeToStopFade - Time.unscaledTime)) / data.time ;

            Color imageCol = Instance.fadeImage.color;
            imageCol.a = Mathf.Lerp(startingAlpha, data.amountToSet, percentageFade);
            Instance.fadeImage.color = imageCol;
            yield return null;
        }
        currentCor = null;
    }

}
