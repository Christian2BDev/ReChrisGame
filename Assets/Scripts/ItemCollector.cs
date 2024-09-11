using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    //hit config
    int HitsRequired;
    public int HitsRemaining;
    public int minHits;
    public int maxHits;

    //resource config
    int ResourceAmount;
    public int minAmount;
    public int maxAmount;
    public Inventory.Materials Item = new Inventory.Materials();


    

    void Start()
    {
        HitsRequired = Random.Range(minHits, maxHits);
        HitsRemaining = HitsRequired;

        ResourceAmount = Random.Range(minAmount, maxAmount);
    }

    public void Damage(int x) {
        HitsRemaining -= x;
        if (HitsRemaining <= 0)
        {
            Destroy(this.gameObject);
        }

        Inventory.ChangeItemAmount(Item, ResourceAmount);
    }
}
