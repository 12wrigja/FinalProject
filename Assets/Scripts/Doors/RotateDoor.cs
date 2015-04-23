using UnityEngine;
using System.Collections;

public class RotateDoor : Interactable {

    public bool isOpen = false;

    public void Start()
    {
        interactText = (isOpen) ? "close the door" : "open the door";
    }

    public override void Interact()
    {
        if (isOpen)
        {
            isOpen = false;
            interactText = "open the door.";
            transform.Rotate(new Vector3(0, 90, 0));
        }
        else
        {
            isOpen = true;
            interactText = "close the door.";
            transform.Rotate(new Vector3(0, -90, 0));
        }
    }

}
