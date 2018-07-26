using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTrainAnimation : MonoBehaviour {

    [SerializeField]
    List<AudioClip> _sounds;

	public void PlaySuccessSound(int aSound)
    {
        this.GetComponent<AudioSource>().clip = _sounds[aSound];
        this.GetComponent<AudioSource>().Play();
    }
}
