using UnityEngine;
using System.Collections;

public class HumanControlScript : MonoBehaviour {

    private Vector3 moveDirection = Vector3.zero;
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, -2, 0);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, 2, 0);
        }

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
