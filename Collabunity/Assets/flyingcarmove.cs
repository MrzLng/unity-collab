using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class flyingcarmove : MonoBehaviour
{
    public Vector3 from;
    public Vector3 to;
    private float speed = 35f;
    private float percentage = 0f;
    public float distance;
    public bool start = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!start) return;
        transform.position = Vector3.Lerp(from, to, percentage);
        percentage += speed / distance * Time.deltaTime;
        if (transform.position == to) Destroy(gameObject);
    }
}
