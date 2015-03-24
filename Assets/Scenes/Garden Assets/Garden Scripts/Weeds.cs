using UnityEngine;
using System.Collections;

public class Weeds : MonoBehaviour {
	
	public Transform front;
	public float acceleration;
	public float rotationalForce;
	public float killY;
	
	public Flower collectionPoint;    // This isn't actually a flower
	public FlowerManager gameFlowers;
	public Flower target;
	public Flower tempTarget;
	public Player player;
	
	public float health;
	public float maxHealth;
	public float damage;
	public float recoil;
	public float targetHealth;
	
	public Vector3 moveDirection;
	public Vector3 desiredDirection;
	
	// Use this for initialization
	void Start () {
		WeedSpawner spawner = GameObject.FindObjectOfType<WeedSpawner>();
		
		gameFlowers = spawner.gameFlowers;
		collectionPoint = spawner.collectionPoint;
		player = spawner.player;
		
		if (gameFlowers.flowers.Length != 1) {
			target = gameFlowers.flowers[Random.Range(1, gameFlowers.flowers.Length)];
		}
		else {
			target = gameFlowers.flowers[0];
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (rigidbody.position.y < killY) {
			Destroy(this.gameObject);
		}
	
		else if (gameFlowers.flowers.Length == 1) {
			target = gameFlowers.flowers[0];
		}
		
		if (gameFlowers.flowers.Length != 1 && target.health <= 0) {
			target = gameFlowers.flowers[Random.Range(1, gameFlowers.flowers.Length)];
		}
		
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
	
	public void OnCollisionEnter(Collision col) {
		if (col.gameObject.name == target.name) {
			target.StartCoroutine("GetHit", damage);
			//TakeDamage (1000);	// probably optional, if I don't do this, I should find a new target
			rigidbody.AddForce(-moveDirection * acceleration * recoil * Time.deltaTime);
		}
		
		
		//target = gameFlowers.flowers[Random.Range(0, gameFlowers.flowers.Length)];
		
	}
	
	public void TakeDamage(float damage) {
		health -= damage;
		
		if (health <= 0) {
			Destroy(this.gameObject);
		}
	}
}
