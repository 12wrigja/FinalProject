using UnityEngine;
using System.Collections;

public class Flower : MonoBehaviour {

	public bool pickedUp;
	public float health;
	public float maxHealth;
	public float hitDelay;
	public float dyingTime;
	
	public FlowerManager gameFlowers;
	
	public Animator anim;

	// Use this for initialization
	public void Start() {
		gameFlowers = GameObject.FindObjectOfType<FlowerManager>();
		
		health = maxHealth;
	}
	
	// Update is called once per frame
	public void Update() {
		if (this.health <= 0) {
			
			Flower[] newArray = new Flower[gameFlowers.flowers.Length - 1];
			for (int i = 0, j = 0; j < newArray.Length; i++, j++) {
				if (gameFlowers.flowers[i] == this) {
					i++;
				}
				newArray[j] = gameFlowers.flowers[i];
			}
			
			gameFlowers.flowers = newArray;
			
			Destroy(this.gameObject);
			anim.SetTrigger ("Die");
		}
	}
	
	public IEnumerator GetHit(float damage) {
		health -= damage;
		anim.SetTrigger("Hit");
		yield return new WaitForSeconds(hitDelay);
	}
	
	public void OnTriggerStay() {
		if (Input.GetMouseButtonDown(0)) {
			pickedUp = !pickedUp;
		}
	}
}
