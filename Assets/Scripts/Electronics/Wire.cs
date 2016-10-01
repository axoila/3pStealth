using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : ElectronicsComponent {

	//public Transform endPoint;
	[SerializeField] private ElectronicsComponent[] modified;
	[SerializeField] private bool inverted = false;

	[SerializeField] private Renderer[] wireObject;
	[SerializeField] private Material offMat;
	[SerializeField] private Material onMat;
	
	// Update is called once per frame
	void Update () {
	}

	protected override void OnActivate()
    {
		//Debug.Log ("activate");

		foreach (ElectronicsComponent component in modified)
			if (inverted)
				component.SetEnabled (!Active, this);
			else
				component.SetEnabled (Active, this);

		foreach (Renderer wirePiece in wireObject)
        {
			wirePiece.material = onMat;
		}
	}

	protected override void OnDeActivate()
    {
		//Debug.Log ("deactivate");
		foreach (ElectronicsComponent component in modified)
			if (inverted)
				component.SetEnabled (!Active, this);
			else
				component.SetEnabled (Active, this);

		foreach (Renderer wirePiece in wireObject)
        {
			wirePiece.material = offMat;
		}
	}
}
