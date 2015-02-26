using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//The menu manager controls the switching between UI menu's.
public class UIManager : MonoBehaviour {

    public KeyCode debugListKeyCode = KeyCode.BackQuote;

    private static UIManager instance;

    private static UIElement[] allUIElements;
    private static List<UIElement> currentUIElements;

    private static UIElement debugUIPanel;

	public void Start(){
        allUIElements = GetComponentsInChildren<UIElement>();
        currentUIElements = new List<UIElement>();
        debugUIPanel = allUIElements[allUIElements.Length - 1];
	}

    public void Update()
    {
        if (Input.GetKeyDown(debugListKeyCode))
        {
            ToggleUIElement(debugUIPanel);
        }
    }

	//Show menu shows the current menu. If null is passed it, it shows nothing while disabling the previous menu.
	public static void ShowUIElementExclusive(UIElement element){
        clearScreen();
        currentUIElements.Add(element);
        element.isOnScreen = true;
	}

    public static void ShowUIElement(UIElement element)
    {
        currentUIElements.Add(element);
        element.isOnScreen = true;
    }

    public static void HideUIElement(UIElement element)
    {
        currentUIElements.Remove(element);
        element.isOnScreen = false;
    }

    public static void clearScreen()
    {
        if (currentUIElements.Count > 0)
        {
            foreach (UIElement element in currentUIElements)
            {
                element.isOnScreen = false;
                currentUIElements.Remove(element);
            }
        }
    }

    private static void ToggleUIElement(UIElement element)
    {
        if (element.isOnScreen)
        {
            HideUIElement(element);
        }
        else
        {
            ShowUIElement(element);
        }
    }
}
