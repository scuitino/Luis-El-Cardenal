using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActivityManager7 : CActivity {

    #region SINGLETON PATTERN
    public static CActivityManager7 _instance = null;
    #endregion

    // answer of the actual question 
    [SerializeField]
    int _playerAnswer;

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
}
