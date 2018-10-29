using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CActivityManager7 : CActivity
{

    #region SINGLETON PATTERN
    public static CActivityManager7 _instance = null;
    #endregion

    // answer of the actual challenge
    [SerializeField]
    int _playerAnswer;

    // correct answer of the actual challenge
    [SerializeField]
    int _correctAnswer;

    // answer index
    int _answerIndex;

    // how many of each sentences i need
    [SerializeField]
    List<int> _firstChallenges;

    // all challenges posibilities
    [SerializeField]
    List<int> _allChallengesTypes;

    // sentences
    [SerializeField]
    List<AudioClip> _2WordsAvailable, _3WordsAvailable, _4WordsAvailable, _5WordsAvailable, _2WordsFail, _3WordsFail, _4WordsFail, _5WordsFail;

    //to play sentences
    AudioSource _aSource;

    // eggs animator references
    [SerializeField]
    List<CEgg> _eggs;

    // to move result button
    [SerializeField]
    DOTweenAnimation _resultButton;

    // to animate chicken
    [SerializeField]
    Animator _chickenAnimator;

    // replay sentence button
    [SerializeField]
    GameObject _replayButton;

    // replay sentence button
    [SerializeField]
    Animator _replaySentenceAnimator;

    // tutorial time
    bool _isTutorial;

    // player score
    public int _score;

    // to control luis animations
    [SerializeField]
    Animator _luisAnimator;

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
        _2WordsFail = new List<AudioClip>();
        _3WordsFail = new List<AudioClip>();
        _4WordsFail = new List<AudioClip>();
        _5WordsFail = new List<AudioClip>();
        _aSource = this.GetComponent<AudioSource>();
        _helpAnimator.SetBool("Activity7", true);
        _isTutorial = true;
        //NextChallenge();
    }

    // Play Sentence
    public void PlaySentence()
    {
        _aSource.Play();
        _replaySentenceAnimator.SetBool("Playing", true);
        Invoke("StopSpeaker", _aSource.clip.length);
    }

    // to stop speaker anim
    public void StopSpeaker()
    {
        _replaySentenceAnimator.SetBool("Playing",false);
    }

    public void StartWithDelay()
    {
        TurnOffSkipButton();
        Invoke("NextChallenge", 2);
        _startFlag.SetActive(true);
    }

    // play next challenge
    public void NextChallenge()
    {
        if (_isTutorial)
        {
            _isTutorial = false;
            StartWithDelay();
            return;
        }

        if (!_win)
        {
            // reseting game
            _playerAnswer = 0;
            for (int i = 0; i < _eggs.Count; i++)
            {
                _eggs[i]._isPressed = false;
                _eggs[i]._eggAnimator.SetTrigger("Return");
            }
            _chickenAnimator.SetTrigger("Return");

            // first 5 challenges or not?
            if (_firstChallenges.Count > 0)
            {
                int tSelectedChallenge = Random.Range(0, _firstChallenges.Count);
                _correctAnswer = _firstChallenges[tSelectedChallenge];
                _firstChallenges.RemoveAt(tSelectedChallenge);
            }
            else
            {
                _correctAnswer = _allChallengesTypes[Random.Range(0, _allChallengesTypes.Count)];
            }

            // taking challenge
            switch (_correctAnswer)
            {
                case 2:
                    if (_2WordsAvailable.Count > 0)
                    {
                        _answerIndex = Random.Range(0, _2WordsAvailable.Count);
                        _aSource.clip = _2WordsAvailable[_answerIndex];
                    }
                    break;
                case 3:
                    if (_3WordsAvailable.Count > 0)
                    {
                        _answerIndex = Random.Range(0, _3WordsAvailable.Count);
                        _aSource.clip = _3WordsAvailable[_answerIndex];
                    }
                    break;
                case 4:
                    if (_4WordsAvailable.Count > 0)
                    {
                        _answerIndex = Random.Range(0, _4WordsAvailable.Count);
                        _aSource.clip = _4WordsAvailable[_answerIndex];
                    }
                    break;
                case 5:
                    if (_5WordsAvailable.Count > 0)
                    {
                        _answerIndex = Random.Range(0, _5WordsAvailable.Count);
                        _aSource.clip = _5WordsAvailable[_answerIndex];
                    }
                    break;
            }

            _replayButton.GetComponent<Button>().interactable = true;
            PlaySentence();
            _readyToPlay = true;
            _luisAnimator.gameObject.GetComponent<Button>().enabled = true;
            _resultButton.DOPlayForward();
        }
        else
        {
            WinGame();
        }
    }

    // to add 1 to word count
    public void AddWordCount()
    {
        if (_readyToPlay && !_win)
        {
            _playerAnswer++;
        }
    }

    // to remove 1 to word count
    public void RemoveWordCount()
    {
        if (_readyToPlay && !_win)
        {
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
                _luisAnimator.gameObject.GetComponent<Button>().enabled = false;
                _resultButton.DOPlayBackwards();
                if (_playerAnswer == _correctAnswer) // success
                {
                    _score++;
                    CWormsManager._instance.Collect();
                    if (_score == 5) // check if the player won
                    {
                        _win = true;
                    }

                    // animations
                    _luisAnimator.SetTrigger("Success");                    
                    for (int i = 0; i < _eggs.Count; i++)
                    {
                        if(_eggs[i]._isPressed)
                        {                            
                            _chickenAnimator.SetTrigger("Success");
                            _eggs[i]._eggAnimator.SetTrigger("Success");                            
                        }
                    }
                    // remove used sentence
                    switch (_correctAnswer)
                    {
                        case 2:
                            _2WordsAvailable.RemoveAt(_answerIndex);
                            break;
                        case 3:
                            _3WordsAvailable.RemoveAt(_answerIndex);
                            break;
                        case 4:
                            _4WordsAvailable.RemoveAt(_answerIndex);
                            break;
                        case 5:
                            _5WordsAvailable.RemoveAt(_answerIndex);
                            break;
                    }
                }
                else // lose
                {
                    // animations
                    _luisAnimator.SetTrigger("Fail");
                    for (int i = 0; i < _eggs.Count; i++)
                    {
                        if (_eggs[i]._isPressed)
                        {
                            
                            _chickenAnimator.SetTrigger("Fail");
                            _eggs[i]._eggAnimator.SetTrigger("Fail");                            
                        }
                    }
                    // regrouping sentences
                    switch (_correctAnswer)
                    {
                        case 2:
                            if(_2WordsAvailable.Count == 1) // if is the last word of this group, reload the list
                            {
                                for (int i = 0; i < _2WordsFail.Count; i++)
                                {
                                    _2WordsAvailable.Add(_2WordsFail[i]);
                                }
                                _2WordsFail.Clear();
                                _2WordsFail.Add(_2WordsAvailable[0]);
                                _2WordsAvailable.RemoveAt(_answerIndex);
                            }
                            else
                            {
                                _2WordsFail.Add(_2WordsAvailable[_answerIndex]);
                                _2WordsAvailable.RemoveAt(_answerIndex);
                            }                            
                            break;
                        case 3:
                            if (_3WordsAvailable.Count == 1)
                            {
                                for (int i = 0; i < _3WordsFail.Count; i++)
                                {
                                    _3WordsAvailable.Add(_3WordsFail[i]);
                                }
                                _3WordsFail.Clear();
                                _3WordsFail.Add(_3WordsAvailable[0]);
                                _3WordsAvailable.RemoveAt(_answerIndex);
                            }
                            else
                            {
                                _3WordsFail.Add(_3WordsAvailable[_answerIndex]);
                                _3WordsAvailable.RemoveAt(_answerIndex);
                            }
                            break;
                        case 4:
                            if (_4WordsAvailable.Count == 1)
                            {
                                for (int i = 0; i < _4WordsFail.Count; i++)
                                {
                                    _4WordsAvailable.Add(_4WordsFail[i]);
                                }
                                _4WordsFail.Clear();
                                _4WordsFail.Add(_4WordsAvailable[0]);
                                _4WordsAvailable.RemoveAt(_answerIndex);
                            }
                            else
                            {
                                _4WordsFail.Add(_4WordsAvailable[_answerIndex]);
                                _4WordsAvailable.RemoveAt(_answerIndex);
                            }
                            break;
                        case 5:
                            if (_5WordsAvailable.Count == 1)
                            {
                                for (int i = 0; i < _5WordsFail.Count; i++)
                                {
                                    _5WordsAvailable.Add(_5WordsFail[i]);
                                }
                                _5WordsFail.Clear();
                                _5WordsFail.Add(_5WordsAvailable[0]);
                                _5WordsAvailable.RemoveAt(_answerIndex);
                            }
                            else
                            {
                                _5WordsFail.Add(_5WordsAvailable[_answerIndex]);
                                _5WordsAvailable.RemoveAt(_answerIndex);
                            }
                            break;
                    }
                }
            }
        }
    }

    // when the player press luis button
    public void ReplayTutorial()
    {
        _replaySentenceAnimator.SetBool("Playing",false);
        _replayButton.GetComponent<Button>().interactable = false;
        _aSource.Stop();
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
        _replayButton.GetComponent<Button>().interactable = true;
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
