using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DebugMenu : MonoBehaviour {

    public KeyCode debugListKeyCode = KeyCode.BackQuote;
    public GameObject UIElementToggle;
    public GameObject toggleContainer;

    private UIElement self;
    

    void Start()
    {
        self = GetComponent<UIElement>();
        foreach (UIElement element in UIManager.AllElements)
        {
            GameObject toggle = Instantiate(UIElementToggle) as GameObject;
            toggle.transform.SetParent(toggleContainer.transform,false);

        }
    }
    
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(debugListKeyCode))
        {
            UIManager.ToggleUIElement(self);
        }
	}
}
