using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPort : Port
{
    public LineRenderer wirePrefab;

    private OutputPort prev;
    private LineRenderer wire;

    public OutputPort Prev
    {
        get{ return prev; }
        set
        {
            if(prev != null && prev != value)
            {
                OutputPort temp = prev;
                prev = null;
                temp.removeNext(this);

                Destroy(wire.gameObject);
            }

            if(value != null && prev != value)
            {
                prev = value;
                if(!prev.containsNext(this))
                {
                    prev.addNext(this);
                }

                wire = GameObject.Instantiate(wirePrefab, transform.position, Quaternion.identity, transform).GetComponent<LineRenderer>();
                wire.SetPosition(1, prev.transform.position - this.transform.position);
            }
        }
    }

    public override bool Status
    {
        get{ return status; }
        set
        {
            if(value != status)
            {
                status = value;
                gate.setInput(status, portID);
            }

            updateColor();
        }
    }

    public override void updateWire()
    {
        if(wire != null)
        {
            wire.SetPosition(1, prev.transform.position - this.transform.position);
        }
    }
}
