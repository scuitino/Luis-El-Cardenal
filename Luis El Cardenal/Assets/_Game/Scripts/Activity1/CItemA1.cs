using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CItemA1 : MonoBehaviour {

    // to identify each item
    [SerializeField]
    int _itemID;
	
    public int GetID()
    {
        return _itemID;
    }
}
