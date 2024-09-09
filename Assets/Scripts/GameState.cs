using TMPro;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField]
    public bool Storm = true;
    public float timerTime;

    public TMP_Text timer;
    [SerializeField]
    private float min = 0;
    [SerializeField]
    private float max = 0;

    [SerializeField]
    public static bool gameOver = false;
 
    private void Start()
    {
        timerTime = Mathf.Round(Random.Range(60f * min, 60f * max));
    }


    void Update()
    {
        timerTime -= Time.deltaTime;

        //timer display
        float minutes = Mathf.Floor(timerTime / 60);
        float seconds = Mathf.Floor(timerTime - minutes * 60f);
        if (seconds <= 9f) timer.text = $"{minutes} : {"0" + seconds}";
        else timer.text = $"{minutes} : {seconds}";

        //timer done
        if (timerTime <= 0.00f)
        {
            switchPhase();
        }
    }

    //generated a new timer
    void switchPhase() {
        timerTime = Mathf.Round(Random.Range(60f * min, 60f * max));
        Storm = !Storm;
    }

}
