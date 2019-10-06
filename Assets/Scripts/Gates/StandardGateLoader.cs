using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class StandardGateLoader : MonoBehaviour
{
    public Gate gate;
    public GameObject inputPortPref;
    public GameObject outputPortPref;
    public TMP_Text text;
    public SpriteRenderer sr;
    public Canvas canvas;

    protected GateInstance instance;
    public GateInstance Instance
    {
        get
        {
            if(instance == null)
            {
                instance = gameObject.AddComponent<GateInstance>();
                instance.gate = gate;
            }

            return instance;
        }
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        load();
    }

    public abstract void load();

    protected void loadText()
    {
        text.text = gate.displayName;
    }

    protected virtual void loadInputs()
    {
        float yScale = Mathf.Max(gate.NumInputs, gate.NumOutputs, 1);
        float bottomOffset = -(yScale / 2);

        // Calculate evenly spaced points for inputs
        if(gate.NumInputs > 0)
        {
            // Vector3[] inputPortPoints = new Vector3[gate.NumInputs];
            float portSpacing = yScale / gate.NumInputs;
            InputPort[] inputPorts = new InputPort[gate.NumInputs];
            for(int i = 0; i < gate.NumInputs; i++)
            {
                inputPorts[i] = GameObject.Instantiate(inputPortPref, new Vector3(-.5f, bottomOffset + (portSpacing * (i+1) - portSpacing/2), -1) + transform.position, Quaternion.identity, transform).GetComponent<InputPort>();
                inputPorts[i].gate = Instance;
                inputPorts[i].portID = i;
            }
            Instance.setInputPorts(inputPorts);
        }
    }

    protected virtual void loadOutputs()
    {
        float yScale = Mathf.Max(gate.NumInputs, gate.NumOutputs, 1);
        float bottomOffset = -(yScale / 2);

        // Calculate evenly spaced points for outputs
        if(gate.NumOutputs > 0)
        {
            // Vector3[] inputPortPoints = new Vector3[gate.NumInputs];
            float portSpacing = yScale / gate.NumOutputs;
            OutputPort[] outputPorts = new OutputPort[gate.NumOutputs];
            for(int i = 0; i < gate.NumOutputs; i++)
            {
                outputPorts[i] = GameObject.Instantiate(outputPortPref, new Vector3(.5f, bottomOffset + (portSpacing * (i+1) - portSpacing/2), -1) + transform.position, Quaternion.identity, transform).GetComponent<OutputPort>();
                outputPorts[i].gate = Instance;
                outputPorts[i].portID = i;
            }
            Instance.setOutputPorts(outputPorts);
        }
    }

    protected virtual void resize()
    {
        float yScale = Mathf.Max(gate.NumInputs, gate.NumOutputs, 1);
        sr.transform.localScale = new Vector2(1, yScale);
        canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(yScale, 1);
    }
}
