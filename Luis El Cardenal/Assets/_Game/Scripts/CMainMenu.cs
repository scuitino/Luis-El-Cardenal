using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CMainMenu : MonoBehaviour {

    // return to specific scene
    public void GoToScene(int aSceneIndex)
    {
        SceneManager.LoadScene(aSceneIndex);
    }
}
