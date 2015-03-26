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
		/*float x = 
		
		// Sets the position of the shovel to the position of the mouse based on the parameters screenSize and offset
		if (shovel.transform.position.x >= 20) {
			//shovel.transform.position.x = 20;
		}
		if (shovel.transform.position.x <= -20) {
			//shovel.transform.position.x = -20;
		}
		if (shovel.transform.position.z >= 15) {
			//shovel.transform.position.y = 15;
		}
		if (shovel.transform.position.z <= -25) {
			//shovel.transform.position.y = 25;
		}
		
		else {
			Vector3 newPosition = new Vector3(Input.mousePosition.x / screenSize.x, shovel.transform.position.y, Input.mousePosition.y / screenSize.y);
		}
		shovel.transform.position = newPosition - new Vector3(screenSize.x - offset.x, 0f, screenSize.y - offset.y); 
		
		// Swings the shovel when the mouse is clicked (left click)
		if (Input.GetMouseButtonDown(0)) {
			Swing();
		}*/
		
		
		
		
		
		float xPos = Input.mousePosition.x / screenSize.x + offset.x;
		float yPos = shovel.transform.position.y;
		float zPos = Input.mousePosition.y / screenSize.y + offset.y;
			
			// Sets the position of the shovel to the position of the mouse based on the parameters screenSize and offset
		if (xPos > 20) {
			xPos = 20;
		}
		if (xPos < -20) {
			xPos = -20;
		}
		if (zPos > 15) {
			zPos = 15;
		}
		if (zPos < -25) {
			zPos = -25;
		}
		
		Vector3 newPosition = new Vector3(xPos, yPos, zPos);
		shovel.transform.position = newPosition;
	}
	
	public void Swing() {
		anim.SetTrigger("Swing");
	}
}
