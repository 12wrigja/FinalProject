using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public GameObject shovel;
	public Vector2 screenSize;
	public Vector2 offset;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPosition = new Vector3(Input.mousePosition.x / screenSize.x, shovel.transform.position.y, Input.mousePosition.y / screenSize.y);
		shovel.transform.position = newPosition - new Vector3(screenSize.x - offset.x, 0f, screenSize.y - offset.y); 
		Debug.Log ("mouse position: " + Input.mousePosition.x + ", " + Input.mousePosition.y);
		//Debug.Log ("shovel position: " + shovel.transform.position.x + ", " +shovel.transform.position.y);
	}
}
