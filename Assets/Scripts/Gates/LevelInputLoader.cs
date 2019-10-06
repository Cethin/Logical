using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInputLoader : StandardGateLoader
{
    public override void load()
    {
        resize();
        loadText();
        loadOutputs();
    }

    protected override void loadOutputs()
    {
        // Inputs of Gate are actually the outputs.
        // The outputs correspond to the expected value at the end of the level.
        float yScale = Mathf.Max(gate.NumInputs, gate.NumInputs, 1);
        float bottomOffset = -(yScale / 2);

        // Calculate evenly spaced points for outputs
        if(gate.NumInputs > 0)
        {
            // Vector3[] inputPortPoints = new Vector3[gate.NumInputs];
            float portSpacing = yScale / gate.NumInputs;
            OutputPort[] outputPorts = new OutputPort[gate.NumInputs];
            for(int i = 0; i < gate.NumInputs; i++)
            {
                outputPorts[i] = GameObject.Instantiate(outputPortPref, new Vector3(.5f, bottomOffset + (portSpacing * (i+1) - portSpacing/2), -1) + transform.position, Quaternion.identity, transform).GetComponent<OutputPort>();
            }
            Instance.setOutputPorts(outputPorts);
        }
    }
}
