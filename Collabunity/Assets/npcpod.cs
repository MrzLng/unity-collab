using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private float percentage = 0f;
    private float percentage2 = 0f;
    private Vector3 vel = Vector3.zero;
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
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 0.434f, transform.position.z), percentage);
            percentage += speed * 0.225f * Time.fixedDeltaTime;
            if (transform.position.y == 0.434f)
            {
                door.position = door.position + new Vector3(0, speed * Time.fixedDeltaTime, 0);
                if (door.localPosition.y > 1.6)
                {
                    movingup = false;
                    percentage = 0f;
                    door.localPosition = new Vector3(0, 1.5f, 0);
                    play = true;

                }

            }
        }
        else if (movingdown)
        {
            door.localPosition = Vector3.Lerp(door.localPosition, Vector3.zero, percentage);
            percentage += speed * Time.fixedDeltaTime;
            if (door.localPosition.y == 0f)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 1f, transform.position.z), percentage2);
                percentage2 += speed * 0.225f * Time.deltaTime;
                if (transform.position.y == 1f)
                {
                    movingdown = false;
                    percentage = 0f;
                    percentage2 = 0f;

                }
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


