using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeathMenu : Selectable
{
    public Image selectImageReplay;
    public Image selectImageMenu;

    public BaseEventData dataReplay;
    public BaseEventData dataMenu;


    public void Update()
    {
        if(IsHighlighted(dataReplay) == true)
        {
            selectImageReplay.gameObject.SetActive(true);
            selectImageMenu.gameObject.SetActive(false);
        }
        else if(IsHighlighted(dataMenu) == true)
        {
            selectImageReplay.gameObject.SetActive(false);
            selectImageMenu.gameObject.SetActive(true);
        }
    }
}
