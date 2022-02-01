using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SceneDifficultyWrapper
{
    public string sceneName;
    public string easyLevel;
    public string mediumLevel;
    public string hardLevel;    
}

public class SceneController : MonoBehaviour
{
    //public string mainScene;
    string menu = "Main";
    const string empty = "Empty";

    public static event System.Action <string> sceneLoaded = delegate { };
    private static SceneController Instance;
    [SerializeField]
    SceneDifficultyWrapper[] sceneDifficulties;

    public static string ActiveScene { get { return SceneManager.GetActiveScene().name; } }

    private Dictionary<string, SceneDifficultyWrapper> sceneDic = new Dictionary<string, SceneDifficultyWrapper>();
    public static string currentScene { get; private set; }
    public static int currentLevel
    {
        get
        {
            for (int i = 0; i < currentScene.Length; i++)
            {
                if(char.IsDigit(currentScene[i]))
                {
                    return Convert.ToInt32(char.GetNumericValue(currentScene[i]));
                }
            }
            return -1;
        }
    }

    private void Start()
    {
        sceneDic = new Dictionary<string, SceneDifficultyWrapper>();
        foreach (var item in sceneDifficulties)
        {
            sceneDic.Add(item.sceneName, item);
        }
        Instance = this;
        Fade.SetCurrentFade(1f);
        StartCoroutine(FadeAndSwitchScenes(menu));
    }

    public static void FadeAndLoadScenes(string sceneName)
    {
        Instance.StartCoroutine(FadeAndSwitchScenes(sceneName));
    }

    static IEnumerator FadeAndSwitchScenes(string sceneName)
    {
        Time.timeScale = 0f;
        yield return Fade.SetFadeAmount(1f, 1f);

        DifficultyLevel dif = DifficultyLevel.NONE;
        if (Timer.Instance)
        {
            dif = Timer.Instance.currentDifficulty;
        }

        yield return SceneManager.LoadSceneAsync(empty);
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        string sceneToLoad = sceneName;
        if (Instance.sceneDic.ContainsKey(sceneToLoad))
        {
            switch (dif)
            {
                case DifficultyLevel.EASY:
                    sceneToLoad = Instance.sceneDic[sceneName].easyLevel;
                    break;
                case DifficultyLevel.MEDIUM:
                    sceneToLoad = Instance.sceneDic[sceneName].mediumLevel;
                    break;
                case DifficultyLevel.HARD:
                    sceneToLoad = Instance.sceneDic[sceneName].hardLevel;
                    break;
                default:
                    sceneToLoad = sceneName;
                    break;
            }
        }

        yield return LoadSceneAndSetActive(sceneName ,sceneToLoad);
        yield return SceneManager.UnloadSceneAsync(empty);

        sceneLoaded(sceneName);

        yield return Fade.SetFadeAmount(0f, 1f);
        Time.timeScale = 1f;
    }

    static IEnumerator LoadSceneAndSetActive(string sceneName, string sceneToLoad)
    {
        yield return SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);

        currentScene = sceneName;

        Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newlyLoadedScene);
    }
}
