using UnityEngine;

public class GameObjectActivator : MonoBehaviour
{
    [SerializeField] private GameObject objectToActivate;

    void Update()
    {
        // Check the gameOver status from GameState and activate/deactivate the GameObject
        if (GameState.gameOver)
        {
            objectToActivate.SetActive(true);
            GameState.gameOver = false;
            PlayerStats.SetHealth(100);
            LoreController.loreState = 1;
        }
 
    }
}
