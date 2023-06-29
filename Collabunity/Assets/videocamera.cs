using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class videocamera : MonoBehaviour
{
    GameObject npc;
    private bool zooming = false;
    private float percentage = 0f;
    private Vector3 currentheight = new Vector3(0f, 30f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        npc = GameObject.Find("/SPECIALNPCPOD/NPC");
    }

    // Update is called once per frame
    void Update()
    {
        if (npc.transform.parent == null)
        {
            zooming = true;
        }
        if (zooming) { currentheight = Vector3.Lerp(currentheight, new Vector3(0f, 15f, 0f), percentage); percentage += 0.2f * Time.deltaTime; }
        transform.position = npc.transform.position + currentheight;
        Debug.Log("AAAAAAAAAAAAAAA "+transform.position);
        transform.rotation = Quaternion.LookRotation(npc.transform.position - transform.position);
    }
}
