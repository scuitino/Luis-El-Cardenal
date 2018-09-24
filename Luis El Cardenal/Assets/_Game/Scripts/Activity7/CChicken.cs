using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CChicken : MonoBehaviour {

    [SerializeField]
    AudioSource _aSource;

    public void PlaySound()
    {
        _aSource.Play();
    }

	public void CallNextChallenge()
    {
        CActivityManager7._instance.NextChallenge();
    }
}
