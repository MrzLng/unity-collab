/* Made and maintained by Jeff [DO NOT TOUCH] */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggPath : MonoBehaviour
{

	public Vector3 from;
	public Vector3 to;
	public float distance;
	public Vector3 dir;
	public float arrivaltime;
	bool forhire;
	float percentage;
	float speed = 25f;
    Dictionary<Vector3, List<Tuple<Vector3, float>>> possible =
		new Dictionary<Vector3, List<Tuple<Vector3, float>>>() {
			{ new Vector3(-497f,    1f,	71f), new List<Tuple<Vector3, float>>(){ new Tuple<Vector3, float>(new Vector3(-497f, 1f, 74f), 1f) }},
			{ new Vector3(-497f,    1f,	74f), new List<Tuple<Vector3, float>>(){ new Tuple<Vector3, float>(new Vector3(-186f, 1f, 74f), 1f) }},
			{ new Vector3(-184.5f,  1f,	71f), new List<Tuple<Vector3, float>>(){ new Tuple<Vector3, float>(new Vector3(-497f, 1f, 71f), 0.95f), new Tuple<Vector3, float>(new Vector3(-186f, 1f, 74f), 1f) }},
			{ new Vector3(-186f,    1f,	74f), new List<Tuple<Vector3, float>>(){ new Tuple<Vector3, float>(new Vector3(7f, 1f, 74f), 0.95f), new Tuple<Vector3, float>(new Vector3(-184.5f, 1f, 71f), 1f) }},
			{ new Vector3( 7f,      1f,	71f), new List<Tuple<Vector3, float>>(){ new Tuple<Vector3, float>(new Vector3(-184.5f, 1f, 71f), 0.95f), new Tuple<Vector3, float>(new Vector3(7f, 1f, 74f), 1f) }},
			{ new Vector3( 7f,		1f,	74f), new List<Tuple<Vector3, float>>(){ new Tuple<Vector3, float>(new Vector3(306.5f, 1f, 74f), 0.95f), new Tuple<Vector3, float>(new Vector3(7f, 1f, 71f), 1f) }},
			{ new Vector3( 308f,	1f,	71f), new List<Tuple<Vector3, float>>(){ new Tuple<Vector3, float>(new Vector3(7f, 1f, 71f), 0.95f), new Tuple<Vector3, float>(new Vector3(306.5f, 1f, 74f), 1f) }},
			{ new Vector3( 306.5f,	1f,	74f), new List<Tuple<Vector3, float>>(){ new Tuple<Vector3, float>(new Vector3(494f, 1f, 74f), 0.95f), new Tuple<Vector3, float>(new Vector3(308f, 1f, 71f), 1f) }},
			{ new Vector3( 494f,	1f,	71f), new List<Tuple<Vector3, float>>(){ new Tuple<Vector3, float>(new Vector3(308f, 1f, 71f), 1f) }},
			{ new Vector3( 494f,	1f,	74f), new List<Tuple<Vector3, float>>(){ new Tuple<Vector3, float>(new Vector3(494f, 1f, 71f), 1f) }},
		};
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
			int goto_place = UnityEngine.Random.Range(0, 100);
			float prob = goto_place / 100f;
			for (int i = 0; i < possible[transform.position].Count; i++) {
				if (prob <= possible[transform.position][i].Item2)
				{
					to = possible[transform.position][i].Item1; break;
				}
			}

            from = transform.position;
			percentage = 0f;
			distance = Vector3.Magnitude(to - from);
			dir = Vector3.Normalize(to - from);
			arrivaltime = Time.time + distance / speed;
			GameObject[] eggs = GameObject.FindGameObjectsWithTag("Egg");
			for (int i = 0; i < eggs.Length; i++)
			{
				if (eggs[i].name != gameObject.name && eggs[i].GetComponent<EggPath>().to == to && (Mathf.Abs(eggs[i].GetComponent<EggPath>().arrivaltime - arrivaltime) <= 0.05)) {
					forhire = true; return;
				}
			}
			Debug.Log("Eggpod " + gameObject.name + " is now moving towards " + to + ". This trip is " + distance + " meters. Estimated time of travel = " + distance / speed + " seconds.");
		}
		else {
				transform.position = Vector3.Lerp(from, to, percentage);
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.Cross(dir, Vector3.up)), percentage * 10);
				percentage += speed / distance * Time.deltaTime;
			if (transform.position == to) forhire = true;
		}
	}
}
