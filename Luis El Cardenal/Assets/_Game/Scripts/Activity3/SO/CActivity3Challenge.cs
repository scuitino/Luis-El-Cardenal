using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Challenge", menuName = "Activities/Activity3/Challenge")]
public class CActivity3Challenge : ScriptableObject
{
    public GameObject _item1, _item2;           // correct items of this challenges
    public bool _wasUsed;                       // to know if this challenge was used
}
