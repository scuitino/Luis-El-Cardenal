using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CItemA1 : MonoBehaviour {

    // to identify each item
    [SerializeField]
    int _itemID;
	
    // get id of the item
    public int GetID()
    {
        return _itemID;
    }

    public void PlayNextWord()
    {
        CActivityManager1._instance.PlayWord();
    }
}
