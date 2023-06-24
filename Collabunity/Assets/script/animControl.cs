using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animControl : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("o"))
        {
            anim.SetBool("walk", true);
        } else
        {
            anim.SetBool("walk", false);
        }

        if (Input.GetKey("e")) 
        {
            anim.SetBool("watch", true);
        } else if (Input.GetKey("r"))
        {
            anim.SetBool("watch", false);
        }
    }
}
