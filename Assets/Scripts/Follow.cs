using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Follow : MonoBehaviour
{
    public GameObject player;
    public GameObject boat;
    public Vector3 pos;
    private float camDistance = -1;
    [SerializeField] float LandCamDis;
    [SerializeField] float SeaCamDis;
    [SerializeField]float AfterStartUpzoomSpeed;
    [SerializeField]float StartZoomSpeed;
    private float ZoomSpeed;

    [SerializeField]
    private float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        ZoomSpeed = StartZoomSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Dock.docked) {
            pos = Vector3.MoveTowards(gameObject.transform.position, boat.transform.position, speed * Time.deltaTime);
            camDistance = Mathf.MoveTowards(camDistance, SeaCamDis, (1 / ZoomSpeed) * Time.deltaTime);
            gameObject.transform.position = new Vector3(pos.x, pos.y, camDistance);

            if (camDistance == SeaCamDis)
            {
                ZoomSpeed = AfterStartUpzoomSpeed;
            }
        }
        else {
            
            pos = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, speed * Time.deltaTime);
            camDistance = Mathf.MoveTowards(camDistance, LandCamDis, (1 / ZoomSpeed) * Time.deltaTime);
            gameObject.transform.position = new Vector3(pos.x, pos.y,camDistance);
        }
        

    }
}
