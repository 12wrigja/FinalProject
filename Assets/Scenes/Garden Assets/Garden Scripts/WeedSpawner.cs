using UnityEngine;
using System.Collections;

public class WeedSpawner : MonoBehaviour {
	
	public float spawnChance;
	public float spawnRate;
	public float initialSpawnRate;
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
		spawnRate = initialSpawnRate / gameFlowers.flowers.Length;
		if (spawnRate < 1.5) {
			spawnRate = 1.5f;
		}
	}
	
	public IEnumerator Spawn() {
		spawnable = false;
		yield return new WaitForSeconds(spawnRate);
		
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
	}
}
