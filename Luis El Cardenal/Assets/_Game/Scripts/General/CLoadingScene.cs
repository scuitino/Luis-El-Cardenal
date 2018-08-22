using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CLoadingScene : MonoBehaviour {

    private void Start()
    {
        Invoke("LoadNextScene",1);
    }

    // load async
    public void LoadNextScene()
    {
        SceneManager.LoadSceneAsync(CLevelsManager._instance._nextScene);
    }
}
