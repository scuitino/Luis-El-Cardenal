using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CTrain : MonoBehaviour {

    // train group ID
    [SerializeField]
    int _trainID;

    // to know which box is
    [SerializeField]
    int _trainNumber;

    // to show the text on the cave
    [SerializeField]
    Text _flagText;

    // letter sounds
    [SerializeField]
    List<AudioClip> _soundLetters;

    // to play letters sound
    [SerializeField]
    AudioSource _letterAudioSource;

    // set Train ID
    public void SetTrainID(string aTrainLetter)
    {        
        switch (aTrainLetter)
        {
            case "A":
                _trainID = 0;
                _flagText.text = "A";
                _letterAudioSource.clip = _soundLetters[0];
                break;
            case "L":
                _trainID = 1;
                _flagText.text = "L";
                _letterAudioSource.clip = _soundLetters[1];
                break;
            case "M":
                _trainID = 2;
                _flagText.text = "M";
                _letterAudioSource.clip = _soundLetters[2];
                break;
            case "S":
                _trainID = 3;
                _flagText.text = "S";
                _letterAudioSource.clip = _soundLetters[3];
                break;
            default:
                break;
        }
    }

    // return train ID
    public int GetTrainID()
    {
        return _trainID;
    }

    // return train Number
    public int GetTrainNumber()
    {
        return _trainNumber;
    }
}
