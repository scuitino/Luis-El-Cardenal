using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCave : MonoBehaviour {

    // cave group number
    [SerializeField]
    int _caveNumber;

    // set Cave ID
    public void SetCaveID(int aCaveID)
    {
        _caveNumber = aCaveID;
    }

    // return cave number
    public int GetCaveID()
    {
        return _caveNumber;
    }
}
