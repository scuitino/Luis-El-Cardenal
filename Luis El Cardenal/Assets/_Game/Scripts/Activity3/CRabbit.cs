using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRabbit : MonoBehaviour {

    // to play on animations
    [SerializeField]
    List<AudioClip> _rabbitSounds;

    // to play animation sounds
    AudioSource _aSource;

    private void Start()
    {
        _aSource = this.GetComponent<AudioSource>();
    }

    // to restart when the animation finish
    public void RestartChallenge()
    {
        CActivityManager3._instance.RestartChallenge();
    }

    // To animate berries
    public void CutBerry()
    {
        CActivityManager3._instance.CutBerry(false);
    }

    public void CutBerryTutorial()
    {
        CActivityManager3._instance.CutBerry(true);
    }

    // to play on animations
    public void PlaySound(int aSound)
    {
        _aSource.clip = _rabbitSounds[aSound];
        _aSource.Play();
    }
}
