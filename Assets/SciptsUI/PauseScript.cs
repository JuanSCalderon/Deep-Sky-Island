using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    
   public GameObject menuPause; 
   public bool pause = false;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (pause == false)
            {
                menuPause.SetActive(true);
                pause = true; 
                Time.timeScale = 0;
                Cursor.visible = true;
            }

            else if(pause == true){
                Resume();
            }
        }
    }

//Cierra el menú de pausa.
    public void Resume(){

        menuPause.SetActive(false);
        pause = false;

        Time.timeScale = 1;
    }

    public void MenuStart(string nameMenu){
        SceneManager.LoadScene(nameMenu);
    }
}
