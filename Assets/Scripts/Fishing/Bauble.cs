using UnityEngine;
using System.Collections;

public class Bauble : MonoBehaviour {

	public Rod rod;
	public Vector3 poolPosition;

	private Transform rodPosition;

	void Start () {
		rodPosition = this.gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void toPool(){
		rodPosition = this.gameObject.transform;
		this.gameObject.transform.SetParent (null);
		this.gameObject.transform.position = poolPosition;
	}

	public void toRod(){
		this.gameObject.transform.SetParent(rod.transform);
		this.gameObject.transform.localPosition = rodPosition.localPosition;
		this.gameObject.transform.localRotation = rodPosition.localRotation;
		this.gameObject.transform.localScale = rodPosition.localScale;
	}
}
