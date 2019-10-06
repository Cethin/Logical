using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOutputLoader : StandardGateLoader
{
    public override void load()
    {
        resize();
        loadText();
        loadInputs();
    }
}
