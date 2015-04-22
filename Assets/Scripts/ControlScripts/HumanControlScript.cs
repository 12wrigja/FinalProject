using UnityEngine;
using System.Collections;

public class HumanControlScript : MonoBehaviour {

    private Vector3 moveDirection = Vector3.zero;
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float rotateAngle = 5f;
    public float gravity = 20.0F;

    public KeyCode interactKey;

    public MonoBehaviour msScript;
    private static HumanControlScript instance;
	// Use this for initialization
	void Start () {
        Cursor.visible = false;
        instance = this;
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

        if (msScript != null) {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                msScript.enabled = false;
            }
            else
            {
                msScript.enabled = true;
            }
        }

        UINotifier.Dismiss();
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position,transform.forward,1f);
        for(int j=0; j<hits.Length; j++){
            GameObject obj = hits[j].transform.gameObject;
            Conversable c = obj.GetComponent<Conversable>();
            Interactable i = obj.GetComponent<Interactable>();

            if (c == null && i == null)
            {
                continue;
            }

            if (c != null && !Input.GetKeyDown(interactKey))
            {
                UINotifier.Notify("Press " + (interactKey.ToString()) + " to talk with " + c.conversee_name);
            }else if (i != null && !Input.GetKeyDown(interactKey))
            {
                UINotifier.Notify("Press " + (interactKey.ToString()) + " to "+i.interactText);
            } else if (c != null && Input.GetKeyDown(interactKey))
            {
                ConversationDisplayEngine.DisplayConversation(c);
                
            } else if (i != null && Input.GetKeyDown(interactKey)){
                i.Interact();
            }
        }
	}

    public static void EnableHuman()
    {
        instance.enabled = true;
        instance.msScript.enabled = true;
    }

    public static void DisableHuman()
    {
        instance.enabled = false;
        instance.msScript.enabled = false;
    }

    public static GameObject GetHuman()
    {
        if (instance != null)
        {
            return instance.gameObject;
        }
        else
        {
            return null;
        }
        
    }

    public static void SaveHuman()
    {
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + (transform.forward));
    }
}
