using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject player;
    public GameObject boat;
    public Vector3 pos;
    [SerializeField]
    private float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Dock.docked) {  pos = Vector3.MoveTowards(gameObject.transform.position, boat.transform.position, speed * Time.deltaTime); }
        else {  pos = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, speed * Time.deltaTime); }
        gameObject.transform.position = new Vector3(pos.x, pos.y, -10);

    }
}
