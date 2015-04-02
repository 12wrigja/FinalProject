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
		// Swings the shovel when the mouse is clicked (left click)
		if (Input.GetMouseButtonDown(0)) {
			Swing();
		}
		
		float xPos = Input.mousePosition.x / screenSize.x + offset.x;
		float yPos = shovel.transform.position.y;
		float zPos = Input.mousePosition.y / screenSize.y + offset.y;
			
			// Sets the position of the shovel to the position of the mouse based on the parameters screenSize and offset
		if (xPos > 19) {
			xPos = 19;
		}
		if (xPos < -19) {
			xPos = -19;
		}
		if (zPos > 15) {
			zPos = 15;
		}
		if (zPos < -20) {
			zPos = -20;
		}
		
		Vector3 newPosition = new Vector3(xPos, yPos, zPos);
		shovel.transform.position = newPosition;
	}
	
	public void Swing() {
		anim.SetTrigger("Swing");
	}
}
