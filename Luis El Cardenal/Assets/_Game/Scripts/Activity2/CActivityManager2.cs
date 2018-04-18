using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActivityManager2 : MonoBehaviour {

    // list of all the words available on this activity
    [SerializeField]
    List<CWordA2> _wordsList;

    // answer of the actual question 
    [SerializeField]
    int _playerAnswer;

    // actual question word
    CWordA2 _actualWord;

    private void Start()
    {
        PlayWord();
    }

    // Play with the next word
    public void PlayWord()
    {
        _playerAnswer = 0; // reset the answer
        bool tReady = false;
        while (!tReady) // while the next word is not selected
        {
            CWordA2 tSelectedWord = _wordsList[Random.Range(0, _wordsList.Count)];
            if (!tSelectedWord._wasUsed) //  is the word was not used
            {
                _actualWord = tSelectedWord;
                tReady = true; // ready to play
            }
        }        
        // TODO: play animation of the word
    }

    // to add 1 to syllables answer
    public void AddSyllable()
    {
        _playerAnswer++;
    }

    // to remove 1 to syllables answer
    public void RemoveSyllable()
    {
        _playerAnswer--;
    }

    // check result of the actual question
    public void CheckResult()
    {
        if (_playerAnswer == _actualWord._numberOfSyllables)
        {
            Debug.Log("Respuesta correcta.");
        }
        else
        {
            Debug.Log("Respuesta incorrecta, intentalo de nuevo.");
        }
    }
}
