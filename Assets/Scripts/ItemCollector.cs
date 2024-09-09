using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Wood"))
        {
            Inventory.ChangeItemAmount(Inventory.Materials.wood, 1);
        }
    }

    
}
