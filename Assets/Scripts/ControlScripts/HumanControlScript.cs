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

        if (rigidbody.velocity.magnitude < 1f)
        {
            rigidbody.velocity = transform.TransformDirection(new Vector3(h * 1, 0, v * 1));
        }
        if (h == 0 && v == 0)
        {
            rigidbody.velocity = new Vector3(0.8f * rigidbody.velocity.x, 0.8f * rigidbody.velocity.y, 0.8f * rigidbody.velocity.z);
        }
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
