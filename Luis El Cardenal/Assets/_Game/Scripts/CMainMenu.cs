using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CMainMenu : MonoBehaviour {

    // applicaton quit
    public void ExitApplication()
    {
        Application.Quit();
    }

    // return to specific scene
    public void GoToScene(int aSceneIndex)
    {
        SceneManager.LoadScene(aSceneIndex);
    }
}
