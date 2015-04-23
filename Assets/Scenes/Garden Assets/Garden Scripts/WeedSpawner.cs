using UnityEngine;
using System.Collections;

public class WeedSpawner : MonoBehaviour {
	
	
	public float spawnChance;
	public float minSpawnTimeDelay;
	public float spawnTimeDelay;
	public float initialSpawnTimeDelay;
	public float spawnGrowthRate;
	public bool spawnable;
	public Weeds weedPrefab;
	public Transform[] spawnLocations;
	
	public Flower collectionPoint;    // This isn't actually a flower
	public FlowerManager gameFlowers;
	public Player player;

	public void Start () {
		spawnable = true;
	}
	
	public void Update () {
		if (spawnable) {
			StartCoroutine("Spawn");
		}
		spawnTimeDelay = initialSpawnTimeDelay / gameFlowers.flowers.Length / spawnGrowthRate;
		if (spawnTimeDelay < minSpawnTimeDelay) {
			spawnTimeDelay = minSpawnTimeDelay;
		}
	}
	
	public IEnumerator Spawn() {
		spawnable = false;
		yield return new WaitForSeconds(spawnTimeDelay);
		
		float chance = Random.Range(0, 100);
		int index = (int)(Random.Range(0, spawnLocations.Length));
		if (chance < spawnChance) {
			// Spawn the weed
			Instantiate(weedPrefab, spawnLocations[index].position, Quaternion.identity);
		}
		spawnable = true;
	}
	
	public void SpawnWave() {
		for (int i = 0; i < spawnLocations.Length; i++) {
			Instantiate(weedPrefab, spawnLocations[i].position, Quaternion.identity);
		}
		Debug.Log ("wave Spawned");
	}
}
