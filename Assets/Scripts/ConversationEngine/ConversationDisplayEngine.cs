using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text.RegularExpressions;

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
    private int lineNumber = -1;
	private string strRegex = @"\[(.*?)\]";
	private Regex animationRegex;

    private static ConversationDisplayEngine instance;
    public void Start()
    {
        instance = this;
        conversationLayout = GetComponent<UIElement>();
		animationRegex = new Regex (strRegex, RegexOptions.None);
		this.enabled = false;
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
		instance.enabled = true;
		instance.lineNumber = -1;
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
			Debug.Log ("Conversee has nothing else to say! Ending conversation!");
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
		lineNumber = -1;
    }

    private void advanceCurrentConversation(int index)
    {
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
			MatchCollection matches = animationRegex.Matches(line);
			int animationCount = matches.Count;
			if(animationCount>1){
				throw new System.Exception("Character with Multiple animations on one conversation line!! Object: " + gameObject.name);
			}
			if(animationCount > 0){
				foreach(Match m in matches){
					if(m.Success){
						Debug.Log ("Playing animation: "+m.Groups[1].Value);
						currentConversee.playAnimation(m.Groups[1].Value);
					}
				}
			}

			string saidText = animationRegex.Replace(line,"");
            ConverseeText.text = saidText;
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
