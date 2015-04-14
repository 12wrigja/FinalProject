/*****************************************************
 * Weeds are instantiated in the WeedSpawner script
 *****************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
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
	
	public float cashValue;
	public float health;
	public float maxHealth;
	public float damage;
	public float recoil;
	public float targetHealth;
	public Slider healthbar;
	public Canvas canvas;
	
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
		
		canvas.transform.LookAt(new Vector3(0, 37 *100, -18 *200));
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
		if (col.gameObject == target.gameObject) {
			if (target.canTakeDamage) {
				target.StartCoroutine("GetHit", damage);
			}
			//TakeDamage (1000);	// probably optional, if I don't do this, I should find a new target
			rigidbody.AddForce(-moveDirection * acceleration * recoil * Time.deltaTime);
		}
		
		if (col.gameObject.name == "Wall(Clone)") {
			Wall w = (Wall)col.gameObject.GetComponent("Wall");
			
			w.TakeDamage(damage);
			rigidbody.AddForce(-moveDirection * acceleration * recoil * Time.deltaTime);
		}
		
		if (col.gameObject.name == "Flower1(Clone)" || col.gameObject.name == "Flower2(Clone)") {
			Flower f = (Flower)col.gameObject.GetComponent("Flower");
			Debug.Log ("did it work");
			f.StartCoroutine("GetHit", damage);
			rigidbody.AddForce(-moveDirection * acceleration * recoil * Time.deltaTime);
		}
		
		
		//target = gameFlowers.flowers[Random.Range(0, gameFlowers.flowers.Length)];
		
	}
	
	public void TakeDamage(float damage) {
		health -= damage;
		healthbar.value =  health / maxHealth * healthbar.maxValue;
		if (health <= 0) {
			canvas.enabled = false;
			gameFlowers.cash += cashValue;
			Destroy(this.gameObject);
		}
	}
}
