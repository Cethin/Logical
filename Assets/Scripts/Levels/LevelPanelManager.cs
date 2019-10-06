using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelPanelManager : MonoBehaviour
{
    public Color correctInputColor;
    public Color errorInputColor;

    public static LevelPanelManager instance;
    public TMP_Text levelName;
    public TMP_Text levelDescription;
    public TMP_Text inputValues;
    public TMP_Text outputValues;
    public TMP_Text ExpectedValues;

    private Level level;

    public void loadLevel(Level l)
    {
        level = l;
        levelName.text = l.levelName;
        levelDescription.text = l.description;

        loadInputs(l);
        loadExpected(l);
        clearOutputValues();
    }

    private void loadInputs(Level l)
    {
        string str = "Input:\n";
        for(int i = 0; i < Mathf.Pow(2f, l.input.NumInputs); i++)
        {
            str += BinaryUtility.intToBinString(i, l.input.NumInputs) + "\n";
        }

        inputValues.text = str;
    }

    private void loadExpected(Level l)
    {
        string str = "Expected:\n";
        for(int i = 0; i < Mathf.Pow(2f, l.input.NumInputs); i++)
        {
            str += BinaryUtility.intToBinString(l.input.outputs[i], l.input.NumOutputs) + "\n";
        }

        ExpectedValues.text = str;
    }

    private void clearOutputValues()
    {
        outputValues.text = "Output:\n";
    }

    public void addInput(int value, bool correct)
    {
        outputValues.text += string.Format("<#{0}>{1}\n", correct ? ColorUtility.ToHtmlStringRGB(correctInputColor) : ColorUtility.ToHtmlStringRGB(errorInputColor), BinaryUtility.intToBinString(value, level.input.NumOutputs));
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
