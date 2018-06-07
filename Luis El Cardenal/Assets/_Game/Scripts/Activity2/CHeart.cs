using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHeart : MonoBehaviour {

	// To Play Heart Sound
    public void PlaySound()
    {
        this.GetComponent<AudioSource>().Play();
    }
}
