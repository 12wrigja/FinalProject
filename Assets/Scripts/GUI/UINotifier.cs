using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(UIElement))]
public class UINotifier : MonoBehaviour {

    private UIElement element;
    private Text notifyText;

    private static UINotifier instance;

	// Use this for initialization
	void Start () {
        instance = this;
        notifyText = GetComponentInChildren<Text>();
        element = GetComponent<UIElement>();
        UIManager.ShowUIElement(element);
	}

    //void Update()
    //{
    //    if (!element.isOnScreen)
    //    {
    //        UIManager.ShowUIElement(element);
    //    }
    //}

    public static void Notify(string text)
    {
        instance.notifyText.text = text;
        instance.notifyText.enabled = true;
    }

    public static void Dismiss()
    {
        instance.notifyText.enabled = false;
    }
}
