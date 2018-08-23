using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayAnimationSounds : MonoBehaviour {

    [SerializeField]
    List<AudioClip> _sounds;

    [SerializeField]
    AudioSource _aSource;

    public void PlaySound(int aIndex)
    {
        _aSource.clip = _sounds[aIndex];
        _aSource.Play();
    }
}
