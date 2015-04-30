using UnityEngine;
using System.Collections;

public class Rod : Interactable{

	public Bauble bauble;
	public bool isPickedUp = false;
	public FishingManager managerPrefab;
	public GameObject fish;
	public GameObject cam;
	public Vector3 cameraPosition;
	public Vector3 cameraRotation;
	public Vector3 fishPosition;
	public Vector3 fishRotation;

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
			this.transform.SetParent(cam.transform);
			this.transform.localPosition = cameraPosition;
			this.transform.localEulerAngles = cameraRotation;
			managerInstance = Instantiate(managerPrefab) as FishingManager;
			managerInstance.setAnimator(this.GetComponent<Animator>(), bauble.getAnimator());
			managerInstance.setRod(this);
			managerInstance.setBauble(bauble);
			managerExists = true;
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
			fishInstance.transform.SetParent (bauble.transform);
			fishInstance.transform.localPosition = fishPosition;
			fishInstance.transform.localEulerAngles = fishRotation;
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
