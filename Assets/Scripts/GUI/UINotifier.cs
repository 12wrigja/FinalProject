using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(UIElement))]
public class UINotifier : MonoBehaviour {

    private UIElement element;
    private Text notifyText;
    public GameObject notifyPanel;

    private static UINotifier instance;

	// Use this for initialization
	void Start () {
        instance = this;
        notifyText = GetComponentInChildren<Text>();
        element = GetComponent<UIElement>();
        instance.notifyPanel.SetActive(false);
        UIManager.ShowUIElement(element);
	}

    public static void Notify(string text)
    {
        if (instance != null)
        {
            instance.notifyText.text = text;
            instance.notifyPanel.SetActive(true);
        }
    }

    public static void Dismiss()
    {
        if (instance != null)
        {
            instance.notifyPanel.SetActive(false);
        }
    }
}
