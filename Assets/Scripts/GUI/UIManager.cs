using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Collections.ObjectModel;

//The menu manager controls the switching between UI menu's.
public class UIManager : MonoBehaviour {

    public static ReadOnlyCollection<UIElement> AllElements
    {
        get
        {
            return allUIElements.AsReadOnly();
        }
    }

    private static UIManager instance;

    private static List<UIElement> allUIElements;
    private static List<UIElement> currentUIElements;

	public void Awake(){
        allUIElements = GetComponentsInChildren<UIElement>().ToList<UIElement>();
        Debug.Log(allUIElements.Count);
        currentUIElements = new List<UIElement>();
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

    public static void ToggleUIElement(UIElement element)
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
