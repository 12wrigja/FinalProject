/*****************************************************
 * Walls are instantiated in the FlowerManager script
 *****************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Wall : MonoBehaviour {
	public float health;
	public float maxHealth;
	public Canvas canvas;
	public Slider healthbar;
	
	public float cashCost;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		canvas.transform.LookAt(new Vector3(0, 37 *100, -18 *200));
	}
	
	public void TakeDamage(float damage) {
		health -= damage;
		healthbar.value =  health / maxHealth * healthbar.maxValue;
		if (health <= 0) {
			canvas.enabled = false;
			Destroy(this.gameObject);
		}
	}
}
