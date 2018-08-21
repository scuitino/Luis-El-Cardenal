using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CActivityManager5 : CActivity {

    #region SINGLETON PATTERN
    public static CActivityManager5 _instance = null; //static - the same variable is shared by all instances of the class that are created
    #endregion

    // points to win
    [SerializeField]
    int _scoreToWin = 8;

    // actual player points
    [SerializeField]
    int _playerScore;

    // reference to the caves
    [SerializeField]
    CCave _cave1, _cave2;

    // mulitas animators
    [SerializeField]
    Animator _mulita1, _mulita2;

    // food for the mulita
    [SerializeField]
    Image _food1, _food2;    

    // min & max letters of each group on the table
    [SerializeField]
    int _minWordsPerGame, _maxWordsPerGame;

    // ID of first group
    int _group1ID;

    // selected letters group 
    List<string> _selectedLetters;

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
    List<GameObject> _wordsForTheTable;

    // slots to place the table words
    [SerializeField]
    List<GameObject> _slots;

    // to control luis animations
    [SerializeField]
    Animator _luisAnimator;

    // example sprite to example animation
    [SerializeField]
    Sprite _exampleSprite;

    // sprites to turn off when the game starts
    [SerializeField]
    GameObject _sprite1, _sprite2;

    // reference to word text
    [SerializeField]
    GameObject _word;

    // is tutorial?
    bool _isTutorial;

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
        _helpAnimator.SetBool("Activity5", true);
        _allSelectedWords = new List<GameObject>();
        _selectedLetters = new List<string>();
        NotReady();
        //SelectGameWords();
    }

    // select all the words for the next game
    public void SelectGameWords()
    {
        TurnOffSkipButton();
        _luisAnimator.gameObject.GetComponent<Button>().enabled = true;
        _startFlag.SetActive(true);

        // turn off example sprites
        _sprite1.SetActive(false);
        _sprite2.SetActive(false);

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
        _wordsForTheTable.Add(_allSelectedWords[tFirstWordIndex]);
        _group1WordsCount++;

        // setting group 1 ID and cave ID
        _group1ID = _allSelectedWords[tFirstWordIndex].GetComponent<DragAndDropItem>().GetItemID();
        _cave1.SetCaveID(_group1ID);

        // removing first item from the all selected list
        _allSelectedWords.RemoveAt(tFirstWordIndex);

        // selecting rest of the table words
        for (int i = 0; i < 7; i++)
        {
            TakeAnotherWord();          
        }

        // place the table objects into the slots
        for (int i = 0; i < _wordsForTheTable.Count; i++)
        {
            Transform tSelectedItem = Instantiate(_wordsForTheTable[i]).transform;
            tSelectedItem.SetParent(_slots[i].transform);
            tSelectedItem.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0); // new Vector2(left, bottom); 
            tSelectedItem.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0); // new Vector2(right, top);
            tSelectedItem.transform.GetComponent<RectTransform>().localPosition = Vector3.zero;  // to set z Pos 
            tSelectedItem.transform.localScale = Vector3.one;
        }

        // clear the list
        _wordsForTheTable.Clear();

        SetReady();
    }

    // to select each word
    public void TakeAnotherWord()
    {
        int tSelectedWord = Random.Range(0, _allSelectedWords.Count);
        if (_allSelectedWords[tSelectedWord].GetComponent<DragAndDropItem>().GetItemID() == _group1ID) // if the selected is of group 1
        {
            if (_group1WordsCount + 1 <= _maxWordsPerGame) // if the group 1 is not at max
            {
                _wordsForTheTable.Add(_allSelectedWords[tSelectedWord]);
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
                        _cave2.SetCaveID(_allSelectedWords[tSelectedWord].GetComponent<DragAndDropItem>().GetItemID());
                        _wordsForTheTable.Add(_allSelectedWords[tSelectedWord]);
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
                _cave2.SetCaveID(_allSelectedWords[tSelectedWord].GetComponent<DragAndDropItem>().GetItemID());
                _wordsForTheTable.Add(_allSelectedWords[tSelectedWord]);
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
                        _wordsForTheTable.Add(_allSelectedWords[tSelectedWord]);
                        _group1WordsCount++;
                        _allSelectedWords.RemoveAt(tSelectedWord);
                        tReady2 = true;
                    }
                }
            }
        }
    }

    // when the player answer is wrong
    public void WrongAnswer(Transform aSlot)
    {
        // check if there are avaialable words for each group

        bool _g1Available = false;
        bool _g2Available = false;
        for (int i = 0; i < _allSelectedWords.Count; i++)
        {
            if (_allSelectedWords[i].GetComponent<DragAndDropItem>().GetItemID() == _group1ID)
            {
                _g1Available = true;
            }
            else
            {
                _g2Available = true;
            }
        }

        // adding words needed
        if (!_g1Available)
        {
            switch (_selectedLetters[0])
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
        }
        else if (!_g2Available)
        {
            switch (_selectedLetters[1])
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
        }

        // select a new word
        TakeAnotherWord();

        // placing the word on slot
        Transform tSelectedItem = Instantiate(_wordsForTheTable[0]).transform;

        tSelectedItem.SetParent(aSlot);

        tSelectedItem.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0); // new Vector2(left, bottom); 
        tSelectedItem.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0); // new Vector2(right, top);
        tSelectedItem.transform.GetComponent<RectTransform>().localPosition = Vector3.zero;  // to set z Pos 
        tSelectedItem.transform.localScale = Vector3.one;
        
        // clear the list
        _wordsForTheTable.Clear();
    }

    // to animate Luis
    public void AnimateResult(bool aOption, int aCaveNumber, Sprite aFoodSprite, string aWord)
    {
        NotReady();
        Invoke("SetReady", 4);

        //  setting icon of food 1 and 2
        if (aCaveNumber == 1) 
        {
            _food1.sprite = aFoodSprite;
            _food1.enabled = true;            
        }
        else
        {
            _food2.sprite = aFoodSprite;
            _food2.enabled = true;            
        }

        if (aOption) // animate good
        {
            _word.GetComponent<Text>().text = aWord;
            _word.GetComponent<Animator>().SetTrigger("Play");

            if (aCaveNumber == 1)
            {
                _mulita1.SetTrigger("Success");
                Invoke("DisableFood1", 1.2f);                
            }
            else
            {
                _mulita2.SetTrigger("Success");
                Invoke("DisableFood2", 1.2f);
            }
            Invoke("GoodAnimation", 1);

            if (!_isTutorial)
            {
                CWormsManager._instance.Collect();
            }
            _playerScore ++;            
            if (_playerScore == _scoreToWin)
            {                
                Invoke("WinGame", 5);
            }
        }
        else //  animate bad
        {
            if (aCaveNumber == 1)
            {
                _mulita1.SetTrigger("Fail");
                Invoke("DisableFood1", 3.2f);
            }
            else
            {
                _mulita2.SetTrigger("Fail");                
                Invoke("DisableFood2", 3.2f);
            }
            Invoke("BadAnimation", 1);
        }
    }

    // mulita example animation
    public void StartExampleAnimation()
    {
        AnimateResult(true, 2, _exampleSprite, "RANCHO");
        Invoke("SelectGameWords", 5);
    }

    public void StopExampleAnimation()
    {
        _isTutorial = false;
        _playerScore++;
        CancelInvoke(); // cancel all invokes
    }

    // disable food1
    public void DisableFood1()
    {
        _food1.enabled = false;
    }

    // disable food2
    public void DisableFood2()
    {
        _food2.enabled = false;
    }

    // set ready to play true
    void SetReady()
    {
        DragAndDropItem.dragDisabled = false;
    }

    // turn of ready
    void NotReady()
    {
        DragAndDropItem.dragDisabled = true;
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
