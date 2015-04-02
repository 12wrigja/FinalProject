using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {
	public float health;
	public float maxHealth;
	
	public float cashCost;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void TakeDamage(float damage) {
		health -= damage;
		Debug.Log("Walls hit");
		
		if (health <= 0) {
			Destroy(this.gameObject);
		}
	}
}
