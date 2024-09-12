using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    private static Animator FadeAnim;
    static string LevelToFade;
    private void Start()
    {
       FadeAnim = GetComponent<Animator>();
    }
    public static void FadeToLevel(string LevelName)
    {
        
        FadeAnim.SetTrigger("T");
        LevelToFade = LevelName;
        
    }

    
    public void FadeComplete() {
        SceneManager.LoadScene(LevelToFade);
    }
}
