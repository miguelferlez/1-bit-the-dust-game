using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
    }

    public void ChangeMain(string mainScene)
    {
        if(mainScene == "Tutorial")
        {
            CheckPointData.Instance.SaveCondition("MustLoadTime", false);
            Time.timeScale = 1f;
            //AudioManager.Instance.music.audioMixer.SetFloat("volumeMusic", -30f);
        }
        SceneController.FadeAndLoadScenes(mainScene);
    }

    public void Continue()
    {
        SceneController.FadeAndLoadScenes(SceneController.ActiveScene);
        Time.timeScale = 1f;
    }

    
}
