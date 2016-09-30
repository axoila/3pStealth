using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatTower : ElectronicsComponent {

	public float cooldown;
	public GameObject particle;

	private float timer;

	// Update is called once per frame
	void Update () {
		if (active) {
			timer += Time.deltaTime;

			if (timer >= cooldown) {
				timer = 0;
				GameObject instance = Instantiate (particle, transform.position + new Vector3 (0f, 0.02f, 0f), Quaternion.Euler (Vector3.zero));
				instance.transform.SetParent (transform);
			}
		}
	}

	protected override void onActivate() {
		transform.GetChild (0).gameObject.SetActive (true);
	}

	protected override void onDeActivate() {
		transform.GetChild (0).gameObject.SetActive (false);
	}
}
