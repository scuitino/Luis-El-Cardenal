﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActivityManager5 : CActivity {

    #region SINGLETON PATTERN
    public static CActivityManager5 _instance = null; //static - the same variable is shared by all instances of the class that are created
    #endregion

    // min & max letters of each group on the table
    [SerializeField]
    int _minWordsPerGame, _maxWordsPerGame;

    // ID of first group
    int _group1ID;

    // number of words of each group
    [SerializeField]
    int _group1WordsCount, _group2WordsCount;

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
    [SerializeField]
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
        while (!tReady) // while all posible words are not selected
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

        // selecting first word
        int tFirstWordIndex = Random.Range(0, _allSelectedWords.Count);
        _wordsOnTheTable.Add(_allSelectedWords[tFirstWordIndex]);
        _group1WordsCount++;

        // setting group 1 ID
        _group1ID = _allSelectedWords[tFirstWordIndex].GetComponent<DragAndDropItem>().GetItemID();

        // removing first item from the all selected list
        _allSelectedWords.RemoveAt(tFirstWordIndex);

        // selecting rest of the table words
        for (int i = 0; i < 7; i++)
        {
            int tSelectedWord = Random.Range(0, _allSelectedWords.Count);
            if (_allSelectedWords[tSelectedWord].GetComponent<DragAndDropItem>().GetItemID() == _group1ID) // if the selected is of group 1
            {
                if (_group1WordsCount + 1 <= _maxWordsPerGame) // if the group 1 is not at max
                {
                    _wordsOnTheTable.Add(_allSelectedWords[tSelectedWord]);
                    _group1WordsCount++;
                    _allSelectedWords.RemoveAt(tSelectedWord);
                }
                else // select group 2
                {
                    bool tReady2 = false;
                    while (!tReady2) // while a group 2 word is not selected
                    {
                        tSelectedWord = Random.Range(0, _allSelectedWords.Count);
                        if (_allSelectedWords[tSelectedWord].GetComponent<DragAndDropItem>().GetItemID() != _group1ID) // if the selected is of group 2
                        {
                            _wordsOnTheTable.Add(_allSelectedWords[tSelectedWord]);
                            _group2WordsCount++;
                            _allSelectedWords.RemoveAt(tSelectedWord);
                            tReady2 = true;
                        }
                    }
                }
            }
            else // if the selected is of group 2
            {
                if (_group2WordsCount + 1 <= _maxWordsPerGame) // if the group 2 is not at max
                {
                    _wordsOnTheTable.Add(_allSelectedWords[tSelectedWord]);
                    _group2WordsCount++;
                    _allSelectedWords.RemoveAt(tSelectedWord);
                }
                else // select group 1
                {
                    bool tReady2 = false;
                    while (!tReady2)
                    {
                        tSelectedWord = Random.Range(0, _allSelectedWords.Count);
                        if (_allSelectedWords[tSelectedWord].GetComponent<DragAndDropItem>().GetItemID() == _group1ID) // if the selected is of group 1
                        {
                            _wordsOnTheTable.Add(_allSelectedWords[tSelectedWord]);
                            _group1WordsCount++;
                            _allSelectedWords.RemoveAt(tSelectedWord);
                            tReady2 = true;
                        }
                    }
                }
            }
        }
    }
}
