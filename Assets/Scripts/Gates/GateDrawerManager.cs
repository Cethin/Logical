using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateDrawerManager : MonoBehaviour
{
    public static GateDrawerManager instance;

    public GameObject GateDrawerItemPref;

    private List<GateDrawerLoader> gateLoaders = new List<GateDrawerLoader>();

    public void addGate(Gate g)
    {
        if(!contains(g))
        {
            GateDrawerLoader l = GameObject.Instantiate(GateDrawerItemPref, transform).GetComponent<GateDrawerLoader>();
            l.gate = g;
            l.load();
            gateLoaders.Add(l);
        }
    }

    public bool contains(Gate g)
    {
        return indexOf(g) >= 0;
    }

    public int indexOf(Gate g)
    {
        for(int i = 0; i < gateLoaders.Count; i++)
        {
            if(gateLoaders[i].gate == g)
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }

        else
        {
            instance = this;
        }
    }
}
