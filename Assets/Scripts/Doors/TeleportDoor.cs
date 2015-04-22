using UnityEngine;
using System.Collections;

public class TeleportDoor : Interactable {

    public string loadSceneName;

    public override void Interact()
    {
        if (loadSceneName != null)
        {
            Application.LoadLevel(loadSceneName);
        }
    }

}
