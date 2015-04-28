using UnityEngine;
using System.Collections;

public class trapdoorcontroller : MonoBehaviour {

    public GameObject trapDoor;
    private Animator trapDoorAnimator;

    public bool foundKey;

	// Use this for initialization
	void Start () {
        foundKey = false;
        trapDoorAnimator = trapDoor.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (foundKey)
        {
            trapDoorAnimator.SetTrigger("RiseTrigger");
        }
	}

    void HaveFoundKey()
    {
        foundKey = true;
    }
}
