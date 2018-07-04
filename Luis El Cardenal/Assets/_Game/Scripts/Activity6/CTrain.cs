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

    // set Train ID
    public void SetTrainID(string aTrainLetter)
    {        
        switch (aTrainLetter)
        {
            case "A":
                _trainID = 0;
                _flagText.text = "A";
                break;
            case "L":
                _trainID = 1;
                _flagText.text = "L";
                break;
            case "M":
                _trainID = 2;
                _flagText.text = "M";
                break;
            case "S":
                _trainID = 3;
                _flagText.text = "S";
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
