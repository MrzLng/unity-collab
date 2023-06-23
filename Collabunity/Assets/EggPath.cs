using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggPath : MonoBehaviour
{

    public Vector3 from;
    public Vector3 to;
    public float distance;
    public Vector3 dir;
    public bool forhire;
    float percentage;
    [SerializeField] float speed = 25f;
    Dictionary<Vector3, List<Vector3>> possible =
        new Dictionary<Vector3, List<Vector3>>() {
            {new Vector3(-497, 1, 71), new List<Vector3>() { new Vector3(-497, 1, 74), new Vector3(494, 1, 71) }},
            {new Vector3(-497, 1, 74), new List<Vector3>() { new Vector3(-497, 1, 71), new Vector3(494, 1, 74) }},
            {new Vector3(494, 1, 71), new List<Vector3>() { new Vector3(-497, 1, 71), new Vector3(494, 1, 74) }},
            {new Vector3(494, 1, 74), new List<Vector3>() { new Vector3(-497, 1, 74), new Vector3(494, 1, 71) } } };
    // Start is called before the first frame update
    void Start()
    {
        forhire = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (forhire)
        {
            forhire = false;
            int goto_place = Random.Range(0, possible[transform.position].Count);
            from = transform.position;
            to = possible[transform.position][goto_place];
            percentage = 0f;
            distance = Vector3.Magnitude(to - from);
            dir = Vector3.Normalize(to - from);
            Debug.Log("Eggpod " + gameObject.name + " is now moving towards " + to + ". This trip is " + distance + " meters. Estimated time of travel = " + distance / speed + " seconds.");
        }
        else {
                transform.position = Vector3.Lerp(from, to, percentage);
                percentage += speed / distance * Time.deltaTime;
            if (transform.position == to) forhire = true;
        }
    }
}
