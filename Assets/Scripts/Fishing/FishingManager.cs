using UnityEngine;
using System.Collections;

public class FishingManager : MonoBehaviour {

	public Rod rod;
	public Bauble bauble;
	public Animator animations;
	public float chanceOfFish;
	public float initialCatchTimer;

	public bool canFish = true;
	public bool isFishing = false;
	public bool canCatch = false;
	public bool fishCaught = false;

	private bool soundIsPlaying = false, movementDisabled = false, baubleIsChild = true;
	private float catchTimer;

	void Start () {
		catchTimer = initialCatchTimer;
		animations.SetBool("isFishing", isFishing);
		animations.SetBool("canCatch", canCatch);
	}

	void Update () {
		handleBauble ();
		handleMovement();
		handleSound ();
		if(fishCaught && Input.GetKeyDown(KeyCode.Space)){
			rod.putDown();
		}
		handleFishing ();

	}

	public void setAnimator(Animator animations){
		this.animations = animations;
	}

	public void setRod(Rod rod){
		this.rod = rod;
	}

	public void setBauble(Bauble bauble){
		this.bauble = bauble;
	}

	private void handleSound(){
		if(canFish && isFishing && canCatch && !soundIsPlaying){
			bauble.GetComponent<AudioSource>().Play();
			soundIsPlaying = true;
		}
		else if(!canCatch && soundIsPlaying){
			bauble.GetComponent<AudioSource>().Stop();
			soundIsPlaying = false;
		}
	}

	private void handleMovement(){
		if(isFishing && !movementDisabled){
			HumanControlScript.DisableHuman();
			movementDisabled = true;
		}
		if (!isFishing && movementDisabled){
			HumanControlScript.EnableHuman();
			movementDisabled = false;
		}
	}

	private void handleFishing(){
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

	private void handleBauble(){
		AnimatorStateInfo info = animations.GetCurrentAnimatorStateInfo (0);
		if(baubleIsChild && (info.IsName("HasBeenCast") || info.IsName("CanCatch"))){
			bauble.toPool();
			baubleIsChild = false;
		}
		if(!baubleIsChild && !(info.IsName("HasBeenCast") || info.IsName("CanCatch"))){
			bauble.toRod();
			baubleIsChild = true;
		}
	}
}
