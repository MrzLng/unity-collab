using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateAnim : MonoBehaviour
{
    public Transform a;
    public Transform b;
    public Transform c;
    public Transform d;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        a.Rotate(new Vector3(0, 0, 2));
        b.Rotate(new Vector3(0, 0, -5));
        c.Rotate(new Vector3(0, 0, 1));
        d.Rotate(new Vector3(0, 0, -3));
    }
}
