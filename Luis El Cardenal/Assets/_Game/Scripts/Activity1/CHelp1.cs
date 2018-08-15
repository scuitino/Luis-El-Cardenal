using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHelp1 : MonoBehaviour {

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

    // to animate the Rabbit in activity 3
    [SerializeField]
    Animator _rabbitanimator;

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

    // to animate the rabbit in activity 3
    public void AnimateRabbit()
    {
        _rabbitanimator.SetTrigger("Tutorial");
    }

    // to close the help panel
    public void CloseHelp()
    {
        this.gameObject.SetActive(false);
    }

    // only for world
    [SerializeField]
    List<Animator> _examplesAnimators;

    // to animate examples
    public void AnimateExample(int aNumber)
    {
        _examplesAnimators[aNumber].SetTrigger("Play");
    }

    // turn off the example items
    public void CloseWorldHelp()
    {
        CMainMenu._instance.CloseHelp();
    }

    // to start activity 4 when the help animation is finished
    public void StartActivity4()
    {
        CActivityManager4._instance.EnableRealFlowers();
        CActivityManager4._instance.StartGame();
    }

    // to start activity 5 mulita animation
    public void StartMulitaAnimation()
    {
        CActivityManager5._instance.StartExampleAnimation();
    }

    // to start activity 6 when the help animation is finished
    public void StartActivity6()
    {        
        CActivityManager6._instance.EnableChallenges();
        CloseHelp();
    }
}
