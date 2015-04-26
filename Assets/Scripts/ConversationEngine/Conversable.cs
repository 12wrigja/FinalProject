using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;
using LitJson;

public class Conversable : MonoBehaviour
{

    public string conversable_tag;
    public string conversee_name;

    public int current_state = 0;

    private List<int> nextStates;

    private Animator anim;

    void Start()
    {
        if (conversable_tag == null)
        {
            throw new System.Exception("Character without conversation tag!! Object: " + gameObject.name);
        }
        if (conversee_name == null)
        {
            conversee_name = conversable_tag;
        }
        anim = GetComponent<Animator>();
    }

    public List<string> GetConversationLines()
    {
        List<string> lines = new List<string>();
        TextAsset textfile = (TextAsset)Resources.Load("script");
        JsonData jdata = JsonMapper.ToObject(textfile.text);
        for (int i = 0; i < jdata["char"].Count; i++)
        {
            if (jdata["char"][i]["name"].Equals(conversee_name))
            {
                for (int c = 0; c < jdata["char"][i]["lines"][current_state]["line"].Count; c++)
                {
                    lines.Add(jdata["char"][i]["lines"][current_state]["line"][c].ToString());
                }
            }
        }
        return lines;
    }

    public List<string> GetConversationOptions()
    {
        List<string> lines = new List<string>();
        nextStates = new List<int>();
        TextAsset textfile = (TextAsset)Resources.Load("script");
        JsonData jdata = JsonMapper.ToObject(textfile.text);
        for (int i = 0; i < jdata["char"].Count; i++)
        {
            if (jdata["char"][i]["name"].Equals(conversee_name))
            {
                for (int c = 0; c < jdata["char"][i]["lines"][current_state]["tostate"].Count; c++)
                {
                    if (jdata["char"][i]["lines"][current_state]["options"].Count > c)
                    {
                        lines.Add(jdata["char"][i]["lines"][current_state]["options"][c].ToString());
                    }
                    nextStates.Add(Convert.ToInt32(jdata["char"][i]["lines"][current_state]["tostate"][c].ToString()));
                }
            }
        }
        return lines;
    }

    public bool transitionConversation(int conversationChoiceIndex)
    {
		if (conversationChoiceIndex >= nextStates.Count || nextStates[conversationChoiceIndex] == 0)
        {
            return false;
        }
        current_state = nextStates[conversationChoiceIndex];
        return true;
    }

    public void playAnimation(String triggerName)
    {
        if (anim != null)
        {
            anim.SetTrigger(triggerName);
        }
        else
        {
            Debug.Log("Animator is null for object: " + gameObject.name);
        }
    }
}
