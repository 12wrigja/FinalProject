using UnityEngine;
using System.Collections;

public class Flower : MonoBehaviour {

	public bool pickedUp;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnTriggerStay() {
		if (Input.GetMouseButtonDown(0)) {
			pickedUp = !pickedUp;
		}
	}
}
