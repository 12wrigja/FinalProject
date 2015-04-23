using UnityEngine;
using System.Collections;

public class ReturnScript : MonoBehaviour {

    public string LevelToReturnTo;

	// Update is called once per frame
	void Update () {
        if(!LevelToReturnTo.Equals(""))
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.LoadLevel(LevelToReturnTo);
            }	
        }
        
	}
}
