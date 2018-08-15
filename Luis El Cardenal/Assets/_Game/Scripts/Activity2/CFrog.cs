using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFrog : MonoBehaviour {

    // heart animator
    [SerializeField]
    Animator _heartsAnimator;

    // to play frog sounds
    AudioSource _frogASource;

    // list of frog sounds
    [SerializeField]
    List<AudioClip> _frogSounds;

    //To animate Luis
    [SerializeField]
    Animator _luisAnimator;    

    private void Start()
    {
        _frogASource = this.GetComponent<AudioSource>();
    }

    // to animate hearts
    public void PlayHearts()
    {
        _heartsAnimator.SetTrigger("Play");
    }

    // to play frog sounds
    public void PlaySound(int aSound)
    {
        _frogASource.clip = _frogSounds[aSound];
        _frogASource.Play();
    }

    // notify the manager that the jump animation is finished
    public void LastJumpCallBack()
    {
        CWormsManager._instance.Collect();
        CActivityManager2._instance.StartSyllablesAnimation();
    }

    // notify the manager that the fail animation is finished
    public void FailJumpCallBack()
    {
        CActivityManager2._instance.StartFailAnimation();
    }

    // good luis animation
    public void PlaySuccess()
    {
        _luisAnimator.SetTrigger("Success");
    }

    // bad luis animation
    public void PlayFail()
    {
        _luisAnimator.SetTrigger("Fail");
    }
}
