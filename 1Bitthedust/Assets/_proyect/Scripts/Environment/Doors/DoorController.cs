using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DoorController : MonoBehaviour, IUnlock
{
    SpriteRenderer render;

    public KeyController keyControl;
    public Canvas unlockCanvas;
    public TextMeshProUGUI needPointsText;

    public Sprite closeDoor;
    public Sprite openDoor;

    public AudioSource closeAudio;

    public string sceneName;
    public int scoreNextLevel;

    bool isOpen;
    bool isClosed;

    public PlayerController playerControl;

    public void Awake()
    {
        SceneController.sceneLoaded += LoadData;
        render = GetComponent<SpriteRenderer>();
        render.sprite = closeDoor;

        unlockCanvas.enabled = false;
        needPointsText.gameObject.SetActive(false);
        var unlockText = unlockCanvas.GetComponentInChildren<TextMeshProUGUI>();
        unlockText.text = "Unlock Door: " + scoreNextLevel + " pts";
    }

    #region KEYS
    public void Start()
    {
        if (keyControl != null)
        {
            keyControl.AddKey(this);
        }
    }

    public void Open()
    {
        render.sprite = openDoor;
        isOpen = true;
        isClosed = false;      
    }

    public void Close()
    {
        render.sprite = closeDoor;
        isClosed = true;
    }

    public void OpenWithScore()
    {
        if (isOpen)
        {
            if (Input.GetKey(KeyCode.E))
            {
                return;
            }
        }

        if (Input.GetKey(KeyCode.E))
        {
            if (ScoreManager.currentPoints >= scoreNextLevel)
            {
                needPointsText.gameObject.SetActive(false);
                ScoreManager.RemoveScore(scoreNextLevel);
                Open();
                closeAudio.mute = true;
            }
            else
            {
                needPointsText.gameObject.SetActive(true);
                closeAudio.Play();
            }
        }
    }

    public void OnDestroy()
    {
        SceneController.sceneLoaded -= LoadData;

        if (keyControl != null)
        {
            keyControl.RemoveKey(this);
        }
    }
    #endregion

    #region SCENE_DATA
    public void OnTriggerStay2D(Collider2D col)
    {
       
        if(col.gameObject.layer == 8)
        {
            OpenWithScore();
            unlockCanvas.enabled = true;
            if (ScoreManager.currentPoints >= scoreNextLevel)
            {
                needPointsText.gameObject.SetActive(false);
                closeAudio.mute = true;
            }

            if (isOpen)
            {
                unlockCanvas.enabled = false;
            }
            else
            {                
                unlockCanvas.enabled = true;
            }
        }
    }
    public void OnTriggerExit2D(Collider2D col)
    {
        unlockCanvas.enabled = false;
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        var colLevel = GetComponent<BoxCollider2D>();
        if(col.gameObject.layer == 8)
        {
            if (col.otherCollider == colLevel)
            {
                if (isOpen)
                {
                    needPointsText.gameObject.SetActive(false);
                    SaveData();
                    SceneController.FadeAndLoadScenes(sceneName);
                    playerControl.walkAudio.GetComponent<AudioSource>().enabled = false;
                }
            }
        }
    }

    public void SaveData()
    {
        if (isOpen)
        {
            CheckPointData.Instance.SaveHealth();
            CheckPointData.Instance.SaveScore();
        }
    }

    void LoadData(string sceneName)
    {
        if (sceneName != "Tutorial")
        {
            CheckPointData.Instance.GetSavedHealth();
            CheckPointData.Instance.GetSavedScore();
        }
    }
    #endregion
}
