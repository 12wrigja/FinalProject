using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(UIElement))]
public class UINotifier : MonoBehaviour {

    private UIElement element;
    private Text notifyText;
    public GameObject notifyPanel;

    private static UINotifier instance;
    private GameObject lockObj;
	// Use this for initialization
	void Start () {
        instance = this;
        notifyText = GetComponentInChildren<Text>();
        element = GetComponent<UIElement>();
        //instance.notifyPanel.SetActive(false);
        UIManager.ShowUIElement(element);
	}

    //void Update()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //}

    public static void NotifyLock(string text, GameObject lockObj)
    {
        Notify(text);
        instance.lockObj = lockObj;
    }

    public static void Notify(string text)
    {
        if (instance != null && instance.lockObj == null)
        {
            instance.notifyText.text = text;
            instance.notifyPanel.SetActive(true);
        }
    }

    public static void DismissLock(GameObject lockObj)
    {
        if (instance.lockObj == lockObj)
        {
            instance.lockObj = null;
            Dismiss();
        }
    }

    public static void Dismiss()
    {
        if (instance != null && instance.lockObj == null)
        {
            instance.notifyPanel.SetActive(false);
        }
    }

    public static bool hasLock(GameObject obj)
    {
        return (null == instance.lockObj)?false:(instance.lockObj == obj);
    }
}
