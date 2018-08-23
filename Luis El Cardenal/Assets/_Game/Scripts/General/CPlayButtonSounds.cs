using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayButtonSounds : MonoBehaviour {

    [SerializeField]
    AudioSource _aSource;

    [SerializeField]
    int _activityNumber;

    private void Start()
    {
        _aSource = this.GetComponent<AudioSource>();
    }

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
            case 4:
                if (CActivityManager4._instance._readyToPlay)
                {
                    _aSource.Play();
                }
                break;
            case 5:
                if (CActivityManager5._instance._readyToPlay)
                {
                    _aSource.Play();
                }
                break;
            case 6:
                if (CActivityManager6._instance._readyToPlay)
                {
                    if (!CActivityManager6._instance._wordIsPlaying)
                    {
                        _aSource.Play();
                        CActivityManager6._instance.DelayWordSound(_aSource.clip.length);
                    }                    
                }
                break;
        }
    }
}
