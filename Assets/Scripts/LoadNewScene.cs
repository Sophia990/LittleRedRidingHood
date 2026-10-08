using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadNewScene : MonoBehaviour
{
    public string sceneName;


    void OnEnable()
    {
        GameObject.Find("UIManager").GetComponent<SceneTransition>().LoadScene(sceneName);
    }
}
