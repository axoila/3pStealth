using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wire : MonoBehaviour {

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
				script.enabled = active;
		} else {
			foreach (MonoBehaviour script in modified)
				script.enabled = !active;
		}

		foreach (Renderer wirePiece in wireObject) {
			wirePiece.material = offMat;
		}
	}

	void OnEnable() {
		active = true;
		if (!inverted) {
			foreach (MonoBehaviour script in modified)
				script.enabled = active;
		} else {
			foreach (MonoBehaviour script in modified)
				script.enabled = !active;
		}

		foreach (Renderer wirePiece in wireObject) {
			wirePiece.material = onMat;
		}
	}
}
