using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public CharacterMovement Player1;
    public CharacterMovement Player2;

    public GameObject LoadingScreen;

    private int currentScene = 1;

    void Awake()
    {
        if (SceneManager.sceneCount == 1) 
        {
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadingScreen.SetActive(false);
    }

    public void NextLevel()
    {
        LoadingScreen.SetActive(true);

        currentScene++;
        SceneManager.LoadSceneAsync(currentScene, LoadSceneMode.Additive);

        Player1.ResetCharacterOnLevelLoad();
        Player2.ResetCharacterOnLevelLoad();

        SceneManager.UnloadSceneAsync(currentScene-1);
    }


}
