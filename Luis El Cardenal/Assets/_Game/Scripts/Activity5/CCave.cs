using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCave : MonoBehaviour {

    // cave group ID
    [SerializeField]
    int _caveID;

    // know if is the left or right cave
    [SerializeField]
    int _caveNumber;

    // set Cave ID
    public void SetCaveID(int aCaveID)
    {
        _caveID = aCaveID;
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
