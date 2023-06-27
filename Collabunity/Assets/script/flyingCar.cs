using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;
using UnityEngine.UIElements;

public class flyingCar : MonoBehaviour
{

    Vector3 normal = new Vector3(0, 470, 0);
    GameObject car;
    // Start is called before the first frame update
    void Start()
    {
        car = GameObject.Find("flyingcar_BASE");
    }

    // Update is called once per frame
    void Update()
    {
        int ret = Random.Range(0, 1000);
        if (ret < 3)
        {
            Vector3 dist = Random.insideUnitSphere;
            dist.Set(Mathf.Abs(dist.x), Mathf.Abs(dist.y), Mathf.Abs(dist.z));
            Vector2 dest = Random.insideUnitCircle;
            dest.Set(Mathf.Abs(dest.x), Mathf.Abs(dest.y));
            dest *= 500; dist *= 500;
            Vector3 dest3 = new Vector3(dest.x, 0f, dest.y);
            GameObject copy = Instantiate(car, dist + normal, Quaternion.LookRotation(dest3));
            Debug.Log("flycar spawns at " + copy.transform.position + " and is now moving towards " + (dist + dest3 + normal));
            flyingcarmove copyscript = copy.GetComponent<flyingcarmove>();
            Renderer mr = copy.transform.Find("Cube_Cube_Material").GetComponent<Renderer>();
            Vector3 clr =Random.insideUnitSphere;
            mr.material.color = new Color(clr.x, clr.y, clr.z);
            copyscript.from = copy.transform.position;
            copyscript.to = dist + dest3 + normal;
            copyscript.start = true;
            copyscript.distance = (copyscript.to - copyscript.from).magnitude;
        }
    }
}

