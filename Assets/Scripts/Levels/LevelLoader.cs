using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public GameObject inputPref;
    public GameObject outputPref;
    public Transform inputPos;
    public Transform outputPos;
    public AudioSource audioSource;
    public AudioClip clickSound;
    public AudioClip levelCompleteSound;
    public int step = 0;
    public List<int> stepFails = new List<int>();
    public int currentLevel;
    public Level[] levels;

    private LevelInputLoader inputLoader;
    private LevelOutputLoader outputLoader;

    private GateInstance inputGate;
    private GateInstance outputGate;
    private bool hasSimulated = false;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        loadLevel();
    }

    private void loadLevel()
    {
        if(currentLevel >= levels.Length)
        {
            // Winner!
            NextLevelController nextLevelController = GameObject.FindObjectOfType<NextLevelController>();
            nextLevelController.loadScene();
            return;
        }

        clear();
        LevelPanelManager.instance.loadLevel(levels[currentLevel]);

        if(inputLoader == null)
        {
            inputLoader = GameObject.Instantiate(inputPref, inputPos.position, Quaternion.identity).GetComponent<LevelInputLoader>();
        }

        if(outputLoader == null)
        {
            outputLoader = GameObject.Instantiate(outputPref, outputPos.position, Quaternion.identity).GetComponent<LevelOutputLoader>();
        }

        inputLoader.gate = levels[currentLevel].input;
        inputLoader.load();
        inputGate = inputLoader.gameObject.GetComponent<GateInstance>();

        outputLoader.gate = levels[currentLevel].output;
        outputLoader.load();
        outputGate = outputLoader.gameObject.GetComponent<GateInstance>();

        stepFails = new List<int>();
        step = 0;
    }

    private void clear()
    {
        GateInstance[] gates = GameObject.FindObjectsOfType<GateInstance>();
        foreach(GateInstance g in gates)
        {
            Destroy(g.gameObject);
            inputLoader = null;
            outputLoader = null;
            step = 0;
            stepFails.Clear();
        }
    }

    public void hardReset()
    {
        loadLevel();
    }

    public void resetLevel()
    {
        if(currentLevel <= levels.Length)
        {
            LevelPanelManager.instance.loadLevel(levels[currentLevel]);
        }
        stepFails.Clear();
        step = 0;
    }

    private void levelComplete()
    {
        LevelCompleteManager.instance.completeLevel(levels[currentLevel]);
        audioSource.PlayOneShot(levelCompleteSound);
        Controller.instance.playing = false;
        foreach(Gate g in levels[currentLevel].rewards)
        {
            GateDrawerManager.instance.addGate(g);
        }
        currentLevel++;
        loadLevel();
    }

    public void tick()
    {
        if(currentLevel >= levels.Length)
        {
            return;
        }

        if(step >= Mathf.Pow(2f, levels[currentLevel].input.NumInputs))
        {
            // If no failures, go to next level
            if(stepFails.Count <= 0)
            {
                levelComplete();
                return;
            }
            else
            {
                resetLevel();
                return;
            }
        }

        if(!hasSimulated)
        {
            audioSource.PlayOneShot(clickSound);
            for(int i = 0; i < inputGate.gate.NumInputs; i++)
            {
                inputGate.setOutput(BinaryUtility.getBit(step, i), i);
            }

            verifyOutput();
            hasSimulated = true;
        }

        else
        {
            step++;
            hasSimulated = false;
        }
    }

    private void verifyOutput()
    {
        if(outputGate.getInputs() != inputGate.getOutputs(step))
        {
            stepFails.Add(step);
            LevelPanelManager.instance.addInput(outputGate.getInputs(), false);
        }
        else
        {
            LevelPanelManager.instance.addInput(outputGate.getInputs(), true);
        }
    }
}
