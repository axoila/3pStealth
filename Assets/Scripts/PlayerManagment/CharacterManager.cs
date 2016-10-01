using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
	public bool active;
	public Color characterOutline;

	[SerializeField] private float friction = 0.1f;
	public float speed;
	[SerializeField] private float gravityScale;

	private Material playerMaterial;
	private Transform worldDirection;
	private Rigidbody rigid;

	private Vector3 moveDirection = Vector3.zero;

	// Use this for initialization
	void Start ()
	{
		playerMaterial = GetComponent<Renderer> ().material;
		worldDirection = Camera.main.transform;

		rigid = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//friction
		rigid.velocity *= 1-(friction * Time.deltaTime);

		if (active) {
			playerMaterial.SetColor ("_OutlineColor", characterOutline);

			moveDirection = (worldDirection.forward * Input.GetAxis ("Vertical")) + 
				(worldDirection.right * Input.GetAxis ("Horizontal"));
			moveDirection.Scale(new Vector3(1, 0, 1));
			moveDirection.Normalize ();
			moveDirection *= speed;

			moveDirection.y = Mathf.Clamp (rigid.velocity.y + Physics.gravity.y * gravityScale * Time.deltaTime, -9999999, 0);
			rigid.velocity = (moveDirection);
		} else {
			playerMaterial.SetColor ("_OutlineColor", new Color (1, 1, 1, 0));
		}
	}

	public void DeActivate ()
	{
		active = false;
	}

	public void Activate ()
	{
		active = true;
	}

	void OnTriggerEnter (Collider coll) {
		if(coll.gameObject.layer == LayerMask.NameToLayer("Kill")){
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
	}
}
