/**
 * parent class for all electronic related scripts
 * when inheriting do NOT use the Start() method 
 * or call base.Start() in your Start() method
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElectronicsComponent : MonoBehaviour {

	[SerializeField] private bool selfPowered;

	public bool active {get; private set;}

	private List<ElectronicsComponent> powerSupplier;

	void Start () {
		Debug.Log ("init component");

		if (selfPowered) {
			active = true;
			onActivate ();
		} else {
			active = false;
			onDeActivate ();
		}
		powerSupplier = new List<ElectronicsComponent> ();
	}

	void Update(){
	}

	public void setEnabled (bool enabled, ElectronicsComponent supplier) {
		if (enabled) {
			powerSupplier.Add (supplier);
			if (powerSupplier.Count == 1)
				onActivate ();
		} else {
			powerSupplier.Remove (supplier);
			if (powerSupplier.Count == 0)
				onDeActivate ();
		}
	}

	protected abstract void onActivate ();

	protected abstract void onDeActivate ();
}
