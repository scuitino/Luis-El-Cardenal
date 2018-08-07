using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActivityManager8 : MonoBehaviour {

    #region SINGLETON PATTERN
    public static CActivityManager8 _instance = null;
    #endregion

    //to play sentences
    AudioSource _aSource;

    // replay sentence button
    [SerializeField]
    GameObject _replayButton;

    // tutorial time
    bool _isTutorial;

    // player score
    int _score;

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

}
