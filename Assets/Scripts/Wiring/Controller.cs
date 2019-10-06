using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour
{
    private static readonly float ACTION_COOLDOWN = .2f;
    private static readonly float TICK_TIME = .25f;

    public static Controller instance;

    public Actions actions;
    public LayerMask selectionMask;
    public LineRenderer wirePrefab;
    public GameObject gatePrefab;
    public AudioSource audioSource;
    public AudioClip wireConnectSound;
    public AudioClip wireDisconnectSound;
    public AudioClip gatePickupSound;
    public AudioClip gateDropSound;

    private LineRenderer wireInstance;
    private LevelLoader levelLoader;
    public Object selected;
    private Vector2 mousePos;

    private float lastSelectionTime = float.MinValue;
    private float lastActivateTime = float.MinValue;
    private float lastTickTime = float.MinValue;

    public bool playing = false;

    private LevelLoader LevelLoader
    {
        get
        {
            if(levelLoader == null)
            {
                levelLoader = GameObject.FindObjectOfType<LevelLoader>();
            }

            return levelLoader;
        }

        set{ levelLoader = value; }
    }
    

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }

        instance = this;
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

        instance = this;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(playing)
        {
            
            if(Time.time - lastTickTime >= TICK_TIME)
            {
                LevelLoader.tick();
                lastTickTime = Time.time;
            }

            if(selected != null)
            {
                selected = null;
            }
        }

        if(selected != null)
        {
            if(selected.GetType().IsSubclassOf(typeof(Port)))
            {
                if(wireInstance == null)
                {
                    wireInstance = GameObject.Instantiate(wirePrefab, Vector3.zero, Quaternion.identity);
                    wireInstance.SetPosition(0, ((Port)selected).transform.position);
                }
                Plane p = new Plane(Vector3.back, Vector3.up);
                Ray ray = Camera.main.ScreenPointToRay(mousePos);
                float enter;
                if(p.Raycast(ray, out enter))
                {
                    wireInstance.SetPosition(1, ray.GetPoint(enter));
                }
            }

            else if(selected.GetType().IsAssignableFrom(typeof(GateInstance)))
            {
                GateInstance selectedGate = (GateInstance)selected;
                Plane p = new Plane(Vector3.back, Vector3.up);
                Ray ray = Camera.main.ScreenPointToRay(mousePos);
                float enter;
                if(p.Raycast(ray, out enter))
                {
                    selectedGate.transform.position = ray.GetPoint(enter);
                }

                selectedGate.updateWires();
            }
        }
        else
        {
            if(wireInstance != null)
            {
                Destroy(wireInstance);
            }
        }
    }

    public void deselect()
    {
        selected = null;
    }

    public void removeSelected()
    {
        if(selected.GetType().IsSubclassOf(typeof(Port)))
        {
            if(selected.GetType().Equals(typeof(InputPort)))
            {
                ((InputPort)selected).Prev = null;
            }
            selected = null;

            audioSource.PlayOneShot(wireDisconnectSound);
        }

        else
        {
            selected = null;
        }
    }

    // Only Ports and Gates should be selected!
    public void select()
    {
        if(playing)
        {
            return;
        }

        if(Time.time - lastSelectionTime <=  ACTION_COOLDOWN)
        {
            return;
        }
        lastSelectionTime = Time.time;

        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = mousePos;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        for(int i = 0; i < results.Count; i++)
        {
            if(results[i].gameObject == null)
            {
                continue;
            }

            if(selected != null)
            {
                // Handle what happens when port is selected
                if(selected.GetType().IsSubclassOf(typeof(Port)))
                {
                    Port hitPort = results[i].gameObject.GetComponent<Port>();
                    if(hitPort != null)
                    {
                        // Both are ports, make sure they aren't the same type
                        if(!selected.GetType().Equals(hitPort.GetType()))
                        {
                            if(selected.GetType().Equals(typeof(InputPort)))
                            {
                                ((OutputPort)hitPort).addNext((InputPort)selected);
                                audioSource.PlayOneShot(wireConnectSound);
                                deselect();
                                return;
                            }
                            else
                            {
                                ((OutputPort)selected).addNext((InputPort)hitPort);
                                audioSource.PlayOneShot(wireConnectSound);
                                deselect();
                                return;
                            }
                        }
                    }
                }
            }

            else // Selected null, select what we clicked on
            {
                Port hitPort = results[i].gameObject.GetComponent<Port>();
                GateInstance gateInstance = results[i].gameObject.GetComponent<GateInstance>();
                GateDrawerLoader gateDrawerItem = results[i].gameObject.GetComponent<GateDrawerLoader>();

                if(hitPort != null)
                {
                    selected = hitPort;
                    audioSource.PlayOneShot(wireConnectSound);
                    return;
                }
                else if(gateInstance != null)
                {
                    selected = gateInstance;
                    audioSource.PlayOneShot(gatePickupSound);
                    return;
                }
                else if(gateDrawerItem != null)
                {
                    GateLoader loader = GameObject.Instantiate(gatePrefab, Vector3.zero, Quaternion.identity).GetComponent<GateLoader>();
                    loader.gate = gateDrawerItem.gate;
                    loader.load();
                    selected = loader.Instance;
                    audioSource.PlayOneShot(gatePickupSound);
                    return;
                }
            }
        }

        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100);
        if(selected != null)
        {
            // Handle what happens when port is selected
            if(selected.GetType().IsSubclassOf(typeof(Port)))
            {
                if(hit.collider != null)
                {
                    Port hitPort = hit.collider.GetComponent<Port>();
                    if(hitPort != null)
                    {
                        // Both are ports, make sure they aren't the same type
                        if(!selected.GetType().Equals(hitPort.GetType()))
                        {
                            if(selected.GetType().Equals(typeof(InputPort)))
                            {
                                ((OutputPort)hitPort).addNext((InputPort)selected);
                                audioSource.PlayOneShot(wireConnectSound);
                                deselect();
                                return;
                            }
                            else
                            {
                                ((OutputPort)selected).addNext((InputPort)hitPort);
                                audioSource.PlayOneShot(wireConnectSound);
                                deselect();
                                return;
                            }
                        }
                    }
                }
                else
                {
                    removeSelected();
                    return;
                }
            }

            else if(selected.GetType().IsAssignableFrom(typeof(GateInstance)))
            {
                deselect();
                audioSource.PlayOneShot(gateDropSound);
                return;
            }
        }
        else if(hit.collider != null)    // Selected null, select what we clicked on
        {
            Port hitPort = hit.collider.GetComponent<Port>();
            GateInstance gateInstance = hit.collider.GetComponent<GateInstance>();

            if(hitPort != null)
            {
                selected = hitPort;
                audioSource.PlayOneShot(wireConnectSound);
                return;
            }
            else if(gateInstance != null)
            {
                selected = gateInstance;
                audioSource.PlayOneShot(gatePickupSound);
                return;
            }
        }

        if(selected != null)
        {
            removeSelected();
        }
    }

    public void activate()
    {
        if(Time.time - lastActivateTime <=  ACTION_COOLDOWN)
        {
            return;
        }
        lastActivateTime = Time.time;

        if(LevelCompleteManager.instance.Enabled)
        {
            LevelCompleteManager.instance.Enabled = false;
        }
        else
        {
            playing = !playing;
            if(playing)
            {
                LevelLoader.resetLevel();
            }
        }
    }

    public void delete()
    {
        if(selected != null)
        {
            if(selected.GetType().IsAssignableFrom(typeof(Port)))
            {
                removeSelected();
            }
            else if(selected.GetType().IsAssignableFrom(typeof(GateInstance)))
            {
                Destroy(((GateInstance)selected).gameObject);
                deselect();
            }
        }
    }

    public void reset()
    {
        LevelLoader.hardReset();
    }

    public void UpdateMousePosition(InputAction.CallbackContext ctx)
    {
        mousePos = ctx.ReadValue<Vector2>();
    }
}
