using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActivityManager6 : CActivity
{

    #region SINGLETON PATTERN
    public static CActivityManager6 _instance = null; //static - the same variable is shared by all instances of the class that are created
    #endregion

    // letter groups
    [SerializeField]
    List<string> _posibleLetters;

    // selected letters group
    List<string> _selectedLetters;

    // words list of each letter
    [SerializeField]
    List<GameObject> _aWords, _lWords, _mWords, _sWords;

    // all selected words
    [SerializeField]
    List<GameObject> _allSelectedWords;

    // actual player option
    [SerializeField]
    Transform _option;

    // trains reference
    List<CTrain> _boxes;

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
        _selectedLetters = new List<string>();
        SelectGameWords();
    }

    // select all the words for the next game
    public void SelectGameWords()
    {
        _startFlag.SetActive(true);

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
                case "L":
                    for (int i = 0; i < _lWords.Count; i++)
                    {
                        _allSelectedWords.Add(_lWords[i]);
                    }
                    break;
                case "M":
                    for (int i = 0; i < _mWords.Count; i++)
                    {
                        _allSelectedWords.Add(_mWords[i]);
                    }
                    break;
                case "S":
                    for (int i = 0; i < _sWords.Count; i++)
                    {
                        _allSelectedWords.Add(_sWords[i]);
                    }
                    break;
                default:
                    break;
            }

            tSelectedCount++;

            if (tSelectedCount == 3)
                tReady = true;
        }

        // selecting first word
        int tFirstWordIndex = Random.Range(0, _allSelectedWords.Count);

        // instantiate and set player option
        SettingPlayerOption(_allSelectedWords[tFirstWordIndex]);        

        // removing first item from the all selected list
        _allSelectedWords.RemoveAt(tFirstWordIndex);

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
        DragAndDropItem.dragDisabled = false;
    }

    // turn of ready
    void NotReady()
    {
        DragAndDropItem.dragDisabled = true;
    }
}
