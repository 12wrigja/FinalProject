using UnityEngine;
using System.Collections;

public class HumanControlScript : MonoBehaviour {

    private Vector3 moveDirection = Vector3.zero;
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float rotateAngle = 5f;
    public float gravity = 20.0F;

    public KeyCode interactKey;

	// Use this for initialization
	void Start () {
        Cursor.visible = false;
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

        //if (Input.GetKey(KeyCode.Q))
        //{
        //    transform.Rotate(0, -rotateAngle, 0);
        //}
        //if (Input.GetKey(KeyCode.E))
        //{
        //    transform.Rotate(0, rotateAngle, 0);
        //}

        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.forward,out hit,1f)){
            GameObject obj = hit.transform.gameObject;
            Conversable c = obj.GetComponent<Conversable>();
            Interactable i = obj.GetComponent<Interactable>();

            UINotifier.Dismiss();
            if (c != null && !Input.GetKeyDown(interactKey))
            {
                Debug.Log("Showing notifier for conversable.");
                UINotifier.Notify("Press " + (interactKey.ToString()) + "to talk with " + c.conversee_name);
            }else if (i != null && !Input.GetKeyDown(interactKey))
            {
                Debug.Log("Showing notifier for interactable.");
                UINotifier.Notify("Press " + (interactKey.ToString()) + "to "+i.interactText);
            } else if (c != null && Input.GetKeyDown(interactKey))
            {
                ConversationDisplayEngine.DisplayConversation(c);
                
            } else if (i != null && Input.GetKeyDown(interactKey)){
                i.Interact();
            }
        }
	}

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + (transform.forward));
    }
}
