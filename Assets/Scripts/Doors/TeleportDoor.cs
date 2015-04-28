using UnityEngine;
using System.Collections;

public class TeleportDoor : Interactable {

    public string loadSceneName;
    public bool overrideplayerposition = true;
    public Vector3 playerStartPosition;
    public Vector3 playerStartRotation;

    public override void Interact()
    {
        if (loadSceneName != null)
        {
            AnxietySystem.SaveValues();
            if (overrideplayerposition)
            {
                SaveStartLocation();
            }
            //Save transform values for returning to this scene
            Application.LoadLevel(loadSceneName);
        }
    }

    private void SaveStartLocation(){
        PlayerPrefs.SetFloat(loadSceneName + "posX", playerStartPosition.x);
        PlayerPrefs.SetFloat(loadSceneName + "posY", playerStartPosition.y);
        PlayerPrefs.SetFloat(loadSceneName + "posZ", playerStartPosition.z);
        PlayerPrefs.SetFloat(loadSceneName + "rotX", playerStartRotation.x);
        PlayerPrefs.SetFloat(loadSceneName + "rotY", playerStartRotation.y);
        PlayerPrefs.SetFloat(loadSceneName + "rotZ", playerStartRotation.z);
    }

}
