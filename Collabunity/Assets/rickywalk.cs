using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class rickywalk : MonoBehaviour
{
    private Vector3[] route = new Vector3[] { new Vector3(0f, 0f, -15f),
        new Vector3(26.65f, 0f, -17.19f), new Vector3(8.13f, 0f, -116.2f),
        new Vector3(-18.12f, 0f, 0f), new Vector3(0f, 0f, 1.15f) };
    private int index = 0;
    bool done = false;
    bool walking = false;
    Vector3 dir = Vector3.zero;
    Vector3 from;
    Vector3 to;
    Animator animator;
    float percentage;
    float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("walk", walking);
    }

    void FixedUpdate()
    {
        if (!walking && !done)
        {
            walking = true;
            dir = route[index];
            index++;
            transform.rotation = Quaternion.LookRotation(dir);
            from = transform.position;
            to = transform.position + dir;
            percentage = 0f;
            return;
        }
        transform.position = Vector3.Lerp(from, to, percentage);
        percentage += speed / dir.magnitude * Time.deltaTime;
        if (transform.position == to) walking = false;
        if (!(index == 5 && walking == false)) return;
        done = true;
        Destroy(gameObject);
    }
}
