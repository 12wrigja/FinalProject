using UnityEngine;
using System.Collections;

public class Rod : MonoBehaviour {

	public bool isPickedUp = false;
	public FishingManager managerPrefab;

	private bool managerExists = false;
	private FishingManager managerInstance;

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
}
