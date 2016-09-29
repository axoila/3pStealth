using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	public GameObject[] characters;
	public CameraManager cam;
	public int activeCharacter;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//cycle through characters
		if (Input.GetAxis ("Mouse ScrollWheel") > 0 || Input.GetButtonDown("Next Character")) {
			characters [activeCharacter].GetComponent<CharacterManager> ().deActivate ();
			activeCharacter++;
			if (activeCharacter >= characters.Length)
				activeCharacter = 0;
			characters [activeCharacter].GetComponent<CharacterManager> ().activate ();
			cam.player = characters [activeCharacter].transform;
		}
		if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
			characters [activeCharacter].GetComponent<CharacterManager> ().deActivate ();
			activeCharacter--;
			if (activeCharacter < 0)
				activeCharacter = characters.Length-1;
			characters [activeCharacter].GetComponent<CharacterManager> ().activate ();
			cam.player = characters [activeCharacter].transform;
		}
	}
}
