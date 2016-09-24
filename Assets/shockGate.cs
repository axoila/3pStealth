using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shockGate : MonoBehaviour
{

	public bool working = true;

	public bool active = true;

	public float upTime = 1;
	public float downTime = 1;

	public GameObject[] energyBars;
	public GameObject armature;

	public float flutterAmount;

	private Vector3[] boneLocations;

	private float timer = 0;
	private Transform[] bones;

	// Use this for initialization
	void Awake ()
	{
		bones = new Transform[armature.transform.childCount];
		for (int i = 0; i < armature.transform.childCount; i++)
			bones [i] = armature.transform.GetChild (i);

		boneLocations = new Vector3[bones.Length];
		for (int i = 0; i < bones.Length; i++) {
			boneLocations [i] = bones [i].transform.position;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (working) {
			timer += Time.deltaTime;
			if (active && timer > upTime) {
				deActivate ();
				active = false;
				timer = 0;
			}
			if (!active && timer > downTime) {
				activate ();
				active = true;
				timer = 0;
			}

			flutter ();
		} else {
			
		}
	}

	void flutter(){
		for (int i = 0; i < bones.Length; i++) {
			bones [i].position = boneLocations [i] + new Vector3 (0, Random.value - 0.5f, Random.value-0.5f) * flutterAmount;


		}
	}

	void activate ()
	{
		foreach (GameObject bar in energyBars)
			bar.SetActive (true);
	}

	void deActivate ()
	{
		foreach (GameObject bar in energyBars)
			bar.SetActive (false);
	}
}
