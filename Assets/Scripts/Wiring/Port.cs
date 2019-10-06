using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Port : MonoBehaviour
{
    public Color offColor;
    public Color onColor;
    public SpriteRenderer spriteRenderer;

    public GateInstance gate;
    public int portID;

    protected bool status = false;
    public abstract bool Status{ get; set; }

    protected void updateColor()
    {
        if(status)
        {
            spriteRenderer.color = onColor;
        }
        else
        {
            spriteRenderer.color = offColor;
        }
    }

    public void setColor(Color c)
    {
        spriteRenderer.color = c;
    }

    public abstract void updateWire();

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        updateColor();
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        updateColor();
    }
}
