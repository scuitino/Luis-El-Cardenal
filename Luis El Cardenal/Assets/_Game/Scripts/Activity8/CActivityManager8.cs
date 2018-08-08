using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CActivityManager8 : CActivity {

    #region SINGLETON PATTERN
    public static CActivityManager8 _instance = null;
    #endregion

    //to play sentences
    AudioSource _aSource;

    // replay sentence button
    [SerializeField]
    GameObject _replayButton;

    // tutorial time
    bool _isTutorial;

    // player score
    int _score;

    // to control animations
    [SerializeField]
    Animator _luisAnimator, _lampAnimator, _licuadoraAnimator, _molinoAnimator;

    // to manage the challenges
    [SerializeField]
    List<CChallengeA8> _availableChallenges;
    CChallengeA8 _selectedChallenge ,_lastChallenge;

    // answer slots
    [SerializeField]
    List<CAnswer> _slots;

    // try error sound
    [SerializeField]
    AudioSource _errorSound;

    //errors count per challenge
    int _errorsCount;

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
        _aSource = this.GetComponent<AudioSource>();
        _helpAnimator.SetBool("Activity8", true);
        StartCoroutine("NextChallenge");
    }

    // change challenge
    public IEnumerator NextChallenge()
    {
        if (!_win)
        {
            // Reset Challenge
            for (int i = 0; i < _slots.Count; i++)
            {
                _slots[i]._correctAnswer = false;
            }

            if (_lampAnimator.isActiveAndEnabled)
            {
                _lampAnimator.gameObject.SetActive(false);
                _licuadoraAnimator.gameObject.SetActive(true);
            }
            else
            {
                _licuadoraAnimator.gameObject.SetActive(false);
                _lampAnimator.gameObject.SetActive(true);
            }

            int tSelectedChallengeIndex = Random.Range(0, _availableChallenges.Count);
            // save selected
            _selectedChallenge = (_availableChallenges[tSelectedChallengeIndex]);
            // remove selected from available list
            _availableChallenges.RemoveAt(tSelectedChallengeIndex);
            // return last challenge to available list
            if (_lastChallenge != null)
            {
                _availableChallenges.Add(_lastChallenge);
            }

            _lastChallenge = _selectedChallenge;

            // setting good answer
            _aSource.clip = _selectedChallenge._clip;

            int tSelectedSlot = Random.Range(0, _slots.Count);
            _slots[tSelectedSlot]._correctAnswer = true;
            _slots[tSelectedSlot]._text.text = _selectedChallenge._letter;

            // setting up wrong answers
            yield return StartCoroutine(ShuffleList(_availableChallenges));

            int tTemp = 0;
            for (int i = 0; i < _slots.Count; i++)
            {
                if (i != tSelectedSlot)
                {
                    _slots[i]._text.text = _availableChallenges[tTemp]._letter;
                    tTemp++;
                }
                if (!_isTutorial)
                {
                    _slots[i].GetComponent<Animator>().SetTrigger("Show");                   
                }
            }

            if (_isTutorial)
            {
                _isTutorial = false;
            }

            _aSource.Play();
            _replayButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            WinGame();
        }        
    }

    // call when the player answer is correct
    public void CorrectAnswer()
    {
        _replayButton.GetComponent<Button>().interactable = false;

        // play animatios
        _luisAnimator.SetTrigger("Success");
        _molinoAnimator.SetTrigger("Success");
        if (_licuadoraAnimator.isActiveAndEnabled)
            _licuadoraAnimator.SetTrigger("Play");
        if (_lampAnimator.isActiveAndEnabled)
            _lampAnimator.SetTrigger("Play");

        for (int i = 0; i < _slots.Count; i++)
        {
            _slots[i].GetComponent<Animator>().SetTrigger("Hide");
        }

        _score++;
        _errorsCount = 0;
        if (_score == 5)
        {
            _win = true;
        }
    }

    // call when the player answer is wrong
    public void IncorrectAnswer()
    {
        _errorsCount++;
        if (_errorsCount < 3)
        {
            _errorSound.Play();
        }
        else
        {
            for (int i = 0; i < _slots.Count; i++)
            {
                _slots[i].GetComponent<Animator>().SetTrigger("Hide");
            }
            _luisAnimator.SetTrigger("Fail");
            StartCoroutine("NextChallenge");
            _errorsCount = 0;
        }
    }

    // shufle List
    public IEnumerator ShuffleList(List<CChallengeA8> aList)
    {        
        //Fisher Shates Shuffle
        for (int i = aList.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i+1);
            CChallengeA8 tTemp = aList[i];
            aList[i] = aList[j];
            aList[j] = tTemp;
        }
        yield return null;
    }

    // Play Sentence
    public void PlaySentence()
    {
        _aSource.Play();
    }
}
