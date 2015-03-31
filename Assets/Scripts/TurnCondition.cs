using UnityEngine;
using System.Collections;

public class TurnCondition : MonoBehaviour {

	public Animator animator;
	public bool isPressed = false;
	private bool currentlyTurned = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Space") && !currentlyTurned){
			isPressed = true;
			currentlyTurned = true;
		}
		else if(Input.GetButtonDown("Space") && currentlyTurned){
			isPressed = false;
			currentlyTurned = false;
		}
		animator.SetBool ("isPressed", isPressed);
	}
}
