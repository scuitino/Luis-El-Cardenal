using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActivityManager5 : CActivity {

    #region SINGLETON PATTERN
    public static CActivityManager5 _instance = null; //static - the same variable is shared by all instances of the class that are created
    #endregion

    // minimun letters of each group on the table
    [SerializeField]
    int _minWordsPerGame;

    // number of words of each group
    [SerializeField]
    int _group1Words, _group2Words;

    // letter groups
    [SerializeField]
    List<string> _posibleLetters;

    // words list of each letter
    [SerializeField]
    List<GameObject> _aWords, _oWords, _nWords, _rWords;

    // all selected words
    [SerializeField]
    List<GameObject> _allSelectedWords;

    // words on the table
    List<GameObject> _wordsOnTheTable;

    private void Awake()
    {
        //Singleton check
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        _allSelectedWords = new List<GameObject>();
        SelectGameWords();
    }

    // select all the words for the next game
    public void SelectGameWords()
    {
        int tSelectedCount = 0; // words needed to start
        bool tReady = false;
        while (tReady == false) // while all posible words are not selected
        {
            // selecting random letter
            int tRandomLetterIndex = Random.Range(0, _posibleLetters.Count);
            string tSelectedLetter = _posibleLetters[tRandomLetterIndex];
            _posibleLetters.RemoveAt(tRandomLetterIndex);

            // adding selected words to the posible words for this game
            switch (tSelectedLetter)
            {
                case "A":
                    for (int i = 0; i < _aWords.Count; i++)
                    {
                        _allSelectedWords.Add(_aWords[i]);
                    }
                    break;
                case "O":
                    for (int i = 0; i < _oWords.Count; i++)
                    {
                        _allSelectedWords.Add(_oWords[i]);
                    }
                    break;
                case "N":
                    for (int i = 0; i < _nWords.Count; i++)
                    {
                        _allSelectedWords.Add(_nWords[i]);
                    }
                    break;
                case "R":
                    for (int i = 0; i < _rWords.Count; i++)
                    {
                        _allSelectedWords.Add(_rWords[i]);
                    }
                    break;
                default:
                    break;
            }

            tSelectedCount++;

            if (tSelectedCount == 2)
                tReady = true;
        }
    }
}
