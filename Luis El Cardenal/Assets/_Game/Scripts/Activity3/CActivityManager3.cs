using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActivityManager3 : CActivity {

    #region SINGLETON PATTERN
    public static CActivityManager3 _instance = null; //static - the same variable is shared by all instances of the class that are created
    #endregion

    // syllables for all questions on this level
    [SerializeField, Header("Configuration")]
    List<GameObject> _challengesOnThisLevel;

    // actual question on the _syllablesOnThisLevel list
    int _actualChallenge;

    // result slots
    [SerializeField, Header("References")]
    DragAndDropCell _flower1;
    DragAndDropCell _flower2;

    // to move result button
    [SerializeField]
    DOTweenAnimation _resultButton;

    // to animate the rabbit
    [SerializeField]
    Animator _rabbitAnimator;

    private void Awake()
    {
        //Singleton check
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        ChangeReady(true);
    }

    // to control when the player can play
    public override void ChangeReady(bool aOption)
    {
        if (!_win)
        {
            base.ChangeReady(aOption);
            _resultButton.DOPlayForward();
        }
    }

    // Play with the next word
    public void PlayWord()
    {
        
    }

    // check result of the actual question
    public void CheckResult()
    {
        if (_readyToPlay)
        {
            if (_flower1.GetItem() != null && _flower2.GetItem() != null) // if both result slots are filled
            {
                _resultButton.DOPlayBackwards();
                if (_flower1.GetItem().GetItemID() == _flower2.GetItem().GetItemID())
                {
                    _rabbitAnimator.SetTrigger("Success");
                    Debug.Log("son iguales");
                }
                else
                {
                    _rabbitAnimator.SetTrigger("Fail");
                    Debug.Log("son diferentes");
                }
            }
            else
            {
                Debug.Log("debe poner dos objetos");
            }
        }        
    }
}
