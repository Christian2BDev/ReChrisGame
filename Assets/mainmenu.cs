using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
  public void playgame ()
    {
        Transition.FadeToLevel("Game");
        //SceneManager.LoadScene("Game");
    }

   public void stopgame()
    {
        Application.Quit();
        Debug.Log("quit");
    }
}
