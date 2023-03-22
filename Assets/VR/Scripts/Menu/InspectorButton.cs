using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

#if UNITY_EDITOR
[CustomEditor(typeof(ButtonFunction))]

public class InspectorButton : Editor
{
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ButtonFunction button = (ButtonFunction)target;

        if(GUILayout.Button("Button"))
        {
            button.ButtonPressedCall();
        }
    }

}
#endif
