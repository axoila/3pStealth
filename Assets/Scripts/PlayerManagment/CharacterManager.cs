using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{

	public bool active;
	public Color characterOutline;

	public float speed;

	private Material playerMaterial;
	private Transform worldDirection;
	private CharacterController controller;

	// Use this for initialization
	void Start ()
	{
		playerMaterial = GetComponent<Renderer> ().material;
		worldDirection = Camera.main.transform;

		controller = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (active) {
			playerMaterial.SetColor ("_OutlineColor", characterOutline);

			Vector3 moveDirection = (worldDirection.forward * Input.GetAxis ("Vertical") + 
				worldDirection.right * Input.GetAxis ("Horizontal")).normalized;
			moveDirection.Scale(new Vector3(1, 0, 1));
			controller.SimpleMove (moveDirection * speed);
		} else {
			playerMaterial.SetColor ("_OutlineColor", new Color (1, 1, 1, 0));
		}
	}

	public void deActivate ()
	{
		active = false;
	}

	public void activate ()
	{
		active = true;
	}

	void OnTriggerEnter (Collider coll) {
		if(coll.gameObject.layer == LayerMask.NameToLayer("Kill")){
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
	}
}
