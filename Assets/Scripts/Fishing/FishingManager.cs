using UnityEngine;
using System.Collections;

public class FishingManager : MonoBehaviour {

	public Rod rod;
	public float chanceOfFish;
	public float initialCatchTimer, timeBetweenSounds;

	public bool canFish = false;
	public bool isFishing = false;
	public bool canCatch = false;
	public bool fishCaught = false;

	private bool firstSoundIsPlaying = false;
	private bool secondSoundIsPlaying = false;
	private float soundCounter;
	private float catchTimer;

	void Start () {
		catchTimer = initialCatchTimer;
		soundCounter = timeBetweenSounds;
	}

	void Update () {

		if(canFish){
			if(isFishing && canCatch) {
				if(catchTimer > 0){
					playSound();
					if(Input.GetKeyDown(KeyCode.Space)){
						fishCaught = true;
						isFishing = false;
						canCatch = false;
					} else{
						catchTimer -= Time.deltaTime;
					}
				} else {
					catchTimer = initialCatchTimer;
					canCatch = false;
					firstSoundIsPlaying = false;
					secondSoundIsPlaying = false;
					soundCounter = timeBetweenSounds;
				}
			} else if(!fishCaught && Input.GetKeyDown(KeyCode.Space)){
				isFishing = !isFishing;
			}

			if(isFishing && !canCatch && chanceOfFish > Random.Range(0f, 100f)){
				canCatch = true;
			}
		}

	}

	private void playSound(){
		if(!firstSoundIsPlaying){
			audio.Play();
			firstSoundIsPlaying = true;
		}
		if(!secondSoundIsPlaying){
			soundCounter -= Time.deltaTime;
			if(soundCounter <= 0){
				audio.Play();
				secondSoundIsPlaying = true;
			}
		}
	}

}
