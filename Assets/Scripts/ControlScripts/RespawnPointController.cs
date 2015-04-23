using UnityEngine;
using System.Collections;

public class RespawnPointController : MonoBehaviour {

    private bool relocated = false;
	
	// Update is called once per frame
	void Update () {
        if (!relocated)
        {
            GameObject human = HumanControlScript.GetHuman();
            if (human != null)
            {
                human.transform.position = transform.position;
            }
            relocated = true;
            enabled = false;
        }
	}
}
