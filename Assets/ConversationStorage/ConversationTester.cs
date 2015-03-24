using UnityEngine;
using System.Collections;

public class ConversationTester : MonoBehaviour {

    Conversable c;

    void Start()
    {
        c = GetComponent<Conversable>();
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ConversationDisplayEngine.DisplayConversation(c);
        }
	}
}
