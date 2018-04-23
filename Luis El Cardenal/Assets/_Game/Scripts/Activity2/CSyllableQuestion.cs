using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSyllableQuestion : MonoBehaviour {

    // number of syllables of this prefab
    [SerializeField]
    int _numberOfSyllables;

    // reference to load the sprites
    [SerializeField]
    List<Image> _imagesToFill;

    public void LoadImageParts(CWordA2 aWord)
    {
        for (int i = 0; i < _numberOfSyllables; i++)
        {
            _imagesToFill[i].sprite = aWord._images[i];
        }
    }
}
