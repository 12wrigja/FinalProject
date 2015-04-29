using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButterflyHolder : MonoBehaviour {
	private float timeToChangeHorizontalDirection;
	private float timeToChangeVerticalDirection;
	private Vector3 currentHorizontalDirection;
	private Vector3 currentVerticalDirection;
	private float xDirectionLimitPositive;
	private float xDirectionLimitNegative;
	private float yDirectionLimitPositive;
	private float yDirectionLimitNegative;
	private float zDirectionLimitPositive;
	private float zDirectionLimitNegative;
	private float horizontalCorrectionSpeed = 3.0f;
	private float verticalCorrectionSpeed = 0.5f;

	public void Start () {
		xDirectionLimitPositive = ButterflyMachine.xLimitPositive;
		xDirectionLimitNegative = ButterflyMachine.xLimitNegative;
		yDirectionLimitPositive = ButterflyMachine.yLimitPositive;
		yDirectionLimitNegative = ButterflyMachine.yLimitNegative;
		zDirectionLimitPositive = ButterflyMachine.zLimitPositive;
		zDirectionLimitNegative = ButterflyMachine.zLimitNegative;
		timeToChangeHorizontalDirection = Random.Range (0f, 3f);
		timeToChangeVerticalDirection = Random.Range (0f, 0.5f);
	}
	
	public void Update () {
		timeToChangeHorizontalDirection -= Time.deltaTime;
		timeToChangeVerticalDirection -= Time.deltaTime;
		if (timeToChangeHorizontalDirection <= 0) {
			currentHorizontalDirection = ChangeHorizontalDirection();
		}
		if (timeToChangeVerticalDirection <= 0) {
			currentVerticalDirection = ChangeVerticalDirection ();
		}
		GetComponent<Rigidbody>().velocity = currentHorizontalDirection + currentVerticalDirection;
		transform.rotation = Quaternion.LookRotation (GetComponent<Rigidbody>().velocity);
	}

	public void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "butterflynet") {
			//Destroy(this.gameObject);
			this.gameObject.SetActive (false);
			ButterflyMachine.butterflyCaught ();
		}
	}

	public void playerWon() {
		this.verticalCorrectionSpeed = 1.0f;
		this.yDirectionLimitNegative = 99;
		this.yDirectionLimitPositive = 100;
		Destroy (this.gameObject, 15);
	}
	
	private Vector3 ChangeHorizontalDirection() {
		if (transform.position.x < xDirectionLimitNegative) {
			timeToChangeHorizontalDirection = 1f;
			return horizontalCorrectionSpeed * (new Vector3 (1f, 0f, 0f));
		}
		if (transform.position.x > xDirectionLimitPositive) {
			timeToChangeHorizontalDirection = 1f;
			return horizontalCorrectionSpeed * (new Vector3 (-1f, 0f, 0f));
		}
		if (transform.position.z < zDirectionLimitNegative) {
			timeToChangeHorizontalDirection = 1f;
			return horizontalCorrectionSpeed * (new Vector3 (0f, 0f, 1f));
		}
		if (transform.position.z > zDirectionLimitPositive) {
			timeToChangeHorizontalDirection = 1f;
			return horizontalCorrectionSpeed * (new Vector3 (0f, 0f, -1f));
		}
		float xDirection = Random.Range (-1f, 1f);
		float zDirection = Random.Range (-1f, 1f);
		timeToChangeHorizontalDirection = Random.Range (0f, 3f);
		float speed = Random.Range (0f, 3f);
		return speed * (new Vector3 (xDirection, 0f, zDirection));
	}

	private Vector3 ChangeVerticalDirection() {
		if (transform.position.y < yDirectionLimitNegative) {
			timeToChangeVerticalDirection = 0.25f;
			return verticalCorrectionSpeed * (new Vector3 (0f, 1f, 0f));
		}
		if (transform.position.y > yDirectionLimitPositive) {
			timeToChangeVerticalDirection = 0.25f;
			return verticalCorrectionSpeed * (new Vector3 (0f, -1f, 0f));
		}
		float yDirection = Random.Range (-1f, 1f);
		timeToChangeVerticalDirection = Random.Range (0f, 0.25f);
		float speed = Random.Range (0f, 1f);
		return speed * (new Vector3 (0f, yDirection, 0f));
	}
}