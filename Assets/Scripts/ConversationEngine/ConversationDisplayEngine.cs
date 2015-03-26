using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ConversationDisplayEngine : MonoBehaviour {

    public Text ConverseeName;
    public Text ConverseeText;
    public RectTransform optionsPanel;
    public GameObject buttonPrefab;
    public static KeyCode conversationAdvanceKeyCode = KeyCode.Space;
    public static KeyCode conversationEndKeyCode = KeyCode.Escape;

    private bool inConversation;
    private Conversable currentConversee;
    private UIElement conversationLayout;
    private int lineNumber = 0;

    private static ConversationDisplayEngine instance;
    public void Start()
    {
        instance = this;
        conversationLayout = GetComponent<UIElement>();
    }

    public static void DisplayConversation(Conversable e)
    {
        if (instance.inConversation)
        {
            return;
        }
        instance.inConversation = true;
        instance.currentConversee = e;
        UIManager.ShowUIElementExclusive(instance.conversationLayout);
        instance.ConverseeName.text = e.conversable_tag;
        instance.StartCoroutine(instance.HaveConversation());
    }

    private IEnumerator HaveConversation()
    {
        int previousOptionsCount = optionsPanel.transform.childCount;
        for (int i = previousOptionsCount - 1; i >= 0; i--)
        {
            Destroy(optionsPanel.GetChild(i).gameObject);
        }
        List<string> conversationLines = currentConversee.GetConversationLines();
        if (conversationLines.Count == 0)
        {
            EndConversation();
        }
        else
        {
            yield return StartCoroutine(displayLines(conversationLines));
            List<string> conversationOptions = currentConversee.GetConversationOptions();
            List<Selectable> options = new List<Selectable>();
            if (conversationOptions.Count == 0)
            {
                int previousLineNumber = lineNumber;
                while (lineNumber == previousLineNumber)
                {
                    yield return null;
                }
                currentConversee.transitionConversation(0);
                EndConversation();
            }
            else
            {
                int copy;
                for (int i = 0; i < conversationOptions.Count; i++)
                {
                    GameObject obj = Instantiate(buttonPrefab) as GameObject;
                    obj.gameObject.transform.SetParent(optionsPanel.transform);
                    Text txt = obj.GetComponentInChildren<Text>();
                    txt.text = conversationOptions[i];
                    Button btn = obj.GetComponent<Button>();
                    Debug.Log("Attaching option with text '" + conversationOptions[i] + "' to index " + i);
                    AddListener(btn, i);
                    options.Add(btn);
                }
            }
        }
    }

    private void AddListener(Button b, int index)
    {
        b.onClick.AddListener(() => advanceCurrentConversation(index));
    }

    private void EndConversation()
    {
        UIManager.ToggleUIElement(conversationLayout);
        inConversation = false;
    }

    private void advanceCurrentConversation(int index)
    {
        Debug.Log("Selected index " + index);
        if (!currentConversee.transitionConversation(index))
        {
            Debug.Log("Unable to transition conversation. Ending Conversation.");
            EndConversation();
            return;
        };
        instance.StartCoroutine(HaveConversation());
    }

    private IEnumerator displayLines(List<string> conversationLines)
    {
        foreach(string line in conversationLines)
        {
            ConverseeText.text = line;
            int nextNum = lineNumber + 1;
            while (nextNum != conversationLines.Count && lineNumber != nextNum)
            {
                yield return 0;
            }
        }
        lineNumber = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(conversationAdvanceKeyCode) && inConversation)
        {
            lineNumber++;
        }
        if (Input.GetKeyDown(conversationEndKeyCode) && inConversation)
        {
            EndConversation();
        }
    }

}
