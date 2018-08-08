using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Word", menuName = "Activities/Activity2/Word")]
public class CWordA2 : ScriptableObject {

    public string _word;                    // name of the object
    public List<string> _syllables;         // all the syllables of the word
    public List<Sprite> _images;                   // sprite of the object
    public List<AudioClip> _sounds;                   // sounds of the object
    public int _numberOfSyllables;          // number of syllables of the word    
    public bool _wasUsed;                   // to know if the word was used
}
