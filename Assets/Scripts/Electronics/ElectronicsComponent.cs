/**
 * parent class for all electronic related scripts
 * when inheriting do NOT use the Start() method 
 * or call base.Start() in your Start() method
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElectronicsComponent : MonoBehaviour
{

	[SerializeField] private bool selfPowered;
	//selfPowered only descripbes the starting situation, when the component is later turned on and off it can be deactivated

	public bool active { get; private set; }

	private List<ElectronicsComponent> powerSupplier;

	// I repeat: do NOT put a Start() method in a child class
	void Start ()
	{
		//init power state
		if (selfPowered) {
			active = true;
			onActivate ();
		} else {
			active = false;
			onDeActivate ();
		}

		//init list of all powerSuppliers
		powerSupplier = new List<ElectronicsComponent> ();
	}

	public void setEnabled (bool enabled, ElectronicsComponent supplier)
	{
		if (enabled) {
			if (!powerSupplier.Contains (supplier)) {
				powerSupplier.Add (supplier);
				if (powerSupplier.Count == 1) {
					active = true;
					onActivate ();
				}
			}
		} else {
			powerSupplier.Remove (supplier);
			if (powerSupplier.Count == 0) {
				active = false;
				onDeActivate ();
			}
		}
	}

	protected abstract void onActivate ();

	protected abstract void onDeActivate ();
}
