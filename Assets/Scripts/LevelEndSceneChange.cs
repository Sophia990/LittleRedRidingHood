using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndSceneChange : MonoBehaviour
{

    public string sceneName;


    void OnTriggerEnter2D()
    {
        GameObject.Find("UIManager").GetComponent<SceneTransition>().LoadScene(sceneName);
    }
}
