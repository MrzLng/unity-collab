using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class videocamera : MonoBehaviour
{
    GameObject npc;
    private bool zooming = false;
    private float percentage = 0f;
    private Vector3 currentheight = new Vector3(-15.86761f, 169.5461f, -38.91922f);
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q)) zooming = true;
        if (zooming && percentage < 360) { transform.rotation = Quaternion.Euler(5f, percentage, 0f); percentage += 20 * Time.deltaTime; }
        if (percentage >= 360) transform.rotation = Quaternion.Euler(percentage, percentage, 0f);
        transform.position = currentheight;
        //transform.rotation = Quaternion.Euler(-75f, 0f, 0f);
        }
}
