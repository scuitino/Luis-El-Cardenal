using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWormsManager : MonoBehaviour {

    #region SINGLETON PATTERN
    public static CWormsManager _instance = null;
    #endregion

    [SerializeField]
    int _wormsCollected; // fix for worms not collect bug

    [SerializeField]
    int _activityNumber;

    private void Start()
    {
        //Replace Singleton
        _instance = this;
        _wormsCollected = 0;
    }

    private void LateUpdate()
    {
        if (_activityNumber == 1)
        {
            if (_wormsCollected < CActivityManager1._instance._winsCount)
            {
                Collect();
            }
        }
        else if (_activityNumber == 2)
        {
            if (_wormsCollected < CActivityManager2._instance._playerScore)
            {
                if (!CActivityManager2._instance._isJumping)
                {
                    Collect();
                }                
            }
        }
        else if (_activityNumber == 3)
        {
            if (_wormsCollected < CActivityManager3._instance._successCount)
            {
                Collect();
            }
        }
        else if (_activityNumber == 4)
        {
            if (_wormsCollected < CActivityManager4._instance._successCount)
            {
                Collect();
            }
        }
        else if (_activityNumber == 5)
        {
            if (_wormsCollected < CActivityManager5._instance._playerScore)
            {
                if (CActivityManager5._instance._playerScore > 1)
                {
                    Collect();
                }
                else
                {
                    _wormsCollected++;
                }
            }
        }
        else if (_activityNumber == 6)
        {
            if (_wormsCollected < CActivityManager6._instance._playerScore)
            {
                Collect();
            }
        }
        else if (_activityNumber == 7)
        {
            if (_wormsCollected < CActivityManager7._instance._score)
            {
                Collect();
            }
        }
        else if (_activityNumber == 8)
        {
            if (_wormsCollected < CActivityManager8._instance._score)
            {
                Collect();
            }
        }
    }

    // collect a worm
    public void Collect()
    {
        _wormsCollected++;
        this.GetComponent<Animator>().SetTrigger("Collect");
    }
}
