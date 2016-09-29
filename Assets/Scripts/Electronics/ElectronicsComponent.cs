using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronicsComponent : MonoBehaviour {

	public bool selfPowered;

	private int powered = 0; //how many other components power this one
	private bool active = false;

	private List<ElectronicsComponent> powerSupplier;

	// Use this for initialization
	void Start () {
		if (selfPowered)
			active = true;
		powerSupplier = new List<ElectronicsComponent> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void disable () {

	}

	public void enable (ElectronicsComponent supplier) {
	}
}
