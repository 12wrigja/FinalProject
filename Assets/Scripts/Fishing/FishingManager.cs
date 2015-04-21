using UnityEngine;
using System.Collections;

public class FishingManager : MonoBehaviour {

	public Rod rod;
	public Animator animations;
	public float chanceOfFish;
	public float initialCatchTimer;

	public bool canFish = false;
	public bool isFishing = false;
	public bool canCatch = false;
	public bool fishCaught = false;

	private bool soundIsPlaying = false;
	private float catchTimer;

	void Start () {
		catchTimer = initialCatchTimer;
		animations.SetBool("isFishing", isFishing);
		animations.SetBool("canCatch", canCatch);
	}

	void Update () {
		if(canFish && isFishing && canCatch && !soundIsPlaying){
			audio.Play();
			soundIsPlaying = true;
		}
		else if(!canCatch && soundIsPlaying){
			audio.Stop();
			soundIsPlaying = false;
		}
		if(canFish){
			if(isFishing && canCatch) {
				if(catchTimer > 0){
					if(Input.GetKeyDown(KeyCode.Space)){
						rod.spawnFish();
						fishCaught = true;
						isFishing = false;
						animations.SetBool("isFishing", isFishing);
						canCatch = false;
						animations.SetBool("canCatch", canCatch);
					} else{
						catchTimer -= Time.deltaTime;
					}
				} else {
					catchTimer = initialCatchTimer;
					canCatch = false;
					animations.SetBool("canCatch", canCatch);
				}
			} else if(!fishCaught && Input.GetKeyDown(KeyCode.Space)){
				isFishing = !isFishing;
				animations.SetBool("isFishing", isFishing);
			}

			if(isFishing && !canCatch && chanceOfFish > Random.Range(0f, 100f)){
				canCatch = true;
				animations.SetBool("canCatch", canCatch);
			}
		}

	}

	public void setAnimator(Animator animations){
		this.animations = animations;
	}

	public void setRod(Rod rod){
		this.rod = rod;
	}
}
