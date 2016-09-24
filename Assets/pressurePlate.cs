using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressurePlate : MonoBehaviour {

	public int currentweight = 0;

	public bool includeLower = true;

	public GameObject sliderBone;
	public Vector3 sliderStart;
	public Vector3 sliderEnd;

	public MonoBehaviour[] outputSize6;

	public playerManager playerMngr;

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
		sliderBone.transform.localPosition = Vector3.Lerp (sliderStart, sliderEnd, currentweight / 6f);

		for (int i = 0; i < outputSize6.Length; i++) {
			if (outputSize6 [i] != null) {
				if (includeLower) {
					if (i < currentweight) {
						outputSize6 [i].enabled = true;
					} else {
						outputSize6 [i].enabled = false;
					}
				} else {
					if (i + 1 == currentweight) {
						outputSize6 [i].enabled = true;
					} else {
						outputSize6 [i].enabled = false;
					}
				}
			}
		}

		/*if (outputSize6[0] != null) {
			if (currentweight > 0)
				outputSize6[0].enabled = true;
			else
				outputSize6[0].enabled = false;
		}*/
	}
}
