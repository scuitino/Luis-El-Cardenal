using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActivityManager1 : CActivity
{
    // how many items on the screen
    [SerializeField]
    int _itemsOnScreen;

    // necesary words to win
    [SerializeField]
    int _wordsToWin;

    // every good answer +1
    int _winsCount;

    // to play words sounds
    AudioSource _audioSource;

    // all the words sounds
    [SerializeField]
    List<AudioClip> _wordsSounds;

    // all the items of the game
    [SerializeField]
    List<GameObject> _allItems;

    // items that are selected for this game
    [SerializeField]
    List<GameObject> _selectedItems;

    // actual correct answer
    [SerializeField]
    int _correctAnswer;

    // to remove from the list when was used
    int _selectedItemIndex;

    private void Start()
    {
        _audioSource = this.GetComponent<AudioSource>();
        SelectGameWords();
        PlayWord();
    }   

    // select all the words for the next game
    public void SelectGameWords()
    {
        for (int i = 0; i < _itemsOnScreen; i++) // selecting words
        {
            int tSelectedItem = Random.Range(0, _allItems.Count);
            _selectedItems.Add(_allItems[tSelectedItem]); // select a random item from the list
            _allItems.RemoveAt(tSelectedItem); // remove the selected item from the all items list
        }

        // active items on screen
        foreach (GameObject tItem in _selectedItems)
        {
            tItem.SetActive(true);
        }
    }

    // to start a new word
	public void PlayWord()
    {
        if (!_win)
        {
            // select the next word to play
            _selectedItemIndex = Random.Range(0, _selectedItems.Count);
            _audioSource.clip = _wordsSounds[_selectedItems[_selectedItemIndex].GetComponent<CItemA1>().GetID()];            
            _correctAnswer = _selectedItems[_selectedItemIndex].GetComponent<CItemA1>().GetID();

            // play word sound
            _audioSource.Play();
        }
    }

    // to check player answer
    public void CheckResult(int aAnswer)
    {
        if (aAnswer == _correctAnswer) // if the answer is correct
        {
            Debug.Log("Gano");
            _winsCount++;
            if (_winsCount >= _wordsToWin) // if the player reach the goal
            {
                WinGame();
            }
            else
            {
                _selectedItems.RemoveAt(_selectedItemIndex);
            }            
        }
        else
        {
            Debug.Log("Perdio");
        }
        if (!_win)
        {
            PlayWord();
        }        
    }
}
