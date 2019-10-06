using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GateLoader : StandardGateLoader
{
    public override void load()
    {
        resize();
        loadText();
        loadInputs();
        loadOutputs();
    }
}
