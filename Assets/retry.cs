using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Required for using UI elements

public class RestartOnButtonPress : MonoBehaviour
{
    public Button restartButton; // Reference to the UI Button
    public string sceneName; // Optional: The name of the scene to load

    void Start()
    {
        // Make sure the Button component is assigned and listen for a button click
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartScene);
        }
    }

    // Method to restart the scene
    void RestartScene()
    {
        // If sceneName is provided, load it, otherwise reload the current scene
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
