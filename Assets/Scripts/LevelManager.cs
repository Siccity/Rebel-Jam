using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    private int currentScene = 1;

    void Awake()
    {
        if (SceneManager.sceneCount == 1) 
        {
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        }
    }

    public void NextLevel()
    {
        SceneManager.UnloadSceneAsync(currentScene);
        currentScene++;
        SceneManager.LoadSceneAsync(currentScene, LoadSceneMode.Additive);
    }
}
