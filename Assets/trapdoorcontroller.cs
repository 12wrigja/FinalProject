using UnityEngine;
using System.Collections;

public class trapdoorcontroller : MonoBehaviour {

    public GameObject trapDoor;
    private Animator trapDoorAnimator;

    public bool foundKey;

	// Use this for initialization
	void Start () {
        foundKey = (PlayerPrefs.GetInt("Trapdoor") == 1) ? true : false;
        trapDoorAnimator = trapDoor.GetComponent<Animator>();
        trapDoor.GetComponent<TeleportDoor>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (foundKey)
        {
            trapDoorAnimator.SetTrigger("RiseTrigger");
            trapDoor.GetComponent<TeleportDoor>().enabled = true;
            PlayerPrefs.SetInt("Trapdoor", 1);
        }
	}

    void HaveFoundKey()
    {
        foundKey = true;
    }
}
