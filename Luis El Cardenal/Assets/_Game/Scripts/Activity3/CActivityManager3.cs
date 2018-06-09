using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CActivityManager3 : CActivity {

    #region SINGLETON PATTERN
    public static CActivityManager3 _instance = null; //static - the same variable is shared by all instances of the class that are created
    #endregion

    // syllables for all questions on this level
    [SerializeField, Header("Configuration")]
    List<CActivity3Challenge> _challengesOnThisLevel;

    // the new selected
    int _selectedChallenge;
    // actual challenge index of the list
    int _actualChallenge;

    // to select extra option
    int _extraOption;

    //how many good answers to win
    [SerializeField]
    int _goodAnswersToWin;

    // number of good answers
    int _successCount;

    // result slots
    [SerializeField, Header("References")]
    DragAndDropCell _flower1;
    [SerializeField]
    DragAndDropCell _flower2;

    // option slots
    [SerializeField]
    Transform _option1, _option2, _option3;

    // to move result button
    [SerializeField]
    DOTweenAnimation _resultButton;

    // to move options
    [SerializeField]
    DOTweenAnimation _optionsContainer;

    // to animate the rabbit
    [SerializeField]
    Animator _rabbitAnimator;

    // list of new instantiated items
    [SerializeField]
    List<Transform> _instantiatedItems;

    // to control luis animations
    [SerializeField]
    Animator _luisAnimator;

    // to modify the leaf
    [SerializeField]
    List<Sprite> _leafSprites;

    // Image of the leaf
    [SerializeField]
    Image _leafImage;

    // current leaf sprite
    [SerializeField]
    int _currentLeaf;

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
        _helpAnimator.SetBool("Activity3", true);
        //StartCoroutine(PlayChallenge()); 
    }

    // to control when the player can play
    public override void ChangeReady(bool aOption)
    {        
        base.ChangeReady(aOption);
        _resultButton.DOPlayForward();
        _optionsContainer.DOPlayForward();
    }

    // select and play the next challenge
    public IEnumerator PlayChallenge()
    {
        _startFlag.SetActive(true);
        yield return null;
        bool tReady = false;
        while (!tReady) // searching challenge data
        {
            _selectedChallenge = Random.Range(0, _challengesOnThisLevel.Count);
            if (_selectedChallenge != _actualChallenge)
            {
                tReady = true;
            }
            yield return null;
        }
        _actualChallenge = _selectedChallenge;
        CActivity3Challenge tSelectedChallengeData = _challengesOnThisLevel[_actualChallenge];

        // instantiate correct answers        
        _instantiatedItems = new List<Transform>();
        _instantiatedItems.Add(Instantiate(tSelectedChallengeData._item1).transform);
        _instantiatedItems.Add(Instantiate(tSelectedChallengeData._item2).transform);

        tReady = false;
        while (!tReady) // searching wrong answer
        {
            _extraOption = Random.Range(0, _challengesOnThisLevel.Count);

            bool tRandomBool = (Random.Range(0, 2) == 0); // to select a random item from the data
            if (tRandomBool == true)
            {
                if (_challengesOnThisLevel[_extraOption]._item1.GetComponent<DragAndDropItem>().GetItemID() != _instantiatedItems[0].GetComponent<DragAndDropItem>().GetItemID()) // to control that the items doesn't repeat
                {
                    _instantiatedItems.Add(Instantiate(_challengesOnThisLevel[_extraOption]._item1.transform));
                    tReady = true;
                }
            }
            else
            {
                if (_challengesOnThisLevel[_extraOption]._item2.GetComponent<DragAndDropItem>().GetItemID() != _instantiatedItems[0].GetComponent<DragAndDropItem>().GetItemID()) // same
                {
                    _instantiatedItems.Add(Instantiate(_challengesOnThisLevel[_extraOption]._item2.transform));
                    tReady = true;
                }
            }
            yield return null;
        }        

        foreach (Transform tTransform in _instantiatedItems) // selecting slots
        {            
            int tRandomSlot;
            tReady = false;
            while (!tReady) // while the slot is not selected
            {
                tRandomSlot = Random.Range(0, 3); // trying random slot
                switch (tRandomSlot)
                {
                    case 0:
                        if (_option1.childCount == 0) // if the slot is empty
                        {
                            tTransform.SetParent(_option1);
                            tReady = true;
                        }
                        break;
                    case 1:
                        if (_option2.childCount == 0)
                        {
                            tTransform.SetParent(_option2);
                            tReady = true;
                        }
                        break;
                    case 2:
                        if (_option3.childCount == 0)
                        {
                            tTransform.SetParent(_option3);
                            tReady = true;
                        }
                        break;
                }
                // re-size
                tTransform.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0); // new Vector2(left, bottom); 
                tTransform.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0); // new Vector2(right, top);
                tTransform.GetComponent<RectTransform>().localPosition = Vector3.zero;  // to set z Pos 
                tTransform.localScale = Vector3.one;
                yield return null;
            }               
        }
        ChangeReady(true);
    }

    // restart the options and results
    public void RestartChallenge()
    {
        if (!_win)
        {
            if (_option1.childCount > 0)
            {
                _instantiatedItems.Remove(_option1.GetChild(0));
                Destroy(_option1.GetChild(0).gameObject);
            }

            if (_option2.childCount > 0)
            {
                _instantiatedItems.Remove(_option2.GetChild(0));
                Destroy(_option2.GetChild(0).gameObject);
            }

            if (_option3.childCount > 0)
            {
                _instantiatedItems.Remove(_option3.GetChild(0));
                Destroy(_option3.GetChild(0).gameObject);
            }

            if (_flower1.transform.childCount > 0)
            {
                _instantiatedItems.Remove(_flower1.transform.GetChild(0));
                Destroy(_flower1.transform.GetChild(0).gameObject);
            }

            if (_flower2.transform.childCount > 0)
            {
                _instantiatedItems.Remove(_flower1.transform.GetChild(0));
                Destroy(_flower2.transform.GetChild(0).gameObject);
            }

            StartCoroutine(PlayChallenge());
        }        
    }

    // check result of the actual question
    public void CheckResult()
    {
        if (_readyToPlay)
        {
            if (_flower1.GetItem() != null && _flower2.GetItem() != null) // if both result slots are filled
            {
                _resultButton.DOPlayBackwards();
                _optionsContainer.DOPlayBackwards();
                if (_flower1.GetItem().GetItemID() == _flower2.GetItem().GetItemID()) // if the ids of the answers are the same
                {
                    _luisAnimator.SetTrigger("Success");
                    _rabbitAnimator.SetTrigger("Success");
                    _successCount++;
                    _challengesOnThisLevel.RemoveAt(_actualChallenge);
                    if (_successCount >= _goodAnswersToWin)
                    {
                        _win = true;
                        Invoke("WinGame", 2);
                    }
                    Debug.Log("son iguales");
                }
                else // if the ids are different
                {
                    _luisAnimator.SetTrigger("Fail");
                    _rabbitAnimator.SetTrigger("Fail");
                    Debug.Log("son diferentes");
                }
            }
            else // if the player doesn't select two items
            {
                Debug.Log("debe poner dos objetos");
            }
        }        
    }

    // to change leaf sprite
    public void CutLeaf()
    {
        _currentLeaf += 1;
        _leafImage.sprite = _leafSprites[_currentLeaf];
    }
}
