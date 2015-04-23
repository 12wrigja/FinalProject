using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DebugMenu : MonoBehaviour {

    public KeyCode debugListKeyCode = KeyCode.BackQuote;
    public GameObject UIElementToggle;
    public GameObject toggleContainer;

    private UIElement self;

    private List<Toggle> toggles;

    void Start()
    {
        self = GetComponent<UIElement>();
        toggles = new List<Toggle>();
        foreach (UIElement element in UIManager.AllElements)
        {
            GameObject toggle = Instantiate(UIElementToggle) as GameObject;
            toggle.transform.SetParent(toggleContainer.transform,false);
            Toggle toggleScript = toggle.GetComponent<Toggle>();
            toggles.Add(toggleScript);
            UIElement linkedElement = element;
            toggleScript.onValueChanged.AddListener((val)=>AlterUIElement(linkedElement,val));
        }
    }
    
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(debugListKeyCode))
        {
            UIManager.ToggleUIElement(self);
            updateDisplay();
        }
	}

    private void updateDisplay()
    {
        int count = 0;
        foreach (UIElement element in UIManager.AllElements)
        {
            Toggle t = toggles[count];
            t.gameObject.GetComponentInChildren<Text>().text = element.gameObject.name;
            t.isOn = element.isOnScreen;
            count++;
        }
    }

    void AlterUIElement(UIElement element, bool showVal)
    {
        element.isOnScreen = showVal;
    }
}
