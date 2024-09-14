using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameState : MonoBehaviour
{
    [SerializeField]
    public bool Storm = true;
    public static float timerTime;

    public TMP_Text timer;
    [SerializeField]
    private float min = 0;
    [SerializeField]
    private float max = 0;

    public GameObject postProccesing;
    private Vignette vg;
    private Volume v;
    private ColorAdjustments ca;
    [SerializeField]
    float target = 0.5f;
    [SerializeField]
    float orignal = 0;
    [SerializeField]
    float duration = 7.5f;
    [SerializeField]
    float value = 0;


    [SerializeField]
    public static bool gameOver = false;
 
    private void Start()
    {
        v = postProccesing.GetComponent<Volume>();
        v.profile.TryGet(out vg);
        v.profile.TryGet(out ca);
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


        if (Storm)
        {
            value = Mathf.MoveTowards(value, target, (1 / duration) * Time.deltaTime);
            

        }
        else {
            value = Mathf.MoveTowards(value, orignal, (1 / duration) * Time.deltaTime);
            
        }
        vg.intensity.value = value;
        ca.postExposure.value = value * -5;
        ca.saturation.value = value * -155.5f;
    }

    //generated a new timer
    void switchPhase() {
        timerTime = Mathf.Round(Random.Range(60f * min, 60f * max));
        Storm = !Storm;
    }
}
