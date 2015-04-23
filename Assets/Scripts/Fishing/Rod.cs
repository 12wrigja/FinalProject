using UnityEngine;
using System.Collections;

public class Rod : Interactable{

	public bool isPickedUp = false;
	public FishingManager managerPrefab;
	public GameObject fish;
	public GameObject cam;

	private bool managerExists = false;
	private bool fishExists = false;
	private FishingManager managerInstance;
	private GameObject fishInstance;
	private Vector3 startPosition;
	private Quaternion startRotation;

	// Use this for initialization
	void Start () {
		startPosition = this.transform.position;
		startRotation = this.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		if (isPickedUp && !managerExists){
			this.transform.SetParent(cam.transform.GetChild(0));
//			this.transform.position = cam.transform.position + new Vector3(3, 0, 5);
			managerInstance = Instantiate(managerPrefab) as FishingManager;
			managerExists = true;
			managerInstance.setAnimator(this.GetComponent<Animator>());
			managerInstance.setRod(this);
		}
		if (!isPickedUp && managerExists){
			this.transform.SetParent(null);
			this.transform.position = startPosition;
			this.transform.rotation = startRotation;
			Destroy(managerInstance.gameObject);
			managerExists = false;
		}
		this.GetComponent<Animator>().SetBool("isPickedUp", isPickedUp);
	}

	public void interact(){
		isPickedUp = true;
	}

	public void spawnFish(){
		if(!fishExists){
			fishExists = true;
			fishInstance = Instantiate (fish) as GameObject;
			fishInstance.transform.SetParent (this.transform.GetChild(0));
			fishInstance.transform.position = this.transform.GetChild (0).position;
		}
	}

	public void destroyFish(){
		if(fishExists){
			Destroy(fishInstance.gameObject);
			fishExists = false;
		}
	}

	public void pickUp(){
		isPickedUp = true;
	}

	public void putDown(){
		isPickedUp = false;
		destroyFish ();
	}


	public override void Interact(){
		pickUp ();
	}
}
