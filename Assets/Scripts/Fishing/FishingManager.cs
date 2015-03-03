using UnityEngine;
using System.Collections;

public class FishingManager : MonoBehaviour {

	public Rod rod;
	public float chanceOfFish;
	public float initialCatchTimer;

	public bool canFish = false;
	public bool isFishing = false;
	public bool canCatch = false;
	public bool fishCaught = false;

	private float catchTimer;

	void Start () {

	}

	void Update () {

		if(canCatch) {
			if(catchTimer > 0){
				if(Input.GetKey(KeyCode.Space)){
					fishCaught = true;
					isFishing = false;
					canCatch = false;
				} else{
					catchTimer -= Time.deltaTime;
				}
			} else {
				catchTimer = initialCatchTimer;
				canCatch = false;
			}
		}

		if(isFishing && !canCatch && chanceOfFish > Random.Range(0f, 100f)){
			canCatch = true;
		}

		if(Input.GetKey(KeyCode.Space) && canFish && !fishCaught){
			isFishing = true;
		}

	}

}
