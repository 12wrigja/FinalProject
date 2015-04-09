using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(AnxietySystem))]
public class AnxietySystemEditor : Editor {
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Increase Anxiety"))
        {
            ((AnxietySystem)target).increaseAnxiety(((AnxietySystem)target).StaminaIncreaseOverTime);
        }
        else if (GUILayout.Button("Decrease Anxiety"))
        {
            ((AnxietySystem)target).increaseAnxiety(((AnxietySystem)target).StaminaDecreaseOverTime);
        }
    }
}
