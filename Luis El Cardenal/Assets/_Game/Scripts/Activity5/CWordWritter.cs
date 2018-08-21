using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWordWritter : MonoBehaviour {

    [SerializeField]
    string _word;

    public string GetWord()
    {
        return _word;
    }
}
