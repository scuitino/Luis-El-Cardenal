using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        _aSource = this.GetComponent<AudioSource>();
        NextChallenge();
    }

    // play next challenge
    public void NextChallenge()
    {
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
                if(_2WordsAvailable.Count > 0)
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

        _aSource.Play();
        _readyToPlay = true;
        _resultButton.DOPlayForward();
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
                _resultButton.DOPlayBackwards();
                if (_playerAnswer == _correctAnswer) // win
                {
                    for (int i = 0; i < _eggs.Count; i++)
                    {
                        if(_eggs[i]._isPressed)
                        {
                            _chickenAnimator.SetTrigger("Success");
                            _eggs[i]._eggAnimator.SetTrigger("Success");
                        }
                    }
                }
                else // lose
                {
                    for (int i = 0; i < _eggs.Count; i++)
                    {
                        if (_eggs[i]._isPressed)
                        {
                            _chickenAnimator.SetTrigger("Fail");
                            _eggs[i]._eggAnimator.SetTrigger("Fail");
                        }
                    }
                }
            }
        }
    }
}
