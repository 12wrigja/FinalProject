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
            UIManager.RestoreStash();
            HumanControlScript.EnableHuman();
        }
    }

    public override void Interact()
    {
        HumanControlScript.DisableHuman();
        UIManager.StashScreen();
        bookUI.leftPage.text = leftPageText;
        bookUI.rightPage.text = rightPageText;
        UIManager.ShowUIElementExclusive(bookUI);
    }

}
