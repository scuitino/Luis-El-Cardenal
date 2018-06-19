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

    // set Cave ID
    public void SetCaveID(int aCaveID)
    {
        _caveID = aCaveID;
        switch (aCaveID)
        {
            case 0:
                _flagText.text = "A";
                break;
            case 1:
                _flagText.text = "O";
                break;
            case 2:
                _flagText.text = "N";
                break;
            case 3:
                _flagText.text = "R";
                break;
            default:
                break;
        }
    }

    // return cave number
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
