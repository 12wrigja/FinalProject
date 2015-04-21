using UnityEngine;
using System.Collections;

public class Rod : MonoBehaviour {

	public bool isPickedUp = false;
	public FishingManager managerPrefab;
	public GameObject fish;

	private bool managerExists = false;
	private FishingManager managerInstance;
	private GameObject fishInstance;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isPickedUp && !managerExists){
			managerInstance = Instantiate(managerPrefab) as FishingManager;
			managerExists = true;
			managerInstance.setAnimator(this.GetComponent<Animator>());
			managerInstance.setRod(this);
		}
		if (!isPickedUp && managerExists){
			Destroy(managerInstance.gameObject);
			managerExists = false;
		}
	}

	public void interact(){
		isPickedUp = true;
	}

	public void spawnFish(){
		fishInstance = Instantiate (fish) as GameObject;
		fishInstance.transform.SetParent (this.transform.GetChild(0));
		fishInstance.transform.position = this.transform.GetChild (0).position;
	}
}
