using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcpod : MonoBehaviour
{
    private Animator anim;
    private Transform npc;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        npc = transform.Find("NPC");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.J))
        {
            npc.localPosition = -npc.transform.forward.normalized;
            anim.SetBool("npcGetOut", true);
        }
    }
}
