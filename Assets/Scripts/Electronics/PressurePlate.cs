using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : ElectronicsComponent {

	public int Currentweight { private set; get;}

	[SerializeField] private bool includeLower = true;

	[SerializeField] private GameObject sliderBone;
	[SerializeField] private Vector3 sliderStart;
	[SerializeField] private Vector3 sliderEnd;

	public ElectronicsComponent[] outputSize6;

	public PlayerManager playerMngr;

	void OnTriggerEnter (Collider colli)
    {
		//Debug.Log (colli);
		if (colli.gameObject == playerMngr.characters [0])
			Currentweight += 1;
		if (colli.gameObject == playerMngr.characters [1])
			Currentweight += 2;
		if (colli.gameObject == playerMngr.characters [2])
			Currentweight += 3;

		UpdateStuff ();
	}

	void OnTriggerExit (Collider colli)
    {
		//Debug.Log (colli);
		if (colli.gameObject == playerMngr.characters [0])
			Currentweight -= 1;
		if (colli.gameObject == playerMngr.characters [1])
			Currentweight -= 2;
		if (colli.gameObject == playerMngr.characters [2])
			Currentweight -= 3;

		UpdateStuff ();
	}

	void UpdateStuff () {
		if (Active) {
			sliderBone.transform.localPosition = Vector3.Lerp (sliderStart, sliderEnd, Currentweight / 6f);
			for (int i = 0; i < outputSize6.Length; i++)
            {
				if (outputSize6 [i] != null)
                {
					if (includeLower) {
						if (i < Currentweight)
                        {
							outputSize6 [i].SetEnabled (true, this);
						}
                        else
                        {
							outputSize6 [i].SetEnabled (false, this);
						}
					}
                    else
                    {
						if (i + 1 == Currentweight)
                        {
							outputSize6 [i].SetEnabled (true, this);
						}
                        else
                        {
							outputSize6 [i].SetEnabled (false, this);
						}
					}
				}
			}
		}
        else
        {
			Debug.Log (Active);

			sliderBone.transform.localPosition = sliderStart;
			for (int i = 0; i < outputSize6.Length; i++)
            {
				if (outputSize6 [i] != null)
                {
					outputSize6 [i].SetEnabled (false, this);
				}
			}
		}
	}

	protected override void OnActivate() {
	}

	protected override void OnDeActivate() {
		UpdateStuff ();
	}
}
