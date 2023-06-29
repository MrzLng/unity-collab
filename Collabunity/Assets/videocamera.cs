using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class videocamera : MonoBehaviour
{
    GameObject npc;
    private bool zooming = false;
    private float percentage = 0f;
    private Vector3 currentheight = new Vector3(-79f, 15.4f, -85f);
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("q"))zooming = true;
        if (zooming) { transform.position = Vector3.Lerp(currentheight, new Vector3(-28f, 2f, -85f), percentage); percentage += 0.2f * Time.deltaTime; }
        transform.rotation = Quaternion.LookRotation(new Vector3(-54.31f, 8.15f, -101.63f) - transform.position);
    }
}
