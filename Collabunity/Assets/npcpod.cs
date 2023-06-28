using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcpod : MonoBehaviour
{
    private Animator anim;
    private Transform npc;

    private Transform door;//Mori
    private bool movingup = false;//Mori
    private bool movingdown = false;//Mori
    private bool play = false;
    private float speed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        npc = transform.Find("NPC");
        door = transform.Find("Cube.001_Cube.004_Material.001");//Mori
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.J))
        {
            movingup = true;//Mori
            //npc.localPosition = -npc.transform.forward.normalized;
            //anim.SetBool("npcGetOut", true);
        }
    }

    void FixedUpdate()
    {
        if (movingup)
        {
            door.position = door.position + new Vector3(0, speed * Time.fixedDeltaTime, 0);
            if (door.localPosition.y > 1.6)
            {
                movingup = false;
                door.localPosition = new Vector3(0, 1.5f, 0);
                play = true;

            }
        }
        else if (movingdown)
        {
            door.position = door.position - new Vector3(0, speed * Time.fixedDeltaTime, 0);
            if (door.localPosition.y < 0)
            {
                movingdown = false;
                door.localPosition = new Vector3(0, 0, 0);
            }
        }
        if (play)
        {
            anim.SetBool("npcGetOut", true);
            play = false;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("idle")) movingdown = true;
    }
}


