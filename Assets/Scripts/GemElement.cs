using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemElement : MonoBehaviour
{

	public float upForce = 1f;
	public float sideForce = .1f;
	private Rigidbody rigidbody;
	public GemElement instance;

	// Use this for initialization
	private void Start ()
	{
		instance = this;
		Debug.Log("AYO");
		rigidbody = GetComponent<Rigidbody>();
	}


	public void LevitationOn()
	{
		var xForce = Random.Range(0, sideForce);
		var yForce = Random.Range(upForce / 2f, upForce);
		var zForce = Random.Range(0, sideForce);
		
		var force = new Vector3(xForce, yForce, zForce);

		rigidbody.velocity = force;
		rigidbody.useGravity = false;
	}


	public void LevitationOff()
	{
		rigidbody.useGravity = true;
	}
}
