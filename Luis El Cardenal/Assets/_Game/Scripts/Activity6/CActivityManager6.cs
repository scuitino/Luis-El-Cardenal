using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CActivityManager6 : CActivity
{

    #region SINGLETON PATTERN
    public static CActivityManager6 _instance = null; //static - the same variable is shared by all instances of the class that are created
    #endregion

    // points to win
    [SerializeField]
    int _scoreToWin;

    // actual player points
    public int _playerScore;

    // to control luis animations
    [SerializeField]
    Animator _luisAnimator;

    // train animator
    [SerializeField]
    Animator _trainAnimator;

    // actual player option
    [SerializeField]
    Transform _option;

    // to animate results
    [SerializeField]
    List<Animator> _resultAnimators;

    // to change the sprite of the result
    [SerializeField]
    List<Image> _resultImages;

    // letter groups
    [SerializeField]
    List<string> _posibleLetters;

    // selected letters group
    List<string> _selectedLetters;

    // words list of each letter
    [SerializeField]
    List<GameObject> _aWords, _lWords, _mWords, _sWords;

    // all selected words
    List<GameObject> _allSelectedWords;

    // playing words
    [SerializeField]
    List<GameObject> _playingWords;

    // trains reference
    [SerializeField]
    List<CTrain> _boxes;

    // to know if word sound is playing
    public bool _wordIsPlaying;

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
        _helpAnimator.SetBool("Activity6", true);
        _allSelectedWords = new List<GameObject>();
        _playingWords = new List<GameObject>();
        _selectedLetters = new List<string>();
        SelectGameWords();
        Invoke("FixDragAndDrop", 2);
    }

    // to fix the freeze error
    void FixDragAndDrop()
    {
        DragAndDropItem.dragDisabled = false;
    }

    // select all the words for the next game
    public void SelectGameWords()
    {     
        // starting words select
        int tSelectedCount = 0; // words needed to start
        bool tReady = false;
        while (!tReady) // while all posible words are not selected
        {
            // selecting random letter
            int tRandomLetterIndex = Random.Range(0, _posibleLetters.Count);
            string tSelectedLetter = _posibleLetters[tRandomLetterIndex];
            _selectedLetters.Add(tSelectedLetter);
            _posibleLetters.RemoveAt(tRandomLetterIndex);

            // adding selected words to the posible words for this game
            switch (tSelectedLetter)
            {
                case "A":
                    for (int i = 0; i < _aWords.Count; i++)
                    {
                        _allSelectedWords.Add(_aWords[i]);
                        _playingWords.Add(_aWords[i]);
                    }
                    break;
                case "L":
                    for (int i = 0; i < _lWords.Count; i++)
                    {
                        _allSelectedWords.Add(_lWords[i]);
                        _playingWords.Add(_lWords[i]);
                    }
                    break;
                case "M":
                    for (int i = 0; i < _mWords.Count; i++)
                    {
                        _allSelectedWords.Add(_mWords[i]);
                        _playingWords.Add(_mWords[i]);
                    }
                    break;
                case "S":
                    for (int i = 0; i < _sWords.Count; i++)
                    {
                        _allSelectedWords.Add(_sWords[i]);
                        _playingWords.Add(_sWords[i]);
                    }
                    break;
                default:
                    break;
            }

            tSelectedCount++;

            if (tSelectedCount == 3)
                tReady = true;
        }

        // setting up trains
        TrainsSetUp();        
    }

    // select next word
    public void NextWord()
    {
        if (_playingWords.Count == 0)
        {
            for (int i = 0; i < _allSelectedWords.Count; i++)
            {
                _playingWords.Add(_allSelectedWords[i]);
            }
        }

        // selecting first word
        int tNextWord = Random.Range(0, _playingWords.Count);

        // instantiate and set player option
        SettingPlayerOption(_playingWords[tNextWord]);

        // removing first item from the all selected list
        _playingWords.RemoveAt(tNextWord);

        SetReady();
    }

    // setting up and instantiate the actual challenge
    public void SettingPlayerOption(GameObject aActualChallenge)
    {
        Transform tSelectedItem = Instantiate(aActualChallenge).transform;
        tSelectedItem.SetParent(_option.transform);
        tSelectedItem.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0); // new Vector2(left, bottom); 
        tSelectedItem.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0); // new Vector2(right, top);
        tSelectedItem.transform.GetComponent<RectTransform>().localPosition = Vector3.zero;  // to set z Pos 
        tSelectedItem.transform.localScale = Vector3.one;
    }

    // setting up trains
    public void TrainsSetUp()
    {
        for (int i = 0; i < _selectedLetters.Count; i++)
        {
            _boxes[i].SetTrainID(_selectedLetters[i]);
        }
    }

    // set ready to play true
    void SetReady()
    {
        _readyToPlay = true;
        DragAndDropItem.dragDisabled = false;
    }

    // turn of ready
    void NotReady()
    {
        _readyToPlay = false;
        DragAndDropItem.dragDisabled = true;
    }

    // to animate Luis
    public void AnimateResult(bool aOption, int aTrainNumber, Sprite aItemSprite)
    {
        NotReady();     
        
        if (aOption) // animate good
        {
            // animate train and luis
            _trainAnimator.SetTrigger("Success");
            GoodAnimation();

            // animate result
            _resultImages[aTrainNumber].sprite = aItemSprite;
            _resultAnimators[aTrainNumber].SetTrigger("Good");
            
            // check score
            _playerScore++;
            CWormsManager._instance.Collect();
            if (_playerScore == _scoreToWin)
            {
                Invoke("WinGame", 7);
                _trainAnimator.SetTrigger("Win");
            }
            else
            {
                // invoke next word
                Invoke("NextWord", 2);
                Invoke("SetReady", 2);
            }
        }
        else // animate bad
        {
            // animate train and luis
            BadAnimation();

            // animate results
            _resultImages[aTrainNumber].sprite = aItemSprite;
            _resultAnimators[aTrainNumber].SetTrigger("Bad");

            // invote next word
            Invoke("NextWord", 2);
            Invoke("SetReady", 2);
        }
    }

    // to not allow the player touch two words at same time
    public void DelayWordSound(float aDelay)
    {
        _wordIsPlaying = true;
        Invoke("ChangeWordSoundState",aDelay);
    }

    public void ChangeWordSoundState()
    {
        _wordIsPlaying = false;
    }

    // Enable Option and train
    public void EnableChallenges()
    {
        // selecting next word
        Invoke("NextWord", 1.5f);        
        _startFlag.SetActive(true);
        TurnOffSkipButton();
        _luisAnimator.gameObject.GetComponent<Button>().enabled = true;
        _option.gameObject.SetActive(true);
        _trainAnimator.gameObject.SetActive(true);
    }

    // good luis
    void GoodAnimation()
    {
        _luisAnimator.SetTrigger("Success");
    }

    // bad luis
    void BadAnimation()
    {
        _luisAnimator.SetTrigger("Fail");
    }

    // when the player press luis button
    public void ReplayTutorial()
    {
        _luisAnimator.SetBool("Talking", true);
        _replayTutorialASource.Play();
        PauseGameplay(false);
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
        PauseGameplay(true);
        _skipReplayButton.SetActive(false);
        StopCoroutine(_stopTalkingCo);
    }

    // to pause gameplay
    public void PauseGameplay(bool aOption)
    {
        _readyToPlay = aOption;
        DragAndDropItem.dragDisabled = !aOption;
    }

    // stop talking when replay ends
    IEnumerator StopTalking(float aDelay)
    {
        yield return new WaitForSeconds(aDelay);
        SkipReTutorial();
    }
}
