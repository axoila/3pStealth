using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatEmit : MonoBehaviour {
	public float extentionTime;

	public float minScale;
	public float maxScale;

	public float verticalScale;

	public float timer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer > extentionTime)
			Destroy (gameObject);

		transform.localScale = new Vector3 (Mathf.Lerp(minScale, maxScale, timer / extentionTime), verticalScale, Mathf.Lerp(minScale, maxScale, timer / extentionTime));
	}
}
