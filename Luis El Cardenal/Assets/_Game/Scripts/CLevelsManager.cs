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

    // continue panel
    [SerializeField]
    Animator _continueAnimator;

    public bool[] _saveArrayFile = new bool[8];

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
        //Debug.Log(_index);
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
        // if the savefile exits, ask if the player want to continue
        if (ES3.KeyExists("saveFile"))
        {            
            _continueAnimator.SetBool("Active",true);
        }
        else
        {
            NewGame();
        }
    }

    // when star new game
    public void NewGame()
    {
        if (!_tutorialUsed) //when the app is open
        {
            CMainMenu._instance.PlayHelp();
            _tutorialUsed = true;
        }
        _continueAnimator.SetBool("Active", false);
    }

    // color cleared levels
    public void CheckLevelsState()
    {
        if (_level1)
        {
            CMainMenu._instance.EnableColorSprite(1);
            _saveArrayFile[0] = true;
        }
        if (_level2)
        {
            CMainMenu._instance.EnableColorSprite(2);
            _saveArrayFile[1] = true;
        }
        if (_level3)
        {
            CMainMenu._instance.EnableColorSprite(3);
            _saveArrayFile[2] = true;
        }
        if (_level4)
        {
            CMainMenu._instance.EnableColorSprite(4);
            _saveArrayFile[3] = true;
        }
        if (_level5)
        {
            CMainMenu._instance.EnableColorSprite(5);
            _saveArrayFile[4] = true;
        }
        if (_level6)
        {
            CMainMenu._instance.EnableColorSprite(6);
            _saveArrayFile[5] = true;
        }
        if (_level7)
        {
            CMainMenu._instance.EnableColorSprite(7);
            _saveArrayFile[6] = true;
        }
        if (_level8)
        {
            CMainMenu._instance.EnableColorSprite(8);
            _saveArrayFile[7] = true;
        }

        Debug.Log("guardando");
        SaveData();
    }

    // save progress
    public void SaveData()
    {
        ES3.Save<bool[]>("saveFile", _saveArrayFile);
    }

    // load progress
    public void LoadData()
    {
        // if the savefile exits, load from it
        if (ES3.KeyExists("saveFile"))
        {
            _saveArrayFile = ES3.Load<bool[]>("saveFile");
            _tutorialUsed = true;
            if (_saveArrayFile[0])
            {
                CMainMenu._instance.EnableColorSprite(1);
                _level1 = true;
            }
            if (_saveArrayFile[1])
            {
                CMainMenu._instance.EnableColorSprite(2);
                _level2 = true;
            }
            if (_saveArrayFile[2])
            {
                CMainMenu._instance.EnableColorSprite(3);
                _level3 = true;
            }
            if (_saveArrayFile[3])
            {
                CMainMenu._instance.EnableColorSprite(4);
                _level4 = true;
            }
            if (_saveArrayFile[4])
            {
                CMainMenu._instance.EnableColorSprite(5);
                _level5 = true;
            }
            if (_saveArrayFile[5])
            {
                CMainMenu._instance.EnableColorSprite(6);
                _level6 = true;
            }
            if (_saveArrayFile[6])
            {
                CMainMenu._instance.EnableColorSprite(7);
                _level7 = true;
            }
            if (_saveArrayFile[7])
            {
                CMainMenu._instance.EnableColorSprite(8);
                _level8 = true;
            }
        }        
        _continueAnimator.SetBool("Active", false);
    }
}
