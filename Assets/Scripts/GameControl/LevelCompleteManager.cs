using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelCompleteManager : MonoBehaviour
{
    private static readonly string str = "{0} Complete!\nUnlocks:";

    public static LevelCompleteManager instance;

    public GameObject gateDrawerItemPref;
    public TMP_Text text;
    public GameObject panel;
    public GameObject rewardsPanel;

    private List<GameObject> rewards = new List<GameObject>();
    private bool enableState = false;

    public bool Enabled
    {
        get{ return enableState; }
        set
        {
            panel.SetActive(value);
            enableState = value;

            if(value == false)
            {
                clear();
            }
        }
    }

    public void enable()
    {
        Enabled = true;
    }

    public void disable()
    {
        Enabled = false;
    }

    public void completeLevel(Level l)
    {
        clear();
        text.text = string.Format(str, l.levelName);

        for(int i = 0; i < l.rewards.Length; i++)
        {
            GateDrawerLoader loader = GameObject.Instantiate(gateDrawerItemPref, rewardsPanel.transform).GetComponent<GateDrawerLoader>();
            loader.gate = l.rewards[i];
            loader.load();
            rewards.Add(loader.gameObject);
        }

        Enabled = true;
    }

    private void clear()
    {
        foreach(GameObject go in rewards)
        {
            Destroy(go);
        }
        rewards.Clear();
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
