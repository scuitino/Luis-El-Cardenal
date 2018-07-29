using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayItemSound : MonoBehaviour {

    AudioSource _aSource;

    private void Start()
    {
        _aSource = this.GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        _aSource.Play();
    }
}
