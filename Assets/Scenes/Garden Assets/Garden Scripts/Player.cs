using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public GameObject shovel;
	public Animator anim;
	public Vector2 screenSize;
	public Vector2 offset;
	public float damage;
	

	// Use this for initialization
	void Start() {
	
	}
	
	// Update is called once per frame
	void Update() {
	
		// Sets the position of the shovel to the position of the mouse based on the parameters screenSize and offset
		Vector3 newPosition = new Vector3(Input.mousePosition.x / screenSize.x, shovel.transform.position.y, Input.mousePosition.y / screenSize.y);
		shovel.transform.position = newPosition - new Vector3(screenSize.x - offset.x, 0f, screenSize.y - offset.y); 
		
		// Swings the shovel when the mouse is clicked (left click)
		if (Input.GetMouseButtonDown(0)) {
			Swing();
		}
	}
	
	public void Swing() {
		anim.SetTrigger("Swing");
	}
}
