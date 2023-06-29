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

    public Vector3 from;
    public Vector3 to;
    public float distance;
    public Vector3 dir;
    private int index = 0;
    private bool forhire = true;
    private bool setted = false;
    public bool donedrive = false;
    public float arrivaltime;
    float podspeed = 25f;
    private bool disconnect = false;
    private Vector3[] route = new Vector3[] { new Vector3(442f, 1f, 74f), new Vector3(435f, 1f, 71f), new Vector3(-31f, 1f, 71f), new Vector3(-40f, 1f, 68.5f), new Vector3(-45f, 1f, 71f), new Vector3(-184.5f, 1f, 71f), new Vector3(-550f, 1f, -293f) };
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
        if (donedrive) return;
        if (forhire)
        {
            if (!setted)
            {
                to = route[index];
                index++;
                from = transform.position;
                percentage = 0f;
                distance = Vector3.Magnitude(to - from);
                dir = Vector3.Normalize(to - from);
                arrivaltime = Time.time + distance / podspeed;
                setted = true;
            }
            GameObject[] eggs = GameObject.FindGameObjectsWithTag("Egg");
            for (int i = 0; i < eggs.Length; i++)
            {
                if ((eggs[i].name != gameObject.name && eggs[i].GetComponent<EggPath>()?.to == to && (Mathf.Abs(eggs[i].GetComponent<EggPath>().arrivaltime - arrivaltime) <= 0.05)) ||
                    (eggs[i].name != gameObject.name && eggs[i].GetComponent<npcpod>()?.to == to && (Mathf.Abs(eggs[i].GetComponent<npcpod>().arrivaltime - arrivaltime) <= 0.05)))
                {
                    forhire = true; return;
                }
            }
            forhire = false;
            Debug.Log("Eggpod " + gameObject.name + " is now moving towards " + to + ". This trip is " + distance + " meters. Estimated time of travel = " + distance / speed + " seconds.");
        }
        else
        {
            transform.position = Vector3.Lerp(from, to, percentage);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.Cross(Vector3.up, dir)), percentage * 10);
            percentage += podspeed / distance * Time.deltaTime;
            if (transform.position == to) { forhire = true; setted = false; }
        }
        if (!((index == 4 || index == 7) && forhire == true)) return;
        else
        {
            donedrive = true; // suppress
            percentage = 0f; //done
            if (index == 4) movingup = true;
            if (index == 7) Destroy(gameObject);
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
                    percentage2 = 0f; donedrive = false;
                }
            }
        }
        if (play && !disconnect)
        {
            anim.SetBool("npcGetOut", true);
            play = false;
        }
        if (!disconnect && anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            movingdown = true;
            npc.SetParent(null, true);
            npc.position = new Vector3(npc.position.x, 0.4f, npc.position.z);
            npc.gameObject.GetComponent<rickywalk>().enabled = true;
            disconnect = true;
        }
        }
}


