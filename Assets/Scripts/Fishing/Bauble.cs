using UnityEngine;
using System.Collections;

public class Bauble : MonoBehaviour {

	public Rod rod;
	public Vector3 poolPosition;
	public GameObject fakeBauble;

	private GameObject fakeBaubleInstance;

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void toPool(){
		this.GetComponent<MeshRenderer> ().enabled = false;
		fakeBaubleInstance = Instantiate (fakeBauble) as GameObject;
		fakeBaubleInstance.transform.position = poolPosition;
	}

	public void toRod(){
		this.GetComponent<MeshRenderer> ().enabled = true;
		Destroy (fakeBaubleInstance.gameObject);
	}
}
