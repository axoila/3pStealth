using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour {

	public bool active;
	public Transform endPoint;
	public MonoBehaviour[] modified;
	public bool inverted = false;

	public Renderer[] wireObject;

	public Material offMat;
	public Material onMat;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnDisable() {
		active = false;
		if (!inverted) {
			foreach (MonoBehaviour script in modified)
				script.enabled = false;
		} else {
			foreach (MonoBehaviour script in modified) {
				script.enabled = true;
			}
		}

		foreach (Renderer wirePiece in wireObject) {
			wirePiece.material = offMat;
		}

		Debug.Log ("DISABLE WIRE");
	}

	void OnEnable() {
		active = true;
		if (!inverted) {
			foreach (MonoBehaviour script in modified)
				script.enabled = true;
		} else {
			foreach (MonoBehaviour script in modified)
				script.enabled = false;
		}

		foreach (Renderer wirePiece in wireObject) {
			wirePiece.material = onMat;
		}
		Debug.Log ("ENABLE WIRE");
	}
}
