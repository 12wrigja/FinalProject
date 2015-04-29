using UnityEngine;
using System.Collections;

public class ButterflyMachine : MonoBehaviour {
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

	private bool gameInProgress = false;
	private GameObject[] butterflyInstances;

	void Start () {
		this.beginGame ();
	}

	void Update () {
		if (this.gameInProgress)
			OnGUI();
		if (this.gameInProgress && ButterflyMachine.butterfliesToCatch <= 0) {
			this.playerWin ();
		}
	}

	void OnGUI() {
		if (this.gameInProgress == false) {
			enabled = false;
		}
		GUI.Label(new Rect(10, 10, 100, 200), "Butterflies to catch: " + butterfliesToCatch.ToString());
	}

	public void beginGame() {
		xLimitPositive = this.xDirectionLimitPositive;
		xLimitNegative = this.xDirectionLimitNegative;
		yLimitPositive = this.yDirectionLimitPositive;
		yLimitNegative = this.yDirectionLimitNegative;
		zLimitPositive = this.xDirectionLimitPositive;
		zLimitNegative = this.zDirectionLimitNegative;
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
		ButterflyMachine.butterfliesToCatch = Random.Range (1, this.numberOfButterflies);
		this.gameInProgress = true;
	}

	public static void butterflyCaught () {
		ButterflyMachine.butterfliesToCatch -= 1;
	}

	public void playerWin () {
		Debug.Log ("derp");
		this.gameInProgress = false;
		for (int i = 0; i < this.butterflyInstances.Length; i++) {
			butterflyInstances[i].GetComponent<ButterflyHolder> ().playerWon ();
		}
	}
}
