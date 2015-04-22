using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InteractableBook : Interactable {

    private KeyCode exitBookKeyCode = KeyCode.Escape;

    public string leftPageText;
    public string rightPageText;

    public BookUIElement bookUI;

    public void Update()
    {
        if (bookUI.isOnScreen && Input.GetKeyDown(exitBookKeyCode))
        {

        }
    }

    public override void Interact()
    {
        HumanControlScript.DisableHuman();
        UIManager.ShowUIElementExclusive(bookUI);
    }

}
