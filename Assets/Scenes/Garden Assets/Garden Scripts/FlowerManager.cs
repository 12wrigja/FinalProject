using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlowerManager : MonoBehaviour {
	public float cash;
	
	public Text currentMoney;
	public Text flowerValue;
	public Text wallValue;
	
	public bool canPlaceFlower;
	public bool canPlaceWall;
	public bool verticalWall;
	public float flowerDistance;
	public Player player;
	public Flower collectionPoint;
	public Wall wallPrefab;
	
	public Flower[] flowers;
	
	public Flower[] flowerPrefabs;
	private int flowerType;
	
	
	
	// Use this for initialization
	public void Start() {
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
	
		currentMoney.text = "" + cash;
		
		if (canPlaceFlower && Input.GetMouseButtonDown(1)) {
			bool flowersHere = false;
			
			for (int i = 1; i < flowers.Length; i++) {
				
				// Done by comparing positions
				/*if (Mathf.Abs (player.shovel.transform.position.x - flowers[i].transform.position.x) < flowerDistance &&
					Mathf.Abs (player.shovel.transform.position.z + 3 - flowers[i].transform.position.z) < flowerDistance) {
					
					flowersHere = true;
				}*/
				
				// Done by measuring distance
				Vector3 shovelPos = new Vector3(player.shovel.transform.position.x, 0f, player.shovel.transform.position.z + 3);
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
		
		if (Input.GetKey (KeyCode.A)) {
			verticalWall = true;
		}
		else {
			verticalWall = false;
		}
		
		if (canPlaceWall && Input.GetMouseButtonDown(1)) {
			Wall wall = Instantiate(wallPrefab) as Wall;
			PlaceWall(wall, verticalWall);
		}
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
		
		flowers[i].transform.position = new Vector3((float)(player.shovel.transform.position.x), 2.5f, (float)(player.shovel.transform.position.z + 3));
	}
	
	public void PlaceWall(Wall wall, bool vertical) {
		canPlaceWall = false;
		cash -= wallPrefab.cashCost;
		wall.transform.position = new Vector3((float)(player.shovel.transform.position.x), 1.5f, (float)(player.shovel.transform.position.z + 3));
		if (vertical) {
			wall.transform.Rotate(0f, 90f, 0f);
		}
	}
}
