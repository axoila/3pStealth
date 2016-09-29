using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {

	public int currentweight = 0;

	public bool includeLower = true;

	public GameObject sliderBone;
	public Vector3 sliderStart;
	public Vector3 sliderEnd;

	public MonoBehaviour[] outputSize6;

	public PlayerManager playerMngr;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider colli) {
		Debug.Log (colli);
		if (colli.gameObject == playerMngr.characters [0])
			currentweight += 1;
		if (colli.gameObject == playerMngr.characters [1])
			currentweight += 2;
		if (colli.gameObject == playerMngr.characters [2])
			currentweight += 3;

		updateStuff ();
	}

	void OnTriggerExit (Collider colli) {
		//Debug.Log (colli);
		if (colli.gameObject == playerMngr.characters [0])
			currentweight -= 1;
		if (colli.gameObject == playerMngr.characters [1])
			currentweight -= 2;
		if (colli.gameObject == playerMngr.characters [2])
			currentweight -= 3;

		updateStuff ();
	}

	void updateStuff () {
		//Debug.Log ("pressure plate now has a weight of: " + currentweight);

		sliderBone.transform.localPosition = Vector3.Lerp (sliderStart, sliderEnd, currentweight / 6f);

		for (int i = 0; i < outputSize6.Length; i++) {
			if (outputSize6 [i] != null) {
				if (includeLower) {
					if (i < currentweight) {
						if(!outputSize6[i].enabled)
							outputSize6 [i].enabled = true;
					} else {
						if(outputSize6[i].enabled)
						outputSize6 [i].enabled = false;
					}
				} else {
					if (i + 1 == currentweight) {
						if(!outputSize6[i].enabled)
							outputSize6 [i].enabled = true;
					} else {
						if(outputSize6[i].enabled)
							outputSize6 [i].enabled = false;
					}
				}
			}
		}
	}
}
