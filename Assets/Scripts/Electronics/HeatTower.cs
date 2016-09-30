using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatTower : MonoBehaviour {

	public float cooldown;
	public GameObject particle;

	private float timer;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (timer >= cooldown) {
			timer = 0;
			GameObject instance = Instantiate (particle, transform.position + new Vector3 (0f, 0.02f, 0f), Quaternion.Euler(Vector3.zero));
			instance.transform.SetParent (transform);
		}
	}

	void OnDisable() {
		//disables the light
		transform.GetChild (0).gameObject.SetActive (false);

	}

	void OnEnable() {
		//enables the light
		transform.GetChild (0).gameObject.SetActive (true);
	}
}
