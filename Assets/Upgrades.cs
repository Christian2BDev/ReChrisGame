using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Upgrades : MonoBehaviour
{
    public static int SpeedUpgrade = 1;
    public static int RotationUpgrade = 1;
    public static int HealthUpgrade = 1;
    public static int RegenUpgrade = 1;
    public static int DmgUpgrade = 1;

    private void Start()
    {
        SpeedUpgrade = 1;
        RotationUpgrade = 1;
        HealthUpgrade = 1;
        RegenUpgrade = 1;
        DmgUpgrade = 1;
        PlayerController.UpdateMaxVelocity();
        PlayerController.UpdateAngularVelocity();
        CannonBallBehaviour.UpdateCannonBallDmg();
        PlayerStats.UpdateMaxHealth();
        Regeneration.UpdateRegen();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Dock.docked && gameObject.name.Equals("UpgradeCanvas"))
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(!gameObject.transform.GetChild(0).gameObject.activeSelf);
        } 
    }


   public void Upgrade(string UpgradeName) {
        switch (UpgradeName) {
            case "Speed": Speed(); break;
            case "Rotation": Rotation(); break;
            case "Health":  Health();  break;
            case "Regen": Regen(); break;
            case "Dmg": Dmg();  break;
        }

    }

    void Speed() {
        Debug.Log(CanUpgrade(SpeedUpgrade, Inventory.Materials.stone));
        if (CanUpgrade(SpeedUpgrade, Inventory.Materials.stone))
        {
           
            UpdateText("Speed", SpeedUpgrade, Inventory.Materials.stone);
            SpeedUpgrade++;
            PlayerController.UpdateMaxVelocity();
            
        }

    }

    void Rotation()
    {
        if (CanUpgrade(RotationUpgrade, Inventory.Materials.wood))
        {
          
            UpdateText("Rotation", RotationUpgrade, Inventory.Materials.wood);
            RotationUpgrade++;
            PlayerController.UpdateAngularVelocity();
        }
    }

    void Health()
    {
        if (CanUpgrade(HealthUpgrade, Inventory.Materials.stone))
        {
            
            UpdateText("Health", HealthUpgrade, Inventory.Materials.steel);
            HealthUpgrade++;
            PlayerStats.UpdateMaxHealth();
        }
    }

    void Regen()
    {
        if (CanUpgrade(RegenUpgrade, Inventory.Materials.meat))
        {
            
            UpdateText("Regen", RegenUpgrade, Inventory.Materials.meat);
            RegenUpgrade++;
            Regeneration.UpdateRegen();
        }
    }

    void Dmg()
    {
        if (CanUpgrade(DmgUpgrade, Inventory.Materials.stone))
        {
           
            UpdateText("Dmg", DmgUpgrade, Inventory.Materials.stone);
            DmgUpgrade++;
            CannonBallBehaviour.UpdateCannonBallDmg();
        }
    }


    void UpdateText(string s, int up, Inventory.Materials m ) {
        Inventory.ChangeItemAmount(m, -GetCurrentPricing(up, 1.5f));
        Inventory.ChangeItemAmount(Inventory.Materials.gold, -GetCurrentPricing(up, 1.2f));
        GameObject.Find(s+"Text").GetComponent<TMP_Text>().text = (up+1).ToString();
        GameObject.Find(s+"itemCost1").GetComponent<TMP_Text>().text = GetCurrentPricing(up+1, 1.5f).ToString();
        GameObject.Find(s+"itemCost2").GetComponent<TMP_Text>().text = GetCurrentPricing(up+1, 1.2f).ToString();

    }

    int GetCurrentPricing(int up, float multiplier)
    {

        return Mathf.FloorToInt(up * up * multiplier);
    }

    bool CanUpgrade(int up,Inventory.Materials m1 )
    {
        if (Inventory.GetItemAmount(m1) >= GetCurrentPricing(up,1.5f) && Inventory.GetItemAmount(Inventory.Materials.gold) >= GetCurrentPricing(up,1.2f))
        {

            return true;
        }
        else { return false; }
        
    }
}
