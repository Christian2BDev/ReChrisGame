using UnityEngine;

public static class Inventory
{
    private static int wood = 0;
    private static int stone = 0;
    private static int food = 0;
    private static int gold = 0;
    private static int iron = 0;

    public enum Materials {
        wood,stone,food,iron,gold
    }

    static void ChangeItemAmount(Materials m, int a) {
        switch (m)
        {
            case Materials.wood: wood += a; break;
            case Materials.stone: stone += a; break;
            case Materials.food: food += a; break;
            case Materials.gold: gold += a; break;
            case Materials.iron: iron += a; break;
        }
       
    }
    static int GetWood(Materials m) {
        switch (m)
        {
            case Materials.wood: return wood; 
            case Materials.stone:return stone; 
            case Materials.food: return food; 
            case Materials.gold: return gold; 
            case Materials.iron: return iron;
            default: return -1;
        }
      
    }
}
