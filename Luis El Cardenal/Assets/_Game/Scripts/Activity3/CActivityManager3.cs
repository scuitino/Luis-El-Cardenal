using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActivityManager3 : CActivity {

    // result slots
    [SerializeField]
    DragAndDropCell _flower1, _flower2;

    // to move result button
    [SerializeField]
    DOTweenAnimation _resultButton;

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
                    Debug.Log("son iguales");
                }
                else
                {
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
