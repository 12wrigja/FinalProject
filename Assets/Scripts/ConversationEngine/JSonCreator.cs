using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class JSonCreator : MonoBehaviour{
	public static void CreateJSon() {
		StreamReader reader = new StreamReader(Application.dataPath+"/Resources/tojson.txt");
		string line;
		List<string> charList = new List<string>();
		List<int> charLineCount = new List<int>();
		List<string> linelist = new List<string>();
		List<List<string>> responseList = new List<List<string>>();
		List<List<int>> toStateList = new List<List<int>>();
		int lineCount = 0;
		while(reader.EndOfStream == false) {
			line = reader.ReadLine();
			if(line != null) {
				if(line[0].Equals('"')) {
					charList.Add(line.Substring(1,(line.Length - 2)));
					if(charList.Count != 1) {
						charLineCount.Add(lineCount);
					}
					lineCount = 0;
					line = reader.ReadLine();
					linelist.Add(line);
					line = reader.ReadLine();
					lineCount++;
				} else {
					linelist.Add(line);
					line = reader.ReadLine();
					lineCount++;
				}
			}
			if(line != null) {
				List<string> response = new List<string>();
				StringBuilder builder = new StringBuilder();
				for(int i = 0;i < line.Length;i++) {
					if(i < line.Length +1) {
						if(line[i].Equals(':') && line[i+1].Equals(':')) {
							response.Add(builder.ToString());
							builder = new StringBuilder();
							i++;
						}
						else {
							builder.Append(line[i]);
						}
					}
					else {
						builder.Append(line[i]);
					}
				}
				response.Add(builder.ToString());
				responseList.Add(response);
				line = reader.ReadLine();
			}
			if(line != null) {
				StringBuilder builder = new StringBuilder();
				List<int> toState = new List<int>();
				for(int i = 0;i < line.Length;i++) {
					if(line[i].Equals(',') == false) {
						builder.Append(line[i]);
					} else {
						int conversion = Convert.ToInt32(builder.ToString());
						toState.Add(conversion);
						builder = new StringBuilder();
					}
				}
				int conver = Convert.ToInt32(builder.ToString());
				toState.Add(conver);
				builder = new StringBuilder();
				toStateList.Add(toState);
			}
		}
		charLineCount.Add(lineCount);
		List<string> file = new List<string>();
		int charCount = 0;
		file.Add("{");
		file.Add("\"char\": [");
		file.Add("{");
		file.Add("\"name\" : \"" + charList[0] + "\",");
		file.Add("\"lines\" : [");
		lineCount = 0;
		int cntr = 0;
		while(cntr < linelist.Count) {
			if((charCount < charLineCount.Count) && (lineCount < charLineCount[charCount])){
				file.Add("{");
				file.Add("\"line\": [");
				file.Add("\"" + linelist[cntr] + "\"");
				file.Add("],");
				file.Add("\"state\": " + lineCount + ",");
				lineCount++;
				file.Add("\"options\" : [");
				for(int c = 0;c < responseList[cntr].Count;c++) {
					if(c + 1 < responseList[cntr].Count) {
						file.Add("\"" + responseList[cntr][c] +"\",");
					} else {
						file.Add("\"" + responseList[cntr][c] +"\"");
					}
				}
				file.Add("],");
				file.Add("\"tostate\": [");
				for(int c = 0;c < toStateList[cntr].Count;c++){
					if(c + 1 < responseList[cntr].Count) {
						file.Add("\"" + toStateList[cntr][c] +"\",");
					} else {
						file.Add("\"" + toStateList[cntr][c] +"\"");
					}
				}
				file.Add("]");
				if(lineCount < charLineCount[charCount]) {
					file.Add("},");
				}
				else {
					file.Add("}");
				}
				cntr++;
			} else {
				charCount++;
				file.Add("]");
				if(charCount < charList.Count) {
					if(charCount != 0) {
						file.Add("},");
					}
					file.Add("{");
					file.Add("\"name\" : \"" + charList[charCount] + "\",");
					file.Add("\"lines\" : [");
					lineCount = 0;
				} else {
					file.Add("}");
				}
			}
		}
		file.Add("]");
		file.Add("}");
		file.Add("]");
		file.Add("}");
		System.IO.File.WriteAllLines("Assets/Resources/script.txt",file.ToArray());
	}
}