using UnityEngine;
using System.Collections;

public class Flower : MonoBehaviour {

	public float health;
	public float maxHealth;
	public float hitDelay;
	public float dyingTime;
	public bool dead;
	public bool canTakeDamage;
	
	public FlowerManager gameFlowers;
	
	public Animator anim;

	// Use this for initialization
	public void Start() {
		gameFlowers = GameObject.FindObjectOfType<FlowerManager>();
		canTakeDamage = true;
		health = maxHealth;
	}
	
	// Update is called once per frame
	public void Update() {
		if (this.health <= 0 && !dead) {
			dead = true;
			StartCoroutine("Die");
			
			//anim.SetTrigger ("Die");
			//Destroy(this.gameObject);
			
		}
	}
	
	public IEnumerator GetHit(float damage) {
		canTakeDamage = false;
		health -= damage;
		anim.SetTrigger("Hit");
		yield return new WaitForSeconds(hitDelay);
		canTakeDamage = true;
	}
	
	public IEnumerator Die() {
		anim.SetTrigger ("Die");
		
		Flower[] newArray = new Flower[gameFlowers.flowers.Length - 1];
		for (int i = 0, j = 0; j < newArray.Length; i++, j++) {
			if (gameFlowers.flowers[i] == this) {
				i++;
			}
			newArray[j] = gameFlowers.flowers[i];
		}
		
		gameFlowers.flowers = newArray;
		
		yield return new WaitForSeconds(dyingTime);
		
		Destroy(this.gameObject);
	}
}
