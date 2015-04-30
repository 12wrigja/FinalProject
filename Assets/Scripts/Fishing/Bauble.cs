using UnityEngine;
using System.Collections;

public class Bauble : MonoBehaviour {

	public Rod rod;
	public Vector3 poolPosition;
	public GameObject fakeBauble;

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void toPool(){
		this.GetComponent<MeshRenderer> ().enabled = false;
		fakeBauble.GetComponent<MeshRenderer> ().enabled = true;
	}

	public void toRod(){
		fakeBauble.GetComponent<MeshRenderer> ().enabled = false;
		this.GetComponent<MeshRenderer> ().enabled = true;
	}

	public Animator getAnimator(){
		return fakeBauble.GetComponent<Animator> ();
	}
}
