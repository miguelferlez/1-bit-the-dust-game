using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HighlightedButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI selectedText;

    public void Awake()
    {
        selectedText.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        selectedText.gameObject.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        selectedText.gameObject.SetActive(false);
    }
}
