using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class podControl : MonoBehaviour
{

    public Transform og;
    public Transform door;
    private bool movingup = false;
    private bool movingdown = false;
    private float speed = 2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up") && !(movingup || movingdown))
        {
            movingup = true;
        }
        if (Input.GetKey("down") && !(movingup || movingdown))
        {
            movingdown = true;
        }
    }

    void FixedUpdate()
    {
        if (movingup)
        {
            door.position = door.position + new Vector3(0, speed * Time.fixedDeltaTime, 0);
            if (door.localPosition.y > 1.5)
            {
                movingup = false;
                door.localPosition = new Vector3(0, 1.5f, 0);
            }
        }
        else if (movingdown)
        {
            door.position = door.position - new Vector3(0, speed * Time.fixedDeltaTime, 0);
            if (door.localPosition.y <0)
            {
                movingdown = false;
                door.localPosition = new Vector3(0, 0, 0);
            }
        }
    }
}
