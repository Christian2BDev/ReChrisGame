using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regeneration : MonoBehaviour
{
    static float RegenTimer = 10f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Regen());
    }

    IEnumerator Regen()
    {

        while (PlayerStats.GetHealth() <= PlayerStats.maxHealth)
        {
            yield return new WaitForSeconds(RegenTimer);
            PlayerStats.Addhealth(1);
         
        }
    }

    public static void UpdateRegen()
    {
        RegenTimer = 10f - (Upgrades.RegenUpgrade-1) * 0.33f;
    }
}
