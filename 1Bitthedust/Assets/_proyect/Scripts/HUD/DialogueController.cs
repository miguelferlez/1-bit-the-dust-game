using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct DialogueData
{
    [TextArea(3, 10)]
    public string[] texts;
}

public class DialogueController : MonoBehaviour
{
    public float textSpeed;

    public Canvas canvas;
    public Image background;
    public TextMeshProUGUI dialogueText;
    public AudioSource textAudio;

    DialogueData currentData;
    Coroutine cor;

    private void Awake()
    {
        canvas.enabled = false;
    }

    public void StartDialogue(DialogueData data)
    {
        Fade.SetFadeAmount(0.75f, 0.25f);
        if(cor != null)
        {
            StopCoroutine(cor);
        }

        cor = StartCoroutine(Dialogue(data));

        canvas.enabled = true;
        Time.timeScale = 0;
    }

    private IEnumerator Dialogue(DialogueData data)
    {

        for (int currentText = 0; currentText < data.texts.Length; currentText++)
        {
            char[] totalChars = data.texts[currentText].ToCharArray();

            for (int currentChars = 1; currentChars <= totalChars.Length; currentChars++)
            {
                char[] currentCharArray = new char[currentChars];

                for (int i = 0; i < currentChars; i++)
                {
                    currentCharArray[i] = totalChars[i];
                }

                dialogueText.text = new string(currentCharArray);

                if (Check())
                {
                    currentChars = totalChars.Length - 1;
                }
                textAudio.Play();
                yield return new WaitForSecondsRealtime(1 / textSpeed); 
                textAudio.Stop();
            }

            yield return new WaitUntil(Check);
            dialogueText.text = string.Empty;
            yield return new WaitForSecondsRealtime(0.35f);
        }
        Fade.SetFadeAmount(0f, 0.25f);
        yield return new WaitForSecondsRealtime(0.25f);
        canvas.enabled = false;
        Time.timeScale = 1;
    }

    private bool Check()
    {
        return Input.GetKey(KeyCode.F);
    }
}
