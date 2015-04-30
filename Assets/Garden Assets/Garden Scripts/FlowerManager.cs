using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlowerManager : MonoBehaviour {
	public float cash;
	
	public Text currentMoney;
	public Text flowerValue;
	public Text wallValue;
	public Text winnerText;
	public Text flowersText;
	public Text timer;
	
	public int waveSpawnDelayInWinningState;
	public float winTimeInitial;
	public float winTimeCurrent;
	public int flowerThreshhold;
	public bool canPlaceFlower;
	public bool canPlaceWall;
	public bool verticalWall;
	public bool winning;
	public float flowerDistance;
	public Player player;
	public Flower collectionPoint;
	public Wall wallPrefab;
	public Wall vertWallPrefab;
	public WeedSpawner weedSpawner;
	
	public Flower[] flowers;
	
	public Flower[] flowerPrefabs;
	private int flowerType;
	
	
	// Use this for initialization
	public void Start() {
		winnerText.enabled = false;
		timer.enabled = false;
	
		canPlaceFlower = false;
		canPlaceWall = false;
		
		Flower[] newArray = new Flower[flowers.Length + 1];
		
		int i;
		for (i = 0; i < flowers.Length; i++) {
			newArray[i] = flowers[i];
		}
		newArray[i] = collectionPoint;
		flowers = newArray;
		
		currentMoney.text = "" + cash;
		flowerValue.text = "" + flowerPrefabs[0].cashCost;
		wallValue.text = "" + wallPrefab.cashCost;
	}
	
	// Update is called once per frame
	public void Update() {
		
		flowersText.text = "Flowers: " + (flowers.Length - 1) + "/" + flowerThreshhold;
		currentMoney.text = "" + cash;
		
		if (canPlaceFlower && Input.GetMouseButtonDown(1)) {
			bool flowersHere = false;
			
			for (int i = 1; i < flowers.Length; i++) {
				
				// Done by measuring distance
				Vector3 shovelPos = new Vector3(player.shovel.transform.position.x, 0f, player.shovel.transform.position.z + 7);
				Vector3 flowerPos = new Vector3(flowers[i].transform.position.x, 0f, flowers[i].transform.position.z);
				if ((shovelPos - flowerPos).magnitude < flowerDistance) {
					flowersHere = true;
				}
			}
			
			if (!flowersHere) {
				Flower newFlower = Instantiate(flowerPrefabs[flowerType]) as Flower;
			
				PlaceFlower(newFlower);
			}
		}
		
		// This used to cause problems with healthbars on walls and other surrounding healthbars
		// It was fixed changing the slider navigation from automatic to none 
		// 
		// The only other problem with vertical walls is it makes the healthbar get really squished
		// Something to do with rotating, maybe try to avoid rotating in the switch from horizontal to vertical
		// Whoa, I could have a seperate prefab that is a vertical wall. I think I'll do that.
		if (Input.GetKey (KeyCode.A)) {
			verticalWall = true;
		}
		else {
			verticalWall = false;
		}
		
		if (canPlaceWall && Input.GetMouseButtonDown(1)) {
			
			PlaceWall(verticalWall);
		}
		
		
		
		/****************************************************************/
		// How winning the game works
		if (flowers.Length >= flowerThreshhold + 1) {
			
			// Trying to get multiple wave spawns to work
			/*if (winning && (int)winTimeCurrent % waveSpawnDelayInWinningState == 0 && (int)winTimeCurrent != 0) {
				winning = false;
			}
			else {
				winning = true;
			}*/
			
			if (!winning) {
				weedSpawner.SpawnWave(); // to make it more interesting
			}
			
			//if ((int)winTimeCurrent % waveSpawnDelayInWinningState != 0) {
				winning = true;
			//}
			timer.enabled = true;
			winTimeCurrent -= Time.deltaTime;
			timer.text = "" + (int)winTimeCurrent;
			

			if (winTimeCurrent <= 0) {
				// This is where you win
				winTimeCurrent = 0;
				winnerText.enabled = true;
				flowers = new Flower[1];
				flowers[0] = collectionPoint;
				weedSpawner.spawnChance = 0;
                AnxietySystem.setAnxiety(25);
			}
		}
		else {
			winTimeCurrent = winTimeInitial;
			timer.enabled = false;
			winning = false;
		}
		/****************************************************************/
	}
	
	public void SelectFlower() {
		if (flowerPrefabs[0].cashCost <= cash) {
			canPlaceFlower = true;
			canPlaceWall = false;
			flowerType = Random.Range(0, flowerPrefabs.Length);
		}
	}
	
	public void SelectWall() {
		if (wallPrefab.cashCost <= cash) {
			canPlaceWall = true;
			canPlaceFlower = false;
		}
	}
	
	public void PlaceFlower(Flower newFlower) {
		Flower[] newArray = new Flower[flowers.Length + 1];
		
		int i;
		for (i = 0; i < flowers.Length; i++) {
			newArray[i] = flowers[i];
		}
		newArray[i] = newFlower;
		flowers = newArray;
		
		canPlaceFlower = false;
		cash -= flowerPrefabs[0].cashCost;
		
		flowers[i].transform.position = new Vector3((float)(player.shovel.transform.position.x), 2.5f, (float)(player.shovel.transform.position.z + 8));
	}
	
	public void PlaceWall( bool vertical) {
		canPlaceWall = false;
		cash -= wallPrefab.cashCost;
		if (!vertical) {
			Wall wall = Instantiate(wallPrefab) as Wall;
			wall.transform.position = new Vector3((float)(player.shovel.transform.position.x), 1.5f, (float)(player.shovel.transform.position.z + 8));
		}
		else {
			Wall wall = Instantiate(vertWallPrefab) as Wall;
			wall.transform.position = new Vector3((float)(player.shovel.transform.position.x), 1.5f, (float)(player.shovel.transform.position.z + 8));
		}
	}
}
