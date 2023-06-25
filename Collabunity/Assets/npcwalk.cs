using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class npcwalk : MonoBehaviour
{

    bool walking = false;
    Vector3 dir;
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
        if (!walking) {
            Vector2 temp = Random.insideUnitCircle * 5f;
            Vector3 tempdir = new Vector3(temp.x, 0f, temp.y);
            if (Physics.Raycast(transform.position, tempdir, 5f)) return;
            walking = true;
            dir = tempdir;
            transform.rotation = Quaternion.FromToRotation(Vector3.forward, dir);
            from = transform.position;
            to = transform.position + dir;
            percentage = 0f;
            return;
        }
        transform.position = Vector3.Lerp(from, to, percentage);
        percentage += speed / 5f * Time.deltaTime;
        if (transform.position == to) walking = false;
    }
}
