using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField] private int[] sceneNumber;
    //Cambio de escena
    public void Change(int sceneNumber)
    {

        SceneManager.LoadScene(sceneNumber);
        if (sceneNumber == 1)
        {
            AudioManager.Instance.StopAndPlayMusic("MainMenuMusic");
        }


    }
}
