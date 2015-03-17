using UnityEngine;
using System.Collections;

public class Weeds : MonoBehaviour {
	
	public Transform front;
	public float acceleration;
	public float rotationalForce;
	
	public FlowerManager gameFlowers;
	public GameObject target;
	public GameObject tempTarget;
	public Player player;
	
	public float health;
	public float maxHealth;
	public float damage;
	
	public Vector3 moveDirection;
	public Vector3 desiredDirection;
	
	// Use this for initialization
	void Start () {
		target = gameFlowers.flowers[Random.Range(0, gameFlowers.flowers.Length)];
	}
	
	// Update is called once per frame
	void Update () {
		Turn();
	}
	
	private void Turn() {
		moveDirection = (new Vector3(front.position.x, front.position.y, front.position.z) - 
						 new Vector3(transform.position.x, transform.position.y, transform.position.z)).normalized;
		
		desiredDirection = (target.transform.position - transform.position).normalized;
		
		float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) - Mathf.Atan2(desiredDirection.x, desiredDirection.z);
		
		if (angle > 0 && angle < Mathf.PI || angle < -Mathf.PI) {
			rigidbody.AddTorque(0, -rotationalForce, 0);
		}
		else {
			rigidbody.AddTorque(0, rotationalForce, 0);
		}
		
		rigidbody.AddForce(moveDirection * acceleration * Time.deltaTime);
	}
	
	public void OnTriggerStay() {
		if (Input.GetMouseButtonDown(0)) {
			TakeDamage(player.damage);
		}
	}
	
	public void TakeDamage(float damage) {
		health -= damage;
		
		if (health <= 0) {
			Destroy(this.gameObject);
		}
	}
}
