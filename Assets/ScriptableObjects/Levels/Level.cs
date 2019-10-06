using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( menuName = "Level")]
[System.Serializable]
public class Level : ScriptableObject
{
    [SerializeField]
    public string levelName = "";
    [SerializeField]
    public string description = "";
    [SerializeField]
    public Gate input;
    [SerializeField]
    public Gate output;
    public Gate[] rewards;
}
