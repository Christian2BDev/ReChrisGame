using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
  public void playgame ()
    {
        SceneManager.LoadScene("Game");
    }

   public void stopgame()
    {
        Application.Quit();
        Debug.Log("quit");
    }
}
