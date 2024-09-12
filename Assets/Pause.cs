using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Pause : MonoBehaviour
{
   public static bool GameIsPaused = false;
    public Animator PauseAnimator;     

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
        Time.timeScale = 1f;
        GameIsPaused = false;
        PauseAnimator.SetTrigger("Out");
    }
     void pause()
    {
        PauseAnimator.SetTrigger("In");
        

    }
    public void pauseFr()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void Menu()
    {
        Transition.FadeToLevel("Menu");
        Time.timeScale = 1f;
    }
}
