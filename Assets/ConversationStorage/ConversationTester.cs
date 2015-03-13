using UnityEngine;
using System.Collections;

public class ConversationTester : MonoBehaviour {

    Conversable c;

    void Start()
    {
        c = new Conversable();
        c.conversable_tag = "greeter";
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ConversationDisplayEngine.DisplayConversation(c);
        }
	}
}
