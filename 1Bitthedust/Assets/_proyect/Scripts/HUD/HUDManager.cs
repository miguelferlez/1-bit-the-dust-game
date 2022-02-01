using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

    public static HUDManager Instance;

    public Canvas pauseMenu;
    bool isPaused;
    public Canvas deathCanvas;
    public Canvas deathMenuCanvas;
    public Canvas selected;


    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI finalLevelText;
    public TextMeshProUGUI LevelText;

    public PlayerHealth player;
    public Slider healthBar;

    public void Awake()
    {
        Instance = this;
        deathCanvas.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
        selected.gameObject.SetActive(false);
    }

    public void Update()
    {
        healthBar.value = player.currentHealth;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!player.isDead)
            {
                if (isPaused)
                {
                    ContinueGame();
                }
                else
                {
                    PauseMenu();
                }
            }
            else
            {
                return;
            }
        }
    }

    public void ShowMenu()
    {
        deathCanvas.gameObject.SetActive(true);
        deathMenuCanvas.gameObject.SetActive(false);
        finalScoreText.text = ScoreManager.currentPoints.ToString();
        finalLevelText.text = SceneController.currentLevel.ToString();
        StartCoroutine(DelayMenu());

        LevelsString();
    }

    public void PauseMenu()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenu.gameObject.SetActive(true);
        OptionMenu.Instance.DisableOptions();
    }
    public void ContinueGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenu.gameObject.SetActive(false);
    }

    public void LevelsString()
    {
        if (SceneController.ActiveScene == "Tutorial")
        {
            finalLevelText.text = "";
            LevelText.text = "Nothing?";
        }
        if (SceneController.currentLevel == 1)
        {
            LevelText.text = "  Level";
        }
    }

    IEnumerator DelayMenu()
    {
        yield return new WaitForSeconds(6f);
        deathMenuCanvas.gameObject.SetActive(true);

        selected.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);
        Time.timeScale = 0f;
    }    
}
