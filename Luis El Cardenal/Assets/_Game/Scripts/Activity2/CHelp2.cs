using System.Collections;
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

    // to animate the touched object
    [SerializeField]
    Animator _touchedAnimator;

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

    //  start touched object animation
    public void StartTouchAnimation()
    {
        _touchedAnimator.SetTrigger("Play");
    }

    // to start the game
    public void StartTheGame()
    {
        CActivityManager2._instance.PlayWord();
    }
}
