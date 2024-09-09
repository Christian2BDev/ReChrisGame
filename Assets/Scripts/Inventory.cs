using UnityEngine;
using TMPro;

public static class Inventory
{
    private static int wood = 0;
    private static int stone = 0;
    private static int meat = 0;
    private static int gold = 0;
    private static int steel = 0;

    public enum Materials {
        wood,stone,meat,steel,gold
    }

    public static void ChangeItemAmount(Materials m, int a) {
        switch (m)
        {
            case Materials.wood: wood += a; break;
            case Materials.stone: stone += a; break;
            case Materials.meat: meat += a; break;
            case Materials.gold: gold += a; break;
            case Materials.steel: steel += a; break;
        }
        UpdateItems();
       
    }
    public static int GetItemAmount(Materials m) {
        switch (m)
        {
            case Materials.wood: return wood; 
            case Materials.stone:return stone; 
            case Materials.meat: return meat; 
            case Materials.gold: return gold; 
            case Materials.steel: return steel;
            default: return -1;
        }
      
    }

    private static void UpdateItems() {
        GameObject.Find("woodValue").GetComponent<TMP_Text>().text = wood.ToString();
        GameObject.Find("stoneValue").GetComponent<TMP_Text>().text = stone.ToString();
        GameObject.Find("meatValue").GetComponent<TMP_Text>().text = meat.ToString();
        GameObject.Find("steelValue").GetComponent<TMP_Text>().text = steel.ToString();
        GameObject.Find("goldValue").GetComponent<TMP_Text>().text = gold.ToString();
    }
}
