using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : ElectronicsComponent {

	public int currentweight = 0;

	public bool includeLower = true;

	public GameObject sliderBone;
	public Vector3 sliderStart;
	public Vector3 sliderEnd;

	public ElectronicsComponent[] outputSize6;

	public PlayerManager playerMngr;
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider colli) {
		//Debug.Log (colli);
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
		if (active) {
			sliderBone.transform.localPosition = Vector3.Lerp (sliderStart, sliderEnd, currentweight / 6f);
			for (int i = 0; i < outputSize6.Length; i++) {
				if (outputSize6 [i] != null) {
					if (includeLower) {
						if (i < currentweight) {
							outputSize6 [i].setEnabled (true, this);
						} else {
							outputSize6 [i].setEnabled (false, this);
						}
					} else {
						if (i + 1 == currentweight) {
							outputSize6 [i].setEnabled (true, this);
						} else {
							outputSize6 [i].setEnabled (false, this);
						}
					}
				}
			}
		} else {
			Debug.Log (active);

			sliderBone.transform.localPosition = sliderStart;
			for (int i = 0; i < outputSize6.Length; i++) {
				if (outputSize6 [i] != null) {
					outputSize6 [i].setEnabled (false, this);
				}
			}
		}
	}

	protected override void onActivate() {
	}

	protected override void onDeActivate() {
		updateStuff ();
	}
}
