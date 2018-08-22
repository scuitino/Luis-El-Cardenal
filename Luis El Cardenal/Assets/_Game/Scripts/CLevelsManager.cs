using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CLevelsManager : MonoBehaviour {

    #region SINGLETON PATTERN
    public static CLevelsManager _instance = null;
    #endregion

    // is first time?
    public bool _tutorialUsed;

    // true if the level is cleared
    [SerializeField]
    public bool _level1, _level2, _level3, _level4, _level5, _level6, _level7, _level8;

    [SerializeField]
    int _clearedLevel;

    // last animation viewed?
    public bool _congratulation;

    // to load the next scene
    public int _nextScene;

    // to use with loading screen
    public void SetNextScene(int _index)
    {
        Debug.Log(_index);
        _nextScene = _index;
    }

    // return to main menu
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void Awake()
    {
        //Singleton check
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }            
        else if (_instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        if (!_tutorialUsed)
        {
            CMainMenu._instance.PlayHelp();
            _tutorialUsed = true;
        }          
    }

    public void CheckLevelsState()
    {
        if (_level1)
        {
            CMainMenu._instance.EnableColorSprite(1);
        }
        if (_level2)
        {
            CMainMenu._instance.EnableColorSprite(2);
        }
        if (_level3)
        {
            CMainMenu._instance.EnableColorSprite(3);
        }
        if (_level4)
        {
            CMainMenu._instance.EnableColorSprite(4);
        }
        if (_level5)
        {
            CMainMenu._instance.EnableColorSprite(5);
        }
        if (_level6)
        {
            CMainMenu._instance.EnableColorSprite(6);
        }
        if (_level7)
        {
            CMainMenu._instance.EnableColorSprite(7);
        }
        if (_level8)
        {
            CMainMenu._instance.EnableColorSprite(8);
        }
    }
}
