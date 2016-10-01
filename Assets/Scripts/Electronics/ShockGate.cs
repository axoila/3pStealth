using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockGate : ElectronicsComponent
{

	public bool buzzing { private set; get;}

	[SerializeField] private float upTime = 1;
	[SerializeField] private float downTime = 1;

	[SerializeField] private GameObject[] energyBars;
	[SerializeField] private GameObject armature;

	[SerializeField] private float flutterAmount;

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
		for (int i = 0; i < bones.Length; i++)
        {
			boneLocations [i] = bones [i].transform.position;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Active)
        {
			timer += Time.deltaTime;
			if (buzzing && timer > upTime && downTime != 0)
            {
				DeactivateBuzz ();
				buzzing = false;
				timer = 0;
			}
			if (!buzzing && timer > downTime)
            {
				ActivateBuzz ();
				buzzing = true;
				timer = 0;
			}

			Flutter ();
		}
	}

	void Flutter(){
		for (int i = 0; i < bones.Length; i++)
        {
			bones [i].position = boneLocations [i] + new Vector3 (0, Random.value - 0.5f, Random.value-0.5f) * flutterAmount;
		}
	}

	void ActivateBuzz ()
	{
		foreach (GameObject bar in energyBars)
			bar.SetActive (true);
	}

	void DeactivateBuzz ()
	{
		foreach (GameObject bar in energyBars)
			bar.SetActive (false);
	}


	protected override void OnActivate() {
		if (buzzing)
			ActivateBuzz ();
	}

	protected override void OnDeActivate() {
		DeactivateBuzz ();
	}
}
