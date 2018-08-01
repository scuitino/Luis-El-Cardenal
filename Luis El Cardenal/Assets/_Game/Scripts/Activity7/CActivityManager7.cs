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

    // how many of each sentences i need
    [SerializeField]
    List<int> _numberPerType;

    // sentences
    [SerializeField]
    List<AudioClip> _2WordsAvailable, _3WordsAvailable, _4WordsAvailable, _5WordsAvailable, _2WordsFail, _3WordsFail, _4WordsFail, _5WordsFail;

    // eggs animator references
    [SerializeField]
    List<CEgg> _eggs;

    // play next challenge
    public void NextChallenge()
    {
        _correctAnswer = Random.Range(0,_numberPerType.Count);
    }

    // to add 1 to word count
    public void AddWordCount()
    {
        if (_readyToPlay && !_win)
        {
            //_whiteButtons[aButtonNumber].DORestart();
            _playerAnswer++;
        }
    }

    // to remove 1 to word count
    public void RemoveWordCount()
    {
        if (_readyToPlay && !_win)
        {
            //_whiteButtons[aButtonNumber].DOPlayBackwards();
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
                if (_playerAnswer == _correctAnswer) // win
                {
                    for (int i = 0; i < _eggs.Count; i++)
                    {
                        if(_eggs[i]._isPressed)
                        {
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
                            _eggs[i]._eggAnimator.SetTrigger("Fail");
                        }
                    }
                }
            }
        }
    }
}
