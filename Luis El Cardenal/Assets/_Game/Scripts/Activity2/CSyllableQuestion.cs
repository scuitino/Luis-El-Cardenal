using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSyllableQuestion : MonoBehaviour {

    // number of syllables of this prefab
    [SerializeField]
    int _numberOfSyllables;

    // reference to load the sprites
    [SerializeField]
    List<Image> _imagesToFill;

    // sounds for each vocal
    [SerializeField]
    List<AudioClip> _sounds;

    // to play vocal Sounds
    AudioSource _audioSource;

    private void Start()
    {
        _audioSource = this.GetComponent<AudioSource>();
    }

    // Load the images and sounds of each vocal
    public void LoadImageParts(CWordA2 aWord)
    {
        // load sprites
        for (int i = 0; i < _numberOfSyllables; i++)
        {
            _imagesToFill[i].sprite = aWord._images[i];            
        }

        // load sounds
        for (int i = 0; i < _numberOfSyllables + 1; i++)
        {
            _sounds[i] = aWord._sounds[i];
        }
    }

    // to play vocal sounds
    public void PlaySound(int aIndex)
    {
        _audioSource.clip = _sounds[aIndex];
        _audioSource.Play();
    }

    // go to the next word
    public void NextQuestion()
    {
        CActivityManager2._instance.PlayWord();
    }

    // to change when the player can play
    public void EnableReady()
    {
        CActivityManager2._instance.ChangeReady(true);
    }

    // to fade in leaf texts
    public void TurnOnLeafSyllable(int aIndex)
    {
        CActivityManager2._instance.TurnOnLeafSyllable(aIndex);
    }
}
