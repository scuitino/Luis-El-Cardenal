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

    // to change leaf sprite
    public void EatLeaf()
    {
        CActivityManager3._instance.CutLeaf();
    }

    // to play on animations
    public void PlaySound(int aSound)
    {
        _aSource.clip = _rabbitSounds[aSound];
        _aSource.Play();
    }
}
