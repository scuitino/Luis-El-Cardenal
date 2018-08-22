using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayButtonSounds : MonoBehaviour {

    [SerializeField]
    AudioSource _aSource;

    [SerializeField]
    int _activityNumber;

    public void PlaySound()
    {
        switch (_activityNumber)
        {
            case 3:
                if (CActivityManager3._instance._readyToPlay)
                {
                    _aSource.Play();
                }
                break;
        }
    }
}
