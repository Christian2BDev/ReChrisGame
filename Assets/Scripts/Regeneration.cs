using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regeneration : MonoBehaviour
{
    static float RegenTimer = 5f;
    float value;

    private float time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        value = PlayerStats.GetHealth();
    }

    private void Update()
    {

        time += Time.deltaTime;

        if (time >= RegenTimer)
        {
            time = time - RegenTimer;
            PlayerStats.ChangeHealth(1);
            // execute block of code here
        }
        //value = Mathf.MoveTowards(value, (PlayerStats.maxHealth), (1 / RegenTimer) * Time.deltaTime);
        //PlayerStats.Sethealth( Mathf.MoveTowards(PlayerStats.GetHealth(), (PlayerStats.maxHealth), Time.deltaTime));
    }

  

    public static void UpdateRegen()
    {
        RegenTimer = 5f - (Upgrades.RegenUpgrade-1) * 0.33f;
    }
}
