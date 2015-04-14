using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public int playerSpeed = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKey (KeyCode.V))
		{
			SetTransformZ((transform.position.z) + playerSpeed);
			
		}
		
		if(Input.GetKey (KeyCode.X))
		{
			SetTransformZ((transform.position.z) - playerSpeed);
			
		}
		
		if(Input.GetKey (KeyCode.D))
		{
			SetTransformX((transform.position.x) - playerSpeed);
		}
		
		if(Input.GetKey (KeyCode.C))
		{
			SetTransformX((transform.position.x) + playerSpeed);
		}
		
		else
		{
			GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
			
		}

		if(Input.GetKey (KeyCode.M))
		{
			transform.Rotate ( Vector3.up * ( 10.0f * Time.deltaTime ) );
		}
		if(Input.GetKey (KeyCode.N))
		{
			transform.Rotate ( Vector3.up * ( -10.0f * Time.deltaTime ) );
		}
		else
		{
			transform.Rotate ( Vector3.up * ( 0.0f * Time.deltaTime ) );
			
		}

	}

	void SetTransformX(float n)
	{
		transform.position = new Vector3(n, transform.position.y, transform.position.z);
	}
	
	void SetTransformZ(float n)
	{
		transform.position = new Vector3(transform.position.x, transform.position.y, n);
	}
}
