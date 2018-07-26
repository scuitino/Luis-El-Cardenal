using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CCave : MonoBehaviour {

    // cave group ID
    [SerializeField]
    int _caveID;

    // know if is the left or right cave
    [SerializeField]
    int _caveNumber;

    // to show the text on the cave
    [SerializeField]
    Text _flagText;

    // letter sounds
    [SerializeField]
    List<AudioClip> _soundLetters;

    // to play letters sound
    [SerializeField]
    AudioSource _letterAudioSource;

    // set Cave ID
    public void SetCaveID(int aCaveID)
    {
        _caveID = aCaveID;
        switch (aCaveID)
        {
            case 0:
                _flagText.text = "A";
                _letterAudioSource.clip = _soundLetters[0];
                break;
            case 1:
                _flagText.text = "O";
                _letterAudioSource.clip = _soundLetters[1];
                break;
            case 2:
                _flagText.text = "N";
                _letterAudioSource.clip = _soundLetters[2];
                break;
            case 3:
                _flagText.text = "R";
                _letterAudioSource.clip = _soundLetters[3];
                break;
            default:
                break;
        }
    }

    // return cave ID
    public int GetCaveID()
    {
        return _caveID;
    }

    // return cave Number
    public int GetCaveNumber()
    {
        return _caveNumber;
    }
}
