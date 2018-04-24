using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CActivityManager2 : MonoBehaviour {

    #region SINGLETON PATTERN
    public static CActivityManager2 _instance = null; //static - the same variable is shared by all instances of the class that are created
    #endregion

    [SerializeField, Header("Configuration")]
    int _1SyllablesWords;
    [SerializeField]
    int _2SyllablesWords, _3SyllablesWords, _4SyllablesWords;

    // list of all the words available on this activity
    [SerializeField, Header("References")]
    List<CWordA2> _1SwordsList;
    [SerializeField]
    List<CWordA2> _2SwordsList, _3SwordsList, _4SwordsList;

    // answer of the actual question 
    [SerializeField]
    int _playerAnswer;

    // actual question word
    CWordA2 _actualWord;    

    // prefabs to load the help images 
    [SerializeField]
    CSyllableQuestion _1Syllable, _2Syllable, _3Syllable, _4Syllable;

    // animator of each prefab
    [SerializeField]
    Animator _1SAnimator, _2SAnimator, _3SAnimator, _4SAnimator;

    // Frog animator
    [SerializeField]
    Animator _frogAnimator;

    // to know if the player can play
    bool _readyToPlay;

    [SerializeField]
    List<DOTweenAnimation> _whiteButtons;

    private void Awake()
    {
        //Singleton check
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        PlayWord();
    }

    // Play with the next word
    public void PlayWord()
    {
        _readyToPlay = false;
        bool tReady = false;
        while (!tReady) // while the next word is not selected
        {
            CWordA2 tSelectedWord = _1SwordsList[Random.Range(0, _1SwordsList.Count)];
            if (!tSelectedWord._wasUsed) //  is the word was not used
            {
                _actualWord = tSelectedWord;
                tReady = true; // ready to play
            }
        }

        // clean previous game
        _playerAnswer = 0; // reset the answer
        _1Syllable.gameObject.SetActive(false);
        _2Syllable.gameObject.SetActive(false);
        _3Syllable.gameObject.SetActive(false);
        _4Syllable.gameObject.SetActive(false);
        // reset frog animations
        _frogAnimator.SetTrigger("Restart");
        _frogAnimator.SetBool("Fall", false);
        // reset player buttons
        for (int i = 0; i < _whiteButtons.Count; i++)
        {
            _whiteButtons[i].DORewind();
        }

        // load the image sprites
        switch (_actualWord._numberOfSyllables)
        {
            case 1:
                _1Syllable.gameObject.SetActive(true);
                _1Syllable.LoadImageParts(_actualWord);
                _1SAnimator.SetTrigger("Restart");
                break;
            case 2:
                _2Syllable.gameObject.SetActive(true);
                _2Syllable.LoadImageParts(_actualWord);
                _1SAnimator.SetTrigger("Restart");
                break;
            case 3:
                _3Syllable.gameObject.SetActive(true);
                _3Syllable.LoadImageParts(_actualWord);
                _1SAnimator.SetTrigger("Restart");
                break;
            case 4:
                _4Syllable.gameObject.SetActive(true);
                _4Syllable.LoadImageParts(_actualWord);
                _1SAnimator.SetTrigger("Restart");
                break;
        }
    }

    // to add 1 to syllables answer
    public void AddSyllable(int aButtonNumber)
    {
        if (_readyToPlay)
        {
            _whiteButtons[aButtonNumber].DOPlay();
            _playerAnswer++;
        }        
    }

    // to remove 1 to syllables answer
    public void RemoveSyllable(int aButtonNumber)
    {
        if (_readyToPlay)
        {
            _whiteButtons[aButtonNumber].DORewind();
            _playerAnswer--;
        }        
    }

    // check result of the actual question
    public void CheckResult()
    {
        if (_readyToPlay)
        {            
            if (_playerAnswer > 0) // if the player make a move
            {
                _readyToPlay = false;
                _frogAnimator.SetTrigger("StartJump");
                _frogAnimator.SetInteger("Result", _playerAnswer);
                if (_playerAnswer == _actualWord._numberOfSyllables)
                {
                    Debug.Log("Respuesta correcta.");
                }
                else
                {
                    _frogAnimator.SetTrigger("Fall");
                    Debug.Log("Respuesta incorrecta, intentalo de nuevo.");
                }
            }
        }             
    }

    // to control when the player can play
    public void ChangeReady(bool aOption)
    {
        _readyToPlay = aOption;
    }

    //start syllables success animation
    public void StartSyllablesAnimation()
    {
        switch (_actualWord._numberOfSyllables)
        {
            case 1:
                _1SAnimator.SetInteger("Syllables", 1);
                _1SAnimator.SetTrigger("StartSuccess");
                break;
            case 2:
                _2SAnimator.SetTrigger("Restart");
                _2SAnimator.SetTrigger("StartSuccess");
                break;
            case 3:
                _3SAnimator.SetTrigger("Restart");
                _3SAnimator.SetTrigger("StartSuccess");
                break;
            case 4:
                _4SAnimator.SetTrigger("Restart");
                _4SAnimator.SetTrigger("StartSuccess");
                break;
        }
    }

    // start fail syllables animation
    public void StartFailAnimation()
    {
        switch (_actualWord._numberOfSyllables)
        {
            case 1:
                _1SAnimator.SetTrigger("StartFail");
                break;
            case 2:
                _2SAnimator.SetTrigger("StartFail");
                break;
            case 3:
                _3SAnimator.SetTrigger("StartFail");
                break;
            case 4:
                _4SAnimator.SetTrigger("StartFail");
                break;
        }
    }
}
