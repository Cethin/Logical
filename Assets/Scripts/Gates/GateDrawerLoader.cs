using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GateDrawerLoader : MonoBehaviour
{
    public Gate gate;
    public GameObject inputPortPref;
    public GameObject outputPortPref;
    public TMP_Text text;

    private RectTransform rectTransform;
    private RectTransform RT
    {
        get
        {
            if(rectTransform == null)
            {
                rectTransform = GetComponent<RectTransform>();
            }

            return rectTransform;
        }
    }

    public void load()
    {
        text.text = gate.displayName;

        loadInputs();
        loadOutputs();
    }

    public void loadInputs()
    {
        float xOffset = (RT.sizeDelta.x/2f) -3;
        float yOffset = (RT.sizeDelta.y/2f) -3;
        float portSpacing = (RT.sizeDelta.y-6) / gate.NumInputs;

        for(int i = 0; i < gate.NumInputs; i++)
        {
            GameObject.Instantiate(inputPortPref, transform.position + new Vector3(-xOffset, -yOffset + (portSpacing * (i+1) - portSpacing/2), 0), Quaternion.identity, transform);
        }
    }

    public void loadOutputs()
    {
        float xOffset = (RT.sizeDelta.x/2f) -3;
        float yOffset = (RT.sizeDelta.y/2f) -3;
        float portSpacing = (RT.sizeDelta.y-6) / gate.NumOutputs;

        for(int i = 0; i < gate.NumOutputs; i++)
        {
            GameObject.Instantiate(outputPortPref, transform.position + new Vector3(xOffset, -yOffset + (portSpacing * (i+1) - portSpacing/2), 0), Quaternion.identity, transform);
        }
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if(gate != null)
        {
            load();
        }
    }
}
