using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private Scene currentScene;
    public Vector3 lastCheckPointPos;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    void Update()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    public void LoadGameOverScene() => SceneManager.LoadScene("Defeat");
    public void LoadVictoryScene() => SceneManager.LoadScene("Victory");
    public void ReloadScene() => SceneManager.LoadScene(currentScene.name);





}

