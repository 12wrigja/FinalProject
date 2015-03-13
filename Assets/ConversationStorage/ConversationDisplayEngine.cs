using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ConversationDisplayEngine : MonoBehaviour {

    public Text ConverseeName;
    public Text ConverseeText;
    public RectTransform optionsPanel;
    public GameObject buttonPrefab;
    public KeyCode conversationAdvanceKeyCode = KeyCode.Space;

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
            Debug.Log("Already in conversation.");
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
            for (int i = 0; i < conversationOptions.Count; i++)
            {
                if (conversationOptions.Count == 1 && conversationOptions[i].Equals(""))
                {
                    int previousLineNumber = lineNumber;
                    while (lineNumber == previousLineNumber)
                    {
                        yield return null;
                    }
                    EndConversation();
                    break;
                }
                GameObject obj = Instantiate(buttonPrefab) as GameObject;
                obj.gameObject.transform.SetParent(optionsPanel.transform);
                Text txt = obj.GetComponentInChildren<Text>();
                txt.text = conversationOptions[i];
                Button btn = obj.GetComponent<Button>();
                options.Add(btn);
                int currentIndex = i;
                btn.onClick.AddListener(() => advanceCurrentConversation(currentIndex));
            }
        }
    }

    private void EndConversation()
    {
        currentConversee.transitionConversation(0);
        UIManager.ToggleUIElement(conversationLayout);
        inConversation = false;
    }

    private void advanceCurrentConversation(int index)
    {
        currentConversee.transitionConversation(index);
        instance.StartCoroutine(HaveConversation());
    }

    private IEnumerator displayLines(List<string> conversationLines)
    {
        foreach(string line in conversationLines)
        {
            Debug.Log("Advancing line in conversation.");
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
            Debug.Log("Advancing Line Number.");
            lineNumber++;
        }
    }

}
