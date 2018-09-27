using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CMainMenu : MonoBehaviour {

    #region SINGLETON PATTERN
    public static CMainMenu _instance = null;
    #endregion

    // to enable color sprite
    [SerializeField]
    Image _level1Image, _level2Image, _level3Image, _level4Image, _level5Image, _level6Image, _level7Image, _level8Image;

    // color sprites
    [SerializeField]
    Sprite _level1Sprite, _level2Sprite, _level3Sprite, _level4Sprite, _level5Sprite, _level6Sprite, _level7Sprite, _level8Sprite;

    // cleared level flags
    [SerializeField]
    GameObject _flag1, _flag2, _flag3, _flag4, _flag5, _flag6, _flag7, _flag8;

    [SerializeField]
    Animator _helpAnimator;

    [SerializeField]
    Animator _luisAnimator;

    [SerializeField]
    GameObject _skipButton;

    public GameObject _exampleItemsContainer;

    private void Awake()
    {
        _luisAnimator.SetBool("Talking", false);
        //Singleton check
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        if (CLevelsManager._instance._tutorialUsed)
        {
            _luisAnimator.GetComponent<Button>().enabled = true;
            CLevelsManager._instance.CheckLevelsState();
        }        
    }

    public void EnableColorSprite(int pLevel)
    {
        switch (pLevel)
        {
            case 1:
                _level1Image.sprite = _level1Sprite;
                _flag1.SetActive(true);
                break;
            case 2:
                _level2Image.sprite = _level2Sprite;
                _flag2.SetActive(true);
                break;
            case 3:
                _level3Image.sprite = _level3Sprite;
                _flag3.SetActive(true);
                break;
            case 4:
                _level4Image.sprite = _level4Sprite;
                _flag4.SetActive(true);
                break;
            case 5:
                _level5Image.sprite = _level5Sprite;
                _flag5.SetActive(true);
                break;
            case 6:
                _level6Image.sprite = _level6Sprite;
                _flag6.SetActive(true);
                break;
            case 7:
                _level7Image.sprite = _level7Sprite;
                _flag7.SetActive(true);
                break;
            case 8:
                _level8Image.sprite = _level8Sprite;
                _flag8.SetActive(true);
                break;
        }
    }

    // play tutorial
    public void PlayHelp()
    {
        _skipButton.SetActive(true);
        _luisAnimator.GetComponent<Button>().enabled = false;
        _luisAnimator.SetBool("Talking", true);
        _exampleItemsContainer.SetActive(true);
        _helpAnimator.gameObject.SetActive(true);
        _helpAnimator.SetBool("World", true);
    }

    // to disable example items
    public void CloseHelp()
    {
        _skipButton.SetActive(false);
        _luisAnimator.GetComponent<Button>().enabled = true;
        _helpAnimator.SetBool("World", true);
        _exampleItemsContainer.SetActive(false);
        _helpAnimator.gameObject.SetActive(false);
        _luisAnimator.SetBool("Talking",false);
    }

    // applicaton quit
    public void ExitApplication()
    {
        Application.Quit();
    }

    // data to loading screen
    public void SetNextScene(int aIndex)
    {
        CLevelsManager._instance.SetNextScene(aIndex);
    }

    // return to specific scene
    public void GoToScene(int aSceneIndex)
    {
        SceneManager.LoadScene(aSceneIndex);
    }
}
