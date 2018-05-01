using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CItemA1 : MonoBehaviour {

    // to identify each item
    [SerializeField]
    int _itemID;

    // to enable button
    [SerializeField]
    GameObject _playButton;

    // to play word sound
    AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = this.GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _playButton.SetActive(true);
    }

    private void OnDisable()
    {
        _playButton.SetActive(false);
    }

    // get id of the item
    public int GetID()
    {
        return _itemID;
    }

    // tell the manager: play next word
    public void PlayNextWord()
    {
        CActivityManager1._instance.PlayWord();
    }

    // play the sound of this word
    public void PlaySound()
    {
        _audioSource.Play();
    }
}
