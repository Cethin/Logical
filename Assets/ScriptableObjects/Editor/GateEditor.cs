using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor( typeof( Gate ) )]
public class GateEditor : Editor
{
    Gate t;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        t = (Gate)target;
    }

    public override void OnInspectorGUI()
    {
        
        EditorGUI.BeginChangeCheck();
        string name = EditorGUILayout.TextField("Name", t.displayName);
        if(EditorGUI.EndChangeCheck())
        {
            t.displayName = name;
            EditorUtility.SetDirty(t);
        }
        

        EditorGUILayout.BeginHorizontal();
        // Number of inputs editor
        EditorGUI.BeginChangeCheck();
        int numInputs = EditorGUILayout.IntField("Number of Inputs", t.NumInputs);
        if(EditorGUI.EndChangeCheck())
        {
            t.NumInputs = numInputs;
            EditorUtility.SetDirty(t);
        }

        // Number of outputs editor
        EditorGUI.BeginChangeCheck();
        int numOutputs = EditorGUILayout.IntField("Number of Outputs", t.NumOutputs);
        if(EditorGUI.EndChangeCheck())
        {
            t.NumOutputs = numOutputs;
            EditorUtility.SetDirty(t);
        }
        EditorGUILayout.EndHorizontal();

        // Input -> Output mapping editor
        if(t.NumInputs > 0)
        {
            for(int i = 0; i < (int)Mathf.Pow(2, t.NumInputs); i++)
            {
                // EditorGUI.BeginChangeCheck();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(BinaryUtility.intToBinString(i, t.NumInputs), new GUILayoutOption[] { GUILayout.Width(8*t.NumInputs) });
                for(int o = 0; o < t.NumOutputs; o++)
                {
                    EditorGUI.BeginChangeCheck();
                    bool b = EditorGUILayout.Toggle((t.outputs[i] & (0b1 << (t.NumOutputs - 1 - o))) > 0, new GUILayoutOption[] { GUILayout.Width(8*t.NumOutputs + 10) });

                    if(EditorGUI.EndChangeCheck())
                    {
                        int value = 0;
                        int mask = 0;
                        for(int p = 0; p < t.NumOutputs; p++)
                        {
                            if(p != t.NumOutputs - 1 - o)
                            {
                                mask |= 0b1 << p;
                            }
                        }
                        value = (t.outputs[i] & mask) | ((b ? 0b1 : 0) << (t.NumOutputs - 1 - o));
                        t.outputs[i] = value;
                        EditorUtility.SetDirty(t);
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
        }
        else
        {
            if(t.NumOutputs > 0)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Value: ", new GUILayoutOption[] { GUILayout.Width(56) });
                for(int o = 0; o < t.NumOutputs; o++)
                {
                    EditorGUI.BeginChangeCheck();
                    bool b = EditorGUILayout.Toggle((t.outputs[0] & (0b1 << (t.NumOutputs - 1 - o))) > 0, new GUILayoutOption[] { GUILayout.Width((10)) });

                    if(EditorGUI.EndChangeCheck())
                    {
                        int value = 0;
                        int mask = 0;
                        for(int p = 0; p < t.NumOutputs; p++)
                        {
                            if(p != t.NumOutputs - 1 - o)
                            {
                                mask |= 0b1 << p;
                            }
                        }
                        value = (t.outputs[0] & mask) | ((b ? 0b1 : 0) << (t.NumOutputs - 1 - o));
                        t.outputs[0] = value;
                        EditorUtility.SetDirty(t);
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
