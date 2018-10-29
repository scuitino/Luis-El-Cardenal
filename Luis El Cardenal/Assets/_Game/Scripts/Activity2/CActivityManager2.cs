using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CActivityManager2 : CActivity {

    #region SINGLETON PATTERN
    public static CActivityManager2 _instance = null; //static - the same variable is shared by all instances of the class that are created
    #endregion

    // syllables for all questions on this level
    [SerializeField, Header("Configuration")]
    List<int> _syllablesOnThisLevel;

    // actual question on the _syllablesOnThisLevel list
    int _actualQuestion;

    // list of all the words available on this activity
    [SerializeField, Header("References")]
    List<CWordA2> _1SWordsList;
    [SerializeField]
    List<CWordA2> _2SWordsList, _3SWordsList, _4SWordsList;

    // answer of the actual question 
    [SerializeField]
    int _playerAnswer;

    // score of the player
    public int _playerScore;    

    // actual question word
    CWordA2 _actualWord;    

    // prefabs to load the help images 
    [SerializeField]
    CSyllableQuestion _1Syllable, _2Syllable, _3Syllable, _4Syllable;

    // animator of each prefab
    [SerializeField]
    Animator _1SAnimator, _2SAnimator, _3SAnimator, _4SAnimator;

    // Frog animator
    public Animator _frogAnimator;    

    // to move result button
    [SerializeField]
    DOTweenAnimation _resultButton;

    // to repeat the word sound
    [SerializeField]
    GameObject _repeatSoundButton;

    // player buttons
    [SerializeField]
    List<DOTweenAnimation> _whiteButtons;

    // leafs syllabes texts 
    [SerializeField]
    List<Text> _leafTexts;

    // to control luis animations
    [SerializeField]
    Animator _luisAnimator;

    // to play button sound
    [SerializeField]
    AudioSource _buttonsSound;

    // tutorial time
    bool _isTutorial;

    // is playing replay tutorial?
    bool _replayIsOn;

    // the frog is jumping?
    public bool _isJumping;

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
        _isTutorial = true;
        _replayIsOn = false;

        // making all the words availables
        foreach(CWordA2 tWord in _1SWordsList)
        {
            tWord._wasUsed = false;            
        }
        foreach (CWordA2 tWord in _2SWordsList)
        {
            tWord._wasUsed = false;
        }
        foreach (CWordA2 tWord in _3SWordsList)
        {
            tWord._wasUsed = false;
        }
        foreach (CWordA2 tWord in _4SWordsList)
        {
            tWord._wasUsed = false;
        }

        // start the game
        //PlayWord();

        _helpAnimator.SetBool("Activity2", true);
    }

    private void Update()
    {
        if (_replayIsOn)
        {
            _readyToPlay = false;
        }
    }

    // Play with the next word
    public void PlayWord()
    {
        if (_isTutorial)
        {
            _startFlag.SetActive(true);
            _isTutorial = false;
        }            

        TurnOffSkipButton();                
        if (_syllablesOnThisLevel.Count > 0)
        {
            _readyToPlay = false;
            bool tReady = false;
            CWordA2 tSelectedWord;
            int tSyllables = 0; // syllables for the next word
            _actualQuestion = Random.Range(0, _syllablesOnThisLevel.Count);
            tSyllables = _syllablesOnThisLevel[_actualQuestion]; // select the sylabbles for the next word

            while (!tReady) // while the next word is not selected
            {
                switch (tSyllables) // try to choose a word
                {
                    case 1:
                        tSelectedWord = _1SWordsList[Random.Range(0, _1SWordsList.Count)];
                        break;
                    case 2:
                        tSelectedWord = _2SWordsList[Random.Range(0, _2SWordsList.Count)];
                        break;
                    case 3:
                        tSelectedWord = _3SWordsList[Random.Range(0, _3SWordsList.Count)];
                        break;
                    case 4:
                        tSelectedWord = _4SWordsList[Random.Range(0, _4SWordsList.Count)];
                        break;
                    default:
                        tSelectedWord = null;
                        break;
                }

                if (!tSelectedWord._wasUsed && _actualWord != tSelectedWord) //  is the word was not used and is not the same of the previous turn
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
                _leafTexts[i].GetComponent<DOTweenAnimation>().DORewind();
            }

            _luisAnimator.gameObject.GetComponent<Button>().enabled = true;

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
                    _2SAnimator.SetTrigger("Restart");
                    break;
                case 3:
                    _3Syllable.gameObject.SetActive(true);
                    _3Syllable.LoadImageParts(_actualWord);
                    _3SAnimator.SetTrigger("Restart");
                    break;
                case 4:
                    _4Syllable.gameObject.SetActive(true);
                    _4Syllable.LoadImageParts(_actualWord);
                    _4SAnimator.SetTrigger("Restart");
                    break;
            }

            // load leaf syllables
            for (int i = 0; i < _actualWord._numberOfSyllables; i++)
            {
                _leafTexts[i].text = _actualWord._syllables[i];
            }
        }
        else // if the player win
        {
            _win = true;
            WinGame(); // win game
        }
    }

    // to add 1 to syllables answer
    public void AddSyllable(int aButtonNumber)
    {
        if (_readyToPlay && !_win)
        {
            _buttonsSound.Play();
            _whiteButtons[aButtonNumber].DORestart();
            _playerAnswer++;
        }        
    }

    // to remove 1 to syllables answer
    public void RemoveSyllable(int aButtonNumber)
    {
        if (_readyToPlay && !_win)
        {
            _buttonsSound.Play();
            _whiteButtons[aButtonNumber].DOPlayBackwards();
            _playerAnswer--;
        }        
    }

    // check result of the actual question
    public void CheckResult()
    {
        if (_readyToPlay)
        {
            _buttonsSound.Play();
            _luisAnimator.gameObject.GetComponent<Button>().enabled = false;
            if (_playerAnswer > 0) // if the player make a move
            {
                ChangeRepeatButtonState(false);
                for (int i = 0; i < _whiteButtons.Count; i++)
                {
                    _whiteButtons[i].DORewind();
                }
                for (int i = 0; i < _playerAnswer; i++)
                {
                    _whiteButtons[i].DORestart();
                }
                _resultButton.DOPlayBackwards();
                _readyToPlay = false;
                _frogAnimator.SetTrigger("StartJump");
                _frogAnimator.SetInteger("Result", _playerAnswer);
                if (_playerAnswer != _actualWord._numberOfSyllables) // check if the answer is wrong
                {
                    _frogAnimator.SetTrigger("Fall");
                }
                else
                {
                    _isJumping = true;
                    _playerScore++;
                    _syllablesOnThisLevel.RemoveAt(_actualQuestion); // remove the actual question
                    _actualWord._wasUsed = true; // label the word as used
                }
            }            
        }             
    }

    // to control when the player can play
    public override void ChangeReady(bool aOption)
    {
        if (!_win)
        {
            _luisAnimator.gameObject.GetComponent<Button>().enabled = aOption;
            base.ChangeReady(aOption);
            _resultButton.DOPlayForward();
        }
    }    

    //start syllables success animation
    public void StartSyllablesAnimation()
    {
        _isJumping = false;
        switch (_actualWord._numberOfSyllables)
        {
            case 1:
                _1SAnimator.SetInteger("Syllables", 1);
                _1SAnimator.SetTrigger("StartSuccess");
                break;
            case 2:
                _2SAnimator.SetInteger("Syllables", 2);
                _2SAnimator.SetTrigger("StartSuccess");
                break;
            case 3:
                _3SAnimator.SetInteger("Syllables", 3);
                _3SAnimator.SetTrigger("StartSuccess");
                break;
            case 4:
                _4SAnimator.SetInteger("Syllables", 4);
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
                _1SAnimator.SetInteger("Syllables", 1);
                _1SAnimator.SetTrigger("StartSuccess");
                break;
            case 2:
                _2SAnimator.SetInteger("Syllables", 2);
                _2SAnimator.SetTrigger("StartSuccess");
                break;
            case 3:
                _3SAnimator.SetInteger("Syllables", 3);
                _3SAnimator.SetTrigger("StartSuccess");
                break;
            case 4:
                _4SAnimator.SetInteger("Syllables", 4);
                _4SAnimator.SetTrigger("StartSuccess");
                break;
        }
    }

    // Fade in leaf syllable (used by word animator)
    public void TurnOnLeafSyllable(int aIndex)
    {
        if (_playerAnswer == _actualWord._numberOfSyllables)
        {
            _leafTexts[aIndex].GetComponent<DOTweenAnimation>().DORestart();
        }        
    }

    // to repeat when de player touch the button
    public void RepeatSound()
    {
        if (_readyToPlay)
        {
            _buttonsSound.Play();
            switch (_actualWord._numberOfSyllables)
            {
                case 1:
                    _1Syllable.PlaySound(0);
                    break;
                case 2:
                    _2Syllable.PlaySound(0);
                    break;
                case 3:
                    _3Syllable.PlaySound(0);
                    break;
                case 4:
                    _4Syllable.PlaySound(0);
                    break;
            }
        }        
    }

    // to enable and disable Repeat Sound button
    public void ChangeRepeatButtonState(bool aState)
    {
        _repeatSoundButton.SetActive(aState);
    }

    // when the player press luis button
    public void ReplayTutorial()
    {
        _replayIsOn = true;
        _luisAnimator.SetBool("Talking", true);
        _replayTutorialASource.Play();
        PauseGameplay(false);
        _skipReplayButton.SetActive(true);
        if (_stopTalkingCo != null)
        {
            StopCoroutine(_stopTalkingCo);
        }
        _stopTalkingCo = StartCoroutine(StopTalking(_replayTutorialASource.clip.length));
    }

    // when the player skip the tutorial replay
    public void SkipReTutorial()
    {
        _replayIsOn = false;
        _luisAnimator.SetBool("Talking", false);
        _replayTutorialASource.Stop();
        PauseGameplay(true);
        _skipReplayButton.SetActive(false);
        StopCoroutine(_stopTalkingCo);
    }

    // to pause gameplay
    public void PauseGameplay(bool aOption)
    {
        _readyToPlay = aOption;
    }

    // stop talking when replay ends
    IEnumerator StopTalking(float aDelay)
    {
        yield return new WaitForSeconds(aDelay);
        SkipReTutorial();
    }
}
