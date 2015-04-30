using UnityEngine;
using System.Collections;

public class ButterflyMachine : Interactable {
	public GameObject player;
	public GameObject butterflyNet;
	public GameObject[] butterflyHolders;

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
	private GameObject[] butterflyInstances;

	void Start () {

	}

	void Update () {
		if (ButterflyMachine.gameInProgress) {
			this.GUItext = "Butterflies to catch: " + ButterflyMachine.butterfliesToCatch.ToString ();
		}
		else {
			this.GUItext = "";
		}
		if (ButterflyMachine.gameInProgress && ButterflyMachine.butterfliesToCatch <= 0) {
			this.playerWin ();
		}
	}

	public override void Interact() {
		this.beginGame();
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
		ButterflyMachine.gameInProgress = true;
	}

	public static void butterflyCaught () {
		ButterflyMachine.butterfliesToCatch -= 1;
	}

	public void playerWin () {
		ButterflyMachine.gameInProgress = false;
		this.GUItext = "";
		for (int i = 0; i < this.butterflyInstances.Length; i++) {
			butterflyInstances[i].GetComponent<ButterflyHolder> ().playerWon ();
		}
	}
}
