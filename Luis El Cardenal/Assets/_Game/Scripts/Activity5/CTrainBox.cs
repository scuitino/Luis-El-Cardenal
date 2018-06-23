using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CTrainBox : MonoBehaviour
{

    // box group ID
    [SerializeField]
    int _boxID;

    // know if is the left, middle or right box
    [SerializeField]
    int _boxNumber;

    // to show the text on the box
    [SerializeField]
    Text _flagText;

    // set Cave ID
    public void SetBoxID(int aBoxID)
    {
        _boxID = aBoxID;
        switch (aBoxID)
        {
            case 0:
                _flagText.text = "A";
                break;
            case 1:
                _flagText.text = "L";
                break;
            case 2:
                _flagText.text = "M";
                break;
            case 3:
                _flagText.text = "S";
                break;
            default:
                break;
        }
    }

    // return box ID
    public int GetBoxID()
    {
        return _boxID;
    }

    // return box Number
    public int GetBoxNumber()
    {
        return _boxNumber;
    }
}
