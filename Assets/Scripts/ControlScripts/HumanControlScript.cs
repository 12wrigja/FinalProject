using UnityEngine;
using System.Collections;

public class HumanControlScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Translate(0.1f * h, 0f, 0.1f * v);
        transform.Rotate(new Vector3(0, 5 * Input.GetAxis("Rotate"), 0));
        RaycastHit hit;
        if(Physics.Raycast(transform.position,-1*Vector3.forward,out hit,1f)){
            GameObject obj = hit.transform.gameObject;
            Conversable c = obj.transform.GetComponent<Conversable>();
            if (c != null && Input.GetKeyDown(ConversationDisplayEngine.conversationAdvanceKeyCode))
            {
                ConversationDisplayEngine.DisplayConversation(c);
            }
        }
	}
}
