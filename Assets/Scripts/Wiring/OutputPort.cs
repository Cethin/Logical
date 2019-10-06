using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputPort : Port
{

    private List<InputPort> next;
    public InputPort getNext(int i)
    {
        return next[i];
    }

    public InputPort addNext(InputPort value)
    {
        if(value == null)
        {
            return null;
        }

        if(next == null)
        {
            next = new List<InputPort>();
        }

        int existingIndex = next.IndexOf(value);
        if(existingIndex >= 0)
        {
            // Already contains value, do nothing.
        }
        else
        {
            next.Add(value);
            if(value.Prev != this)
            {
                value.Prev = this;
            }

            value.Status = status;
        }

        return value;
    }

    public InputPort removeNext(InputPort value)
    {
        if(next.Contains(value))
        {
            value.Prev = null;
            next.Remove(value);
            return value;
        }

        return null;
    }

    public void removeAll()
    {
        if(next != null)
        {
            for(int i = 0; i < next.Count; i++)
            {
                next[i].Prev = null;
            }

            next.Clear();
        }
    }

    public bool containsNext(InputPort value)
    {
        return next.Contains(value);
    }

    public override bool Status
    {
        get{ return status; }
        set
        {
            if(value != status)
            {
                status = value;
                
                // Update the next in line
                if(next != null)
                {
                    for(int i = 0; i < next.Count; i++)
                    {
                        next[i].Status = status;
                    }
                }
            }

            updateColor();
        }
    }

    public override void updateWire()
    {
        if(next != null)
        {
            foreach(InputPort p in next)
            {
                if(p != null)
                {
                    p.updateWire();
                }
            }
        }
    }
}
