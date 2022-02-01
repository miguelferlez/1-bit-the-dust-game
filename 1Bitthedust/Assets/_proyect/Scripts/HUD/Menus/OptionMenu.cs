using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    public static OptionMenu Instance;

    GraphicRaycaster gr;
    public Canvas optionCanvas;

    public void Awake()
    {
        Instance = this;

        gr = GetComponent<GraphicRaycaster>();
        optionCanvas.gameObject.SetActive(false);
    }

    public void EnableOptions()
    {
        optionCanvas.gameObject.SetActive(true);
        gr.enabled = false;
    }

    public void DisableOptions()
    {
        optionCanvas.gameObject.SetActive(false);
        gr.enabled = true;
    }
}
