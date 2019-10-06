using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateInstance : MonoBehaviour
{
    public Gate gate;

    private InputPort[] inputs;
    private OutputPort[] outputs;

    public void setInputPorts(InputPort[] ports)
    {
        if(inputs != null)
        {
            for(int i = 0; i < inputs.Length; i++)
            {
                if(inputs[i] != null)
                {
                    bool match = false;
                    for(int p = 0; p < ports.Length; p++)
                    {
                        if(inputs[i] == ports[p])
                        {
                            match = true;
                            continue;
                        }
                    }

                    if(!match)
                    {
                        Destroy(inputs[i]);
                    }
                }
            }
        }

        inputs = ports;
    }

    public void setOutputPorts(OutputPort[] ports)
    {
        if(outputs != null)
        {
            for(int i = 0; i < outputs.Length; i++)
            {
                if(outputs[i] != null)
                {
                    bool match = false;
                    for(int p = 0; p < ports.Length; p++)
                    {
                        if(outputs[i] == ports[p])
                        {
                            match = true;
                            continue;
                        }
                    }

                    if(!match)
                    {
                        Destroy(outputs[i]);
                    }
                }
            }
        }
        
        outputs = ports;
    }

    public void setInput(bool value, int id)
    {
        inputs[id].Status = value;
        updateOutput();
    }

    public void setOutput(bool value, int id)
    {
        outputs[id].Status = value;
    }

    private void updateOutput()
    {
        if(inputs != null && outputs != null)
        {
            int output = gate.outputs[getInputs()];
            for(int i = 0; i < outputs.Length; i++)
            {
                outputs[i].Status = (output & (0b1 << i)) > 0;
            }
        } // Otherwise this must be the level input gate
    }

    public int getInputs()
    {
        int value = 0;
        for(int i = 0; i < inputs.Length; i++)
        {
            value |= (inputs[i].Status ? 0b1 : 0) << i;
        }

        return value;
    }

    public int getOutputs()
    {
        return getOutputs(getInputs());
    }

    public int getOutputs(int value)
    {
        return gate.outputs[value];
    }

    public void updateWires()
    {
        updateWires(inputs);
        updateWires(outputs);
    }
    public void updateWires(Port[] ports)
    {
        if(ports != null)
        {
            foreach(Port p in ports)
            {
                if(p != null)
                {
                    p.updateWire();
                }
            }
        }
    }

    public void disconnectInputs()
    {
        if(inputs != null)
        {
            for(int i = 0; i < inputs.Length; i++)
            {
                inputs[i].Prev = null;
            }
        }
    }

    public void disconnectOutputs()
    {
        if(outputs != null)
        {
            for(int i = 0; i < outputs.Length; i++)
            {
                outputs[i].removeAll();
            }
        }
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        disconnectInputs();
        disconnectOutputs();
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        updateOutput();
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        updateOutput();
    }
}
