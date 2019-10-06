using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelController : MonoBehaviour
{
    [SerializeField]
    public string nextScene;

    public void loadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
    }
}
