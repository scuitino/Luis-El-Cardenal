﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHelp2 : MonoBehaviour {

    // all the clips for the help animation
    [SerializeField]
    List<AudioClip> _helpSounds;

    // to use with animation sounds
    AudioSource _helpASource;

    // to animate luis
    [SerializeField]
    Animator _luisAnimator;

    // to animate the frog
    [SerializeField]
    Animator _frogAnimator;

    // to enable real Frog and toad
    [SerializeField]
    GameObject _realFrog, _realToad;

    private void Start()
    {
        _helpASource = this.GetComponent<AudioSource>();
    }

    // play the selected sound
    public void PlayHelpSound(int aSound)
    {
        _helpASource.clip = _helpSounds[aSound];
        _helpASource.Play();
    }	

    // start talking animation
    public void StartTalking()
    {
        _luisAnimator.SetBool("Talking", true);
    }

    // stop talking animation
    public void StopTalking()
    {
        _luisAnimator.SetBool("Talking", false);
    }

    // start success animation
    public void StartSuccess()
    {
        _luisAnimator.SetTrigger("Success");
    }

    //  start Frog  animation
    public void StartFrogAnimation()
    {
        _frogAnimator.SetBool("Example", true);
        _frogAnimator.SetInteger("Result", 2);
        _frogAnimator.SetTrigger("StartJump");
    }

    // to start the game
    public void StartTheGame()
    {
        CActivityManager2._instance.PlayWord();
    }

    // to close help panel
    public void FinishHelp()
    {
        CActivityManager2._instance.PlayWord();
        _realFrog.SetActive(true);
        _realToad.SetActive(true);
        this.gameObject.SetActive(false);
    }
}