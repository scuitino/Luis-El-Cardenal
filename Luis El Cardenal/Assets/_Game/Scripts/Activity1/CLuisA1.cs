using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLuisA1 : MonoBehaviour {

    // to play fail sound
    AudioSource _luisASource;

    private void Start()
    {
        _luisASource = this.GetComponent<AudioSource>();
    }

    // to go to the next word
    public void PlayNextWord()
    {
        CActivityManager1._instance.PlayWord();
    }

    // to play fail sound 
    public void PlayFailSound()
    {
        _luisASource.Play();
    }
}
