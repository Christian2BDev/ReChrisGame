using System.Collections;
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

    private GameObject breakParticle;
    

    void Start()
    {
        breakParticle = transform.GetChild(1).gameObject;
        
        HitsRequired = Random.Range(minHits, maxHits);
        HitsRemaining = HitsRequired;

        ResourceAmount = Random.Range(minAmount, maxAmount);
    }

    public void Damage(int x) {
        HitsRemaining -= x;
        if (HitsRemaining <= 0)
        {
            breakParticle.SetActive(true);
            StartCoroutine(PassiveMe(0.2f));
            Inventory.ChangeItemAmount(Item, ResourceAmount);
            GameState.timerTime -= Random.Range(0, 3);
        }
    }

    IEnumerator PassiveMe(float secs)
    {
        yield return new WaitForSeconds(secs);
        Camera.main.transform.GetComponent<SoundManager>().PlayDestroyItem();
        Destroy(this.gameObject);
    }
}
