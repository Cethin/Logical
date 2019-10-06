using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( menuName = "Logic Gate")]
[System.Serializable]
public class Gate : ScriptableObject
{
    public string displayName = "";

    [SerializeField]
    private int numInputs = 0;
    public int NumInputs
    {
        get { return numInputs; }
        set
        {
            if(value != numInputs)
            {
                numInputs = value;
                resizeOutputsArray();
            }
        }
    }

    [SerializeField]
    private int numOutputs = 0;
    public int NumOutputs
    {
        get { return numOutputs; }
        set
        {
            if(value != numOutputs && numInputs <= 0)
            {
                resizeOutputsArray();
            }
            numOutputs = value;
        }
    }

    [SerializeField]
    public int[] outputs;

    public void resizeOutputsArray()
    {
        if(NumOutputs > 0 && NumInputs <= 0)
        {
            resizeOutputsArray(1);
        }
        else
        {
            resizeOutputsArray((int)Mathf.Pow(2, NumInputs));
        }
    }
    public void resizeOutputsArray(int length)
    {
        int[] temp = new int[length];
        for(int i = 0; i < length && outputs != null && i < outputs.Length; i++)
        {
            temp[i] = outputs[i];
        }

        outputs = temp;
    }

}
