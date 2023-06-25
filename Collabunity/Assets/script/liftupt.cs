using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class liftupt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "npcman")
        {
            collision.gameObject.transform.position = new Vector3(-24, 18, -80);
        }
    }
}
