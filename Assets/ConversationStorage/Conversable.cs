using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;
using LitJson;

public class Conversable : MonoBehaviour {

    public string conversable_tag;
    public string conversee_name;
	public int start_state;
	private int current_state;

    private static readonly string conversationFolder = "Assets/Resources/";

    private string delimiter = "::";

    private string conversation_state = "start";

    private List<string> nextStates;

    void Start()
    {
		current_state = start_state;
        if (conversable_tag == null)
        {
            throw new System.Exception("Character without conversation tag!! Object: " + gameObject.name);
        }
        if (conversee_name == null)
        {
            conversee_name = conversable_tag;
        }
    }

    public List<string> GetConversationLines()
    {
		List<string> lines = new List<string>();
		TextAsset textfile = (TextAsset)Resources.Load("script");
		JsonData jdata = JsonMapper.ToObject(textfile.text);
		for(int i = 0;i < jdata["char"].Count;i++) {
			if(jdata["char"][i]["name"].Equals(conversee_name)) {
				for(int c = 0;c < jdata["char"][i]["lines"][current_state]["line"].Count;c++) {
					lines.Add(jdata["char"][i]["lines"][current_state]["line"][c].ToString());
				}
			}
		}
		return lines;
    }

    public List<string> GetConversationOptions()
    {
        List<string> lines = new List<string>();
		TextAsset textfile = (TextAsset)Resources.Load("script");
		JsonData jdata = JsonMapper.ToObject(textfile.text);
		for(int i = 0;i < jdata["char"].Count;i++) {
			if(jdata[i]["name"].Equals(conversee_name)) {
				for(int c = 0;c < jdata["char"][i]["lines"][current_state]["options"].Count;c++) {
					lines.Add(jdata["char"][i]["lines"][current_state]["options"][c].ToString());
				}
			}
		}
        return lines;
    }

    public void transitionConversation(int conversationChoiceIndex)
	{
		int index = conversationChoiceIndex - 1;
		List<string> lines = new List<string>();
		TextAsset textfile = (TextAsset)Resources.Load("script");
		JsonData jdata = JsonMapper.ToObject(textfile.text);
		for(int i = 0;i < jdata["char"].Count;i++) {
			if(jdata[i]["name"].Equals(conversee_name)) {
				current_state = (int)jdata["char"][i]["lines"][current_state]["tostate"][index];
			}
		}
	}
}
