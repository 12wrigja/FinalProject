using UnityEngine;
using System.Collections;

public class ButterflyMachine : MonoBehaviour {
	public GameObject player;
	public GameObject butterflyNet;
	public GameObject butterflyCollider;
	public GameObject[] butterflyHolders;

	public KeyCode butterflyNetKey;
	private bool allowSwing = true;
	private float swingDelay = 0f;

	public int numberOfButterflies;
	public static int butterfliesToCatch;
	public float xDirectionLimitPositive;
	public float xDirectionLimitNegative;
	public float yDirectionLimitPositive;
	public float yDirectionLimitNegative;
	public float zDirectionLimitPositive;
	public float zDirectionLimitNegative;
	public static float xLimitPositive;
	public static float xLimitNegative;
	public static float yLimitPositive;
	public static float yLimitNegative;
	public static float zLimitPositive;
	public static float zLimitNegative;

	public static bool gameInProgress = false;

	private string GUItext;
	private GameObject instantiatedNet;
	private GameObject[] butterflyInstances;

	void Update () {
		if (ButterflyMachine.gameInProgress) {
			swingDelay -= Time.deltaTime;
			this.GUItext = "Butterflies to catch: " + ButterflyMachine.butterfliesToCatch.ToString ();
			if (Input.GetKeyDown(butterflyNetKey) && this.allowSwing) {
				this.allowSwing = false;
				this.swingDelay = 1f;
				this.butterflyCollider.GetComponent<Collider>().enabled = true;
				//////////////////////////////////
				// ANIMATE instantiatedNet HERE //
				//////////////////////////////////
			}
			if (this.swingDelay <= 0) {
				this.allowSwing = true;
				this.butterflyCollider.GetComponent<Collider>().enabled = false;
			}
		}
		if (ButterflyMachine.gameInProgress && ButterflyMachine.butterfliesToCatch <= 0) {
			this.playerWin ();
		}
	}
	
	void OnGUI() {
		GUI.Label(new Rect(10, 10, 100, 200), this.GUItext);
	}

	public void beginGame() {
		if (ButterflyMachine.gameInProgress) {
			return;
		}
		xLimitPositive = this.xDirectionLimitPositive; // 50
		xLimitNegative = this.xDirectionLimitNegative; // -8
		yLimitPositive = this.yDirectionLimitPositive; // 5
		yLimitNegative = this.yDirectionLimitNegative; // 1
		zLimitPositive = this.zDirectionLimitPositive; // 85
		zLimitNegative = this.zDirectionLimitNegative; // 60
		this.butterflyInstances = new GameObject[numberOfButterflies];
		int i = 0;
		while (i < numberOfButterflies) {
			int butterfly = Random.Range (0, butterflyHolders.Length);
			float xcoord = Random.Range (xDirectionLimitNegative, xDirectionLimitPositive);
			float ycoord = Random.Range (yDirectionLimitNegative, yDirectionLimitPositive);
			float zcoord = Random.Range (zDirectionLimitNegative, zDirectionLimitPositive);
			butterflyInstances[i] = (GameObject)(Instantiate (butterflyHolders[butterfly], new Vector3 (xcoord, ycoord, zcoord), Quaternion.identity));
			i += 1;
		}
		ButterflyMachine.butterfliesToCatch = Random.Range (5, 20);
		this.GUItext = "Butterflies to catch: " + ButterflyMachine.butterfliesToCatch.ToString ();
		this.equipPlayer ();
		ButterflyMachine.gameInProgress = true;
	}

	public static void butterflyCaught () {
		ButterflyMachine.butterfliesToCatch -= 1;
	}

	public void playerWin () {
		ButterflyMachine.gameInProgress = false;
		Destroy (this.instantiatedNet);
		this.GUItext = "";
		this.butterflyCollider.GetComponent<Collider>().enabled = false;
		///////////////////////////
		// DECREASE ANXIETY HERE //
		///////////////////////////

        AnxietySystem.decreaseAnxiety(100);

		for (int i = 0; i < this.butterflyInstances.Length; i++) {
			butterflyInstances[i].GetComponent<ButterflyHolder> ().playerWon ();
		}
	}

	private void equipPlayer() {
		this.instantiatedNet = (GameObject)Instantiate (this.butterflyNet);
		instantiatedNet.transform.parent = player.transform;
		instantiatedNet.transform.localPosition = new Vector3 (0.5f, -0.36f, 0.88f);
		instantiatedNet.transform.localRotation = Quaternion.Euler (4.4f, 264, 337);
	}
}
