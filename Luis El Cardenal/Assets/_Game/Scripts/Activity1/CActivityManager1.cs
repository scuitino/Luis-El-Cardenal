﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CActivityManager1 : CActivity
{
    #region SINGLETON PATTERN
    public static CActivityManager1 _instance = null; //static - the same variable is shared by all instances of the class that are created
    #endregion

    // how many items on the screen
    [SerializeField]
    int _itemsOnScreen;

    // necesary words to win
    [SerializeField]
    int _wordsToWin;

    // every good answer +1
    public int _winsCount;

    // to play words sounds
    AudioSource _audioSource;

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

    // to check if the carpenter or the door was used
    bool _wasUsed;

    // timer and timerLimit to repeat the word sound
    float _timer;
    float _timerLimit = 7;

    // to control luis animations
    [SerializeField]
    Animator _luisAnimator;

    // tutorial time
    bool _isTutorial;

    // replay speaker animator
    [SerializeField]
    Animator _speakerAnimator;

    // first challenge flag
    bool _firstChallenge;

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
        _isTutorial = true;
        _audioSource = this.GetComponent<AudioSource>();
        SelectGameWords();
        //PlayWord();
        _helpAnimator.SetBool("Activity1", true);
        _firstChallenge = true;
    }

    private void Update()
    {
        if (_readyToPlay)
        {
            if(_timer >= _timerLimit) // if the timer reach the limit play the sound
            {
                ReplaySound();                
            }
            _timer += Time.deltaTime;
        }    
    }

    public void ReplaySound()
    {        
        if (!_selectedItems[_selectedItemIndex].GetComponent<AudioSource>().isPlaying)
        {
            _timer = 0;
            Invoke("StopSpeaker", _selectedItems[_selectedItemIndex].GetComponent<AudioSource>().clip.length);
            _speakerAnimator.SetBool("Playing", true);
            _selectedItems[_selectedItemIndex].GetComponent<AudioSource>().Play();
        }        
    }

    // to stop speaker anim
    public void StopSpeaker()
    {
        _speakerAnimator.SetBool("Playing", false);
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
        for (int i = 0; i < _selectedItems.Count; i++)
        {
            if (_selectedItems[i].GetComponent<CItemA1>().GetID() == 0 || _selectedItems[i].GetComponent<CItemA1>().GetID() == 1) // check if this item is the door or the carpenter
            {
                if (!_wasUsed) // if is one of them but the other is not used
                {
                    _wasUsed = true;
                }
                else // if both are used, delete this and add another item
                {
                    _selectedItems.Remove(_selectedItems[i]);
                    _selectedItems.Add(_allItems[Random.Range(0, _allItems.Count)]);
                }
            }
            // active each item
            _selectedItems[i].SetActive(true);
        }
    }

    // to start a new word
	public void PlayWord()
    {        
        _luisAnimator.gameObject.GetComponent<Button>().enabled = true;
        _speakerAnimator.gameObject.GetComponent<Button>().interactable = true;
        ChangeReady(false);
        if (!_win)
        {
            // select the next word to play
            if (_firstChallenge)
            {
                _selectedItemIndex = 1;
                _firstChallenge = false;
            }
            else
            {
                _selectedItemIndex = Random.Range(0, _selectedItems.Count);
            }            
            //_selectedItems[_selectedItemIndex].GetComponent<CItemA1>().PlaySound();
            ReplaySound();
            _correctAnswer = _selectedItems[_selectedItemIndex].GetComponent<CItemA1>().GetID();

            // waiting for the sound
            //StartCoroutine(PlayOn(_selectedItems[_selectedItemIndex].GetComponent<AudioSource>().clip.length)); 
            StartCoroutine(PlayOn(0));
        }
    }

    // to check player answer
    public void CheckResult(int aAnswer)
    {        
        if (_readyToPlay)
        {            
            ChangeReady(false);
            _speakerAnimator.gameObject.GetComponent<Button>().interactable = false;
            if (aAnswer == _correctAnswer) // if the answer is correct
            {
                _winsCount++;
                _luisAnimator.SetTrigger("Success");                
                _selectedItems[_selectedItemIndex].GetComponent<Animator>().SetTrigger("Play"); // animate word
                if (_winsCount >= _wordsToWin) // if the player reach the goal
                {
                    _win = true;
                    StartCoroutine(WinGame(_selectedItems[_selectedItemIndex].GetComponent<AudioSource>().clip.length)); // win the game!
                }
                else
                {
                    _selectedItems.RemoveAt(_selectedItemIndex); // remove used word and continue
                }
                CWormsManager._instance.Collect();                
            }
            else
            {
                Invoke("PlayWord", 2.5f);
                _luisAnimator.SetTrigger("Fail");
                foreach (GameObject tItem in _selectedItems)
                {
                    tItem.GetComponent<AudioSource>().Stop();
                }
            }
        }        
    }

    // to control when the player can play
    public IEnumerator PlayOn(float aDelay)
    {
        yield return new WaitForSeconds(aDelay);
        _speakerAnimator.gameObject.GetComponent<Button>().interactable = true;
        ChangeReady(true);
        _timer = 0;
        yield return null;
    }

    // delay before win
    public IEnumerator WinGame(float aDelay)
    {
        yield return new WaitForSeconds(aDelay);
        WinGame();
        yield return null;
    }

    // when the player press luis button
    public void ReplayTutorial()
    {
        _luisAnimator.SetBool("Talking", true);
        _replayTutorialASource.Play();
        if (_selectedItems[_selectedItemIndex].GetComponent<AudioSource>().isPlaying)
        {
            _selectedItems[_selectedItemIndex].GetComponent<AudioSource>().Stop();
            _speakerAnimator.SetBool("Playing", false);
        }
        ChangeReady(false);
        _speakerAnimator.gameObject.GetComponent<Button>().interactable = false;
        _skipReplayButton.SetActive(true);
        if (_stopTalkingCo != null)
        {
            StopCoroutine(_stopTalkingCo);
        }
        _stopTalkingCo = StartCoroutine(StopTalking(_replayTutorialASource.clip.length));
    }

    // when the player skip the tutorial replay
    public void SkipReTutorial()
    {
        _luisAnimator.SetBool("Talking", false);
        _replayTutorialASource.Stop();
        ChangeReady(true);
        _speakerAnimator.gameObject.GetComponent<Button>().interactable = true;
        _skipReplayButton.SetActive(false);
        StopCoroutine(_stopTalkingCo);
    }

    // stop talking when replay ends
    IEnumerator StopTalking(float aDelay)
    {
        yield return new WaitForSeconds(aDelay);
        SkipReTutorial();
    }

    // delay on the start of activity 1
    public void DelayPlay()
    {
        TurnOffSkipButton();
        if (_isTutorial)
        {
            _isTutorial = false;
            _startFlag.SetActive(true);
            //_wormsContainer.SetActive(true);
        }        
        Invoke("PlayWord",2);
    }
}
