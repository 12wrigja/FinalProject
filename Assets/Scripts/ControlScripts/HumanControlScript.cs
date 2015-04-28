using UnityEngine;
using System.Collections;
using System.IO;

//[RequireComponent(typeof(AudioSource))]
public class HumanControlScript : MonoBehaviour {

    private Vector3 moveDirection = Vector3.zero;
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float rotateAngle = 5f;
    public float gravity = 20.0F;

    public KeyCode interactKey;

	//public AudioClip walking;
	private int stepCount = 0;

    public MonoBehaviour msScript;
    private static HumanControlScript instance;
	// Use this for initialization
	void Start () {
        Cursor.visible = false;
        instance = this;

        TryRestoreSavedPosition();
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
//		if ((moveDirection.x != 0 || moveDirection.z != 0)) {
//			if(stepCount == 16){
//				GetComponent<AudioSource>().PlayOneShot(walking, 1f);
//				stepCount = 0;
//			}
//			stepCount++;
//		}
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
            if (Input.GetKeyDown(KeyCode.V))
            {
                msScript.enabled = !msScript.enabled;
            }
        }

        UINotifier.Dismiss();
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position,transform.forward,1.5f);
        for(int j=0; j<hits.Length; j++){
            GameObject obj = hits[j].transform.gameObject;
            Conversable c = obj.GetComponent<Conversable>();
            Interactable i = obj.GetComponent<Interactable>();

            if (c == null && i == null)
            {
                continue;
            }

            if (c != null && !Input.GetKeyDown(interactKey) && c.enabled)
            {
                UINotifier.Notify("Press " + (interactKey.ToString()) + " to talk with " + c.conversee_name);
            }else if (i != null && !Input.GetKeyDown(interactKey) && i.enabled)
            {
                UINotifier.Notify("Press " + (interactKey.ToString()) + " to "+i.interactText);
            } else if (c != null && Input.GetKeyDown(interactKey) && c.enabled)
            {
                ConversationDisplayEngine.DisplayConversation(c);
                
            } else if (i != null && Input.GetKeyDown(interactKey) && i.enabled){
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

    private void TryRestoreSavedPosition()
    {
        string sceneName = Application.loadedLevelName;
        if(
            PlayerPrefs.HasKey(sceneName+"posX") &&
            PlayerPrefs.HasKey(sceneName+"posY") &&
            PlayerPrefs.HasKey(sceneName+"posZ") &&
            PlayerPrefs.HasKey(sceneName+"rotX") &&
            PlayerPrefs.HasKey(sceneName+"rotY") &&
            PlayerPrefs.HasKey(sceneName+"rotZ")
            ){
                float posX = PlayerPrefs.GetFloat(sceneName + "posX");
                float posY = PlayerPrefs.GetFloat(sceneName + "posY");
                float posZ = PlayerPrefs.GetFloat(sceneName + "posZ");

                float rotX = PlayerPrefs.GetFloat(sceneName + "rotX");
                float rotY = PlayerPrefs.GetFloat(sceneName + "rotY");
                float rotZ = PlayerPrefs.GetFloat(sceneName + "rotZ");
                Debug.Log("Setting Position: ("+posX+", "+posY+", "+posZ);
                Debug.Log("Setting Rotation: ("+rotX+", "+rotY+", "+rotZ);
                transform.position = new Vector3(posX, posY, posZ);
                transform.eulerAngles = new Vector3(rotX, rotY, rotZ);

                PlayerPrefs.DeleteKey(sceneName+"posX");
                PlayerPrefs.DeleteKey(sceneName + "posY");
                PlayerPrefs.DeleteKey(sceneName + "posZ");

                PlayerPrefs.DeleteKey(sceneName + "rotX");
                PlayerPrefs.DeleteKey(sceneName + "rotY");
                PlayerPrefs.DeleteKey(sceneName + "rotZ");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + (transform.forward));
    }
}
