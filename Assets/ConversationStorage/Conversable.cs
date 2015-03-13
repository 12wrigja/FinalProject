using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;

public class Conversable : MonoBehaviour {

    public string conversable_tag;
    public string conversee_name;

    private static readonly string conversationFolder = "Assets/Resources/";

    private string delimiter = "::";

    private string conversation_state = "start";

    private List<string> nextStates;

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
    }

    public List<string> GetConversationLines()
    {
        List<string> lines = new List<string>();
        string line;
        StreamReader reader = new StreamReader(conversationFolder+conversable_tag + ".conversation", Encoding.Default);
        using (reader)
        {
            do
            {
                line = reader.ReadLine();
                if (line != null)
                {
                    if (line.Equals(conversation_state + delimiter))
                    {
                        line = reader.ReadLine();
                        while(!line.Equals(delimiter))
                        {
                            lines.Add(line);
                            line = reader.ReadLine();
                        }
                        break;
                    }
                }
            } while (line != null);
            reader.Close();
        }
        return lines;
    }

    public List<string> GetConversationOptions()
    {
        List<string> lines = new List<string>();
        string line;
        StreamReader reader = new StreamReader(conversationFolder + conversable_tag + ".conversation", Encoding.Default);
        nextStates = new List<string>();
        using (reader)
        {
            line = reader.ReadLine();
            while (!line.Equals(conversation_state + delimiter))
            {
                line = reader.ReadLine();
            }
            while (!line.Equals(delimiter))
            {
                line = reader.ReadLine();
            }
            line = reader.ReadLine();
            Debug.Log(line);
            while (!line.Equals(delimiter))
            {
                string[] parts = line.Split(new String[] { delimiter }, StringSplitOptions.None);
                lines.Add(parts[0]);
                nextStates.Add(parts[1]);
                line = reader.ReadLine();
            }
        }
        return lines;
    }

    public void transitionConversation(int conversationChoiceIndex)
    {
        conversation_state = nextStates[conversationChoiceIndex];
    }
}
