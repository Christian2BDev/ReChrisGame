using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Pause : MonoBehaviour
{
   public static bool GameIsPaused = false;
    public GameObject PauseCanvasUI;
        

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                pause();
            }
    }   } 
   public void Resume()
    {
        PauseCanvasUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
     void pause()
    {
        PauseCanvasUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }
}
