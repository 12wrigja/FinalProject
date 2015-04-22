using UnityEngine;
using System.Collections;

public class PreferenceClearer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("Clearning Preferences.");
        PlayerPrefs.DeleteAll();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
