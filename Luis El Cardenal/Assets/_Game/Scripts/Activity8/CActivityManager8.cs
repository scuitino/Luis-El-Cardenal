using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        _aSource = this.GetComponent<AudioSource>();
        //_helpAnimator.SetBool("Activity8", true);
        StartCoroutine("NextChallenge");
    }

    // change challenge
    public IEnumerator NextChallenge()
    {
        // Reset Challenge
        for (int i = 0; i < _slots.Count; i++)
        {
            _slots[i]._correctAnswer = false;
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
        }

        _aSource.Play();
    }

    // call when the player answer is correct
    public void CorrectAnswer()
    {
        // play animatios
        _molinoAnimator.SetTrigger("Success");
        if (_licuadoraAnimator.isActiveAndEnabled)
            _licuadoraAnimator.SetTrigger("Play");
        if (_lampAnimator.isActiveAndEnabled)
            _lampAnimator.SetTrigger("Play");
        Debug.Log("Acertaste");
    }

    // call when the player answer is wrong
    public void IncorrectAnswer()
    {
        Debug.Log("Erraste");
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
}
