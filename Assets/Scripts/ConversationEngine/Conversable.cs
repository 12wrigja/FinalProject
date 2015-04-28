using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;
using LitJson;

public class Conversable : MonoBehaviour
{
	public NotificationsManager plotManager;
    public string conversable_tag;
    public string conversee_name;
	public int[] startState;
	public int plotpoint;

    public int current_state = 0;

    private List<int> nextStates;

    private Animator anim;
	private bool plotAdvancement;

    void Start()
    {
		if (plotManager == null)
		{
			throw new System.Exception("Character without plot manager Object: " + gameObject.name);
		} else
		{
			plotManager.AddListener(this,"AdvancePlot");
		}
        if (conversable_tag == null)
        {
            throw new System.Exception("Character without conversation tag!! Object: " + gameObject.name);
        }
        if (conversee_name == null)
        {
            conversee_name = conversable_tag;
        }
        anim = GetComponent<Animator>();
		plotAdvancement = true;
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
		if (nextStates[conversationChoiceIndex] == 100)
		{
			if(plotAdvancement) {
				plotManager.PostNotification(this,"AdvancePlot");
				StartCoroutine(this.PlotTimer());
			}
			return false;
		}
		if (conversationChoiceIndex >= nextStates.Count || nextStates[conversationChoiceIndex] == 0)
        {
			current_state = startState[plotpoint];
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
	public void AdvancePlot()
	{
		plotpoint++;
		current_state = startState[plotpoint];
	}
	public IEnumerator PlotTimer()
	{
		plotAdvancement = false;
		yield return new WaitForSeconds(5);
		plotAdvancement = true;
	}
}
