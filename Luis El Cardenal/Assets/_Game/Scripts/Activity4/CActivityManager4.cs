using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActivityManager4 : CActivity {

    #region SINGLETON PATTERN
    public static CActivityManager4 _instance = null; //static - the same variable is shared by all instances of the class that are created
    #endregion

    // challenges
    [SerializeField, Header("Configuration")]
    List<CActivity3Challenge> _caChallenges;
    [SerializeField]
    List<CActivity3Challenge> _esChallenges, _gaChallenges, _laChallenges, _leChallenges, _maChallenges,
        _moChallenges, _oChallenges, _raChallenges, _reChallenges, _teChallenges, _zaChallenges;

    // to take random challenges
    [SerializeField]
    List<int> _challengesAvailables;

    // selected challenges
    [SerializeField]
    List<int> _selectedChallenges;

    //how many good answers to win
    [SerializeField]
    int _goodAnswersToWin;

    // number of good answers
    int _successCount;

    // to know if the player is grabbing a object
    public bool _isGrabbing;

    // result slots
    [SerializeField, Header("References")]
    List<Transform> _flowersSlots;

    // to move result button
    [SerializeField]
    DOTweenAnimation _resultButton;

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
        StartCoroutine(PlayChallenge()); 
    }

    // to control when the player can play
    public override void ChangeReady(bool aOption)
    {        
        base.ChangeReady(aOption);
        _resultButton.DOPlayForward();
    }

    // select and play the next challenge
    public IEnumerator PlayChallenge()
    {
        yield return null;
        for (int i = 0; i < _goodAnswersToWin; i++) // selecting challenges types
        {
            int tSelectedIndex = Random.Range(0,_challengesAvailables.Count);
            _selectedChallenges[i] = _challengesAvailables[tSelectedIndex];
            _challengesAvailables.RemoveAt(tSelectedIndex);
        }

        for (int i = 0; i < _goodAnswersToWin; i++) // spawning selecting challenges
        {
            yield return null;
            int _tSelectedChallenge;
            int _tSelectedFlower;
            GameObject tSelectedItem;
            Debug.Log(_selectedChallenges[i] + "selected");
            switch (_selectedChallenges[i]) 
            {
                case 0: // CA 
                    _tSelectedChallenge = Random.Range(0,_caChallenges.Count); // selecting random challenge of this type

                    _tSelectedFlower = Random.Range(0, _flowersSlots.Count); // selecting random slot
                    tSelectedItem = Instantiate(_caChallenges[_tSelectedChallenge]._item1); // instantiate item1
                    Debug.Log(tSelectedItem.name);
                    // re-size
                    tSelectedItem.transform.SetParent(_flowersSlots[_tSelectedFlower]);
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0); // new Vector2(left, bottom); 
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0); // new Vector2(right, top);
                    tSelectedItem.transform.GetComponent<RectTransform>().localPosition = Vector3.zero;  // to set z Pos 
                    tSelectedItem.transform.localScale = Vector3.one;
                    _flowersSlots.RemoveAt(_tSelectedFlower); // removing selected slot

                    yield return null;

                    _tSelectedFlower = Random.Range(0, _flowersSlots.Count); // selecting random slot
                    tSelectedItem = Instantiate(_caChallenges[_tSelectedChallenge]._item2); // instantiate item2
                    Debug.Log(tSelectedItem.name);
                    // re-size
                    tSelectedItem.transform.SetParent(_flowersSlots[_tSelectedFlower]); // setting parent
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0); // new Vector2(left, bottom); 
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0); // new Vector2(right, top);
                    tSelectedItem.transform.GetComponent<RectTransform>().localPosition = Vector3.zero;  // to set z Pos 
                    tSelectedItem.transform.localScale = Vector3.one;
                    _flowersSlots.RemoveAt(_tSelectedFlower); 
                    break;

                case 1: // ES
                    _tSelectedChallenge = Random.Range(0, _esChallenges.Count); // selecting random challenge of this type

                    _tSelectedFlower = Random.Range(0, _flowersSlots.Count); // selecting random slot for the first item
                    tSelectedItem = Instantiate(_esChallenges[_tSelectedChallenge]._item1); // instantiate item1
                    Debug.Log(tSelectedItem.name);
                    // re-size
                    tSelectedItem.transform.SetParent(_flowersSlots[_tSelectedFlower]);
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0); // new Vector2(left, bottom); 
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0); // new Vector2(right, top);
                    tSelectedItem.transform.GetComponent<RectTransform>().localPosition = Vector3.zero;  // to set z Pos 
                    tSelectedItem.transform.localScale = Vector3.one;
                    _flowersSlots.RemoveAt(_tSelectedFlower); // removing selected slot

                    yield return null;

                    _tSelectedFlower = Random.Range(0, _flowersSlots.Count); // selecting random slot for the second item
                    tSelectedItem = Instantiate(_esChallenges[_tSelectedChallenge]._item2); // instantiate item2
                    Debug.Log(tSelectedItem.name);
                    // re-size
                    tSelectedItem.transform.SetParent(_flowersSlots[_tSelectedFlower]);
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0); // new Vector2(left, bottom); 
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0); // new Vector2(right, top);
                    tSelectedItem.transform.GetComponent<RectTransform>().localPosition = Vector3.zero;  // to set z Pos 
                    tSelectedItem.transform.localScale = Vector3.one;
                    _flowersSlots.RemoveAt(_tSelectedFlower);
                    break;

                case 2: // GA
                    _tSelectedChallenge = Random.Range(0, _gaChallenges.Count); // selecting random challenge of this type

                    _tSelectedFlower = Random.Range(0, _flowersSlots.Count); // selecting random slot
                    tSelectedItem = Instantiate(_gaChallenges[_tSelectedChallenge]._item1); // instantiate item1
                    Debug.Log(tSelectedItem.name);
                    // re-size
                    tSelectedItem.transform.SetParent(_flowersSlots[_tSelectedFlower]); // setting parent
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0); // new Vector2(left, bottom); 
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0); // new Vector2(right, top);
                    tSelectedItem.transform.GetComponent<RectTransform>().localPosition = Vector3.zero;  // to set z Pos 
                    tSelectedItem.transform.localScale = Vector3.one;
                    _flowersSlots.RemoveAt(_tSelectedFlower); // removing selected slot

                    yield return null;

                    _tSelectedFlower = Random.Range(0, _flowersSlots.Count);
                    tSelectedItem = Instantiate(_gaChallenges[_tSelectedChallenge]._item2); // instantiate item2
                    Debug.Log(tSelectedItem.name);
                    // re-size
                    tSelectedItem.transform.SetParent(_flowersSlots[_tSelectedFlower]); // setting parent
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0); // new Vector2(left, bottom); 
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0); // new Vector2(right, top);
                    tSelectedItem.transform.GetComponent<RectTransform>().localPosition = Vector3.zero;  // to set z Pos 
                    tSelectedItem.transform.localScale = Vector3.one;
                    _flowersSlots.RemoveAt(_tSelectedFlower);
                    break;

                case 3: // LA
                    _tSelectedChallenge = Random.Range(0, _laChallenges.Count); // selecting random challenge of this type

                    _tSelectedFlower = Random.Range(0, _flowersSlots.Count); // selecting random slot
                    tSelectedItem = Instantiate(_laChallenges[_tSelectedChallenge]._item1); // instantiate item1
                    Debug.Log(tSelectedItem.name);
                    // re-size
                    tSelectedItem.transform.SetParent(_flowersSlots[_tSelectedFlower]); // setting parent
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0); // new Vector2(left, bottom); 
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0); // new Vector2(right, top);
                    tSelectedItem.transform.GetComponent<RectTransform>().localPosition = Vector3.zero;  // to set z Pos 
                    tSelectedItem.transform.localScale = Vector3.one;
                    _flowersSlots.RemoveAt(_tSelectedFlower); // removing selected slot

                    yield return null;

                    _tSelectedFlower = Random.Range(0, _flowersSlots.Count);
                    tSelectedItem = Instantiate(_laChallenges[_tSelectedChallenge]._item2); // instantiate item2
                    Debug.Log(tSelectedItem.name);
                    // re-size
                    tSelectedItem.transform.SetParent(_flowersSlots[_tSelectedFlower]); // setting parent
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0); // new Vector2(left, bottom); 
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0); // new Vector2(right, top);
                    tSelectedItem.transform.GetComponent<RectTransform>().localPosition = Vector3.zero;  // to set z Pos 
                    tSelectedItem.transform.localScale = Vector3.one;
                    _flowersSlots.RemoveAt(_tSelectedFlower);
                    break;

                case 4: // LE
                    _tSelectedChallenge = Random.Range(0, _leChallenges.Count); // selecting random challenge of this type

                    _tSelectedFlower = Random.Range(0, _flowersSlots.Count); // selecting random slot
                    tSelectedItem = Instantiate(_leChallenges[_tSelectedChallenge]._item1); // instantiate item1
                    Debug.Log(tSelectedItem.name);
                    // re-size
                    tSelectedItem.transform.SetParent(_flowersSlots[_tSelectedFlower]); // setting parent
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0); // new Vector2(left, bottom); 
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0); // new Vector2(right, top);
                    tSelectedItem.transform.GetComponent<RectTransform>().localPosition = Vector3.zero;  // to set z Pos 
                    tSelectedItem.transform.localScale = Vector3.one;
                    _flowersSlots.RemoveAt(_tSelectedFlower); // removing selected slot

                    yield return null;

                    _tSelectedFlower = Random.Range(0, _flowersSlots.Count);
                    tSelectedItem = Instantiate(_leChallenges[_tSelectedChallenge]._item2); // instantiate item2
                    Debug.Log(tSelectedItem.name);
                    // re-size
                    tSelectedItem.transform.SetParent(_flowersSlots[_tSelectedFlower]); // setting parent
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0); // new Vector2(left, bottom); 
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0); // new Vector2(right, top);
                    tSelectedItem.transform.GetComponent<RectTransform>().localPosition = Vector3.zero;  // to set z Pos 
                    tSelectedItem.transform.localScale = Vector3.one;
                    _flowersSlots.RemoveAt(_tSelectedFlower);
                    break;

                case 5: // MA
                    _tSelectedChallenge = Random.Range(0, _maChallenges.Count); // selecting random challenge of this type

                    _tSelectedFlower = Random.Range(0, _flowersSlots.Count); // selecting random slot
                    tSelectedItem = Instantiate(_maChallenges[_tSelectedChallenge]._item1); // instantiate item1
                    Debug.Log(tSelectedItem.name);
                    // re-size
                    tSelectedItem.transform.SetParent(_flowersSlots[_tSelectedFlower]); // setting parent
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0); // new Vector2(left, bottom); 
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0); // new Vector2(right, top);
                    tSelectedItem.transform.GetComponent<RectTransform>().localPosition = Vector3.zero;  // to set z Pos 
                    tSelectedItem.transform.localScale = Vector3.one;
                    _flowersSlots.RemoveAt(_tSelectedFlower); // removing selected slot

                    yield return null;

                    _tSelectedFlower = Random.Range(0, _flowersSlots.Count);
                    tSelectedItem = Instantiate(_maChallenges[_tSelectedChallenge]._item2); // instantiate item2
                    Debug.Log(tSelectedItem.name);
                    // re-size
                    tSelectedItem.transform.SetParent(_flowersSlots[_tSelectedFlower]); // setting parent
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0); // new Vector2(left, bottom); 
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0); // new Vector2(right, top);
                    tSelectedItem.transform.GetComponent<RectTransform>().localPosition = Vector3.zero;  // to set z Pos 
                    tSelectedItem.transform.localScale = Vector3.one;
                    _flowersSlots.RemoveAt(_tSelectedFlower);
                    break;

                case 6: // MO
                    _tSelectedChallenge = Random.Range(0, _moChallenges.Count); // selecting random challenge of this type

                    _tSelectedFlower = Random.Range(0, _flowersSlots.Count); // selecting random slot
                    tSelectedItem = Instantiate(_moChallenges[_tSelectedChallenge]._item1); // instantiate item1
                    Debug.Log(tSelectedItem.name);
                    // re-size
                    tSelectedItem.transform.SetParent(_flowersSlots[_tSelectedFlower]); // setting parent
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0); // new Vector2(left, bottom); 
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0); // new Vector2(right, top);
                    tSelectedItem.transform.GetComponent<RectTransform>().localPosition = Vector3.zero;  // to set z Pos 
                    tSelectedItem.transform.localScale = Vector3.one;
                    _flowersSlots.RemoveAt(_tSelectedFlower); // removing selected slot

                    yield return null;

                    _tSelectedFlower = Random.Range(0, _flowersSlots.Count);
                    tSelectedItem = Instantiate(_moChallenges[_tSelectedChallenge]._item2); // instantiate item2
                    Debug.Log(tSelectedItem.name);
                    // re-size
                    tSelectedItem.transform.SetParent(_flowersSlots[_tSelectedFlower]); // setting parent
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0); // new Vector2(left, bottom); 
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0); // new Vector2(right, top);
                    tSelectedItem.transform.GetComponent<RectTransform>().localPosition = Vector3.zero;  // to set z Pos 
                    tSelectedItem.transform.localScale = Vector3.one;
                    _flowersSlots.RemoveAt(_tSelectedFlower);
                    break;

                case 7: // O
                    _tSelectedChallenge = Random.Range(0, _oChallenges.Count); // selecting random challenge of this type

                    _tSelectedFlower = Random.Range(0, _flowersSlots.Count); // selecting random slot
                    tSelectedItem = Instantiate(_oChallenges[_tSelectedChallenge]._item1); // instantiate item1
                    Debug.Log(tSelectedItem.name);
                    // re-size
                    tSelectedItem.transform.SetParent(_flowersSlots[_tSelectedFlower]); // setting parent
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0); // new Vector2(left, bottom); 
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0); // new Vector2(right, top);
                    tSelectedItem.transform.GetComponent<RectTransform>().localPosition = Vector3.zero;  // to set z Pos 
                    tSelectedItem.transform.localScale = Vector3.one;
                    _flowersSlots.RemoveAt(_tSelectedFlower); // removing selected slot

                    yield return null;

                    _tSelectedFlower = Random.Range(0, _flowersSlots.Count);
                    tSelectedItem = Instantiate(_oChallenges[_tSelectedChallenge]._item2); // instantiate item2
                    Debug.Log(tSelectedItem.name);
                    // re-size
                    tSelectedItem.transform.SetParent(_flowersSlots[_tSelectedFlower]); // setting parent
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0); // new Vector2(left, bottom); 
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0); // new Vector2(right, top);
                    tSelectedItem.transform.GetComponent<RectTransform>().localPosition = Vector3.zero;  // to set z Pos 
                    tSelectedItem.transform.localScale = Vector3.one;
                    _flowersSlots.RemoveAt(_tSelectedFlower);
                    break;

                case 8: // RA
                    _tSelectedChallenge = Random.Range(0, _raChallenges.Count); // selecting random challenge of this type

                    _tSelectedFlower = Random.Range(0, _flowersSlots.Count); // selecting random slot
                    tSelectedItem = Instantiate(_raChallenges[_tSelectedChallenge]._item1); // instantiate item1
                    Debug.Log(tSelectedItem.name);
                    // re-size
                    tSelectedItem.transform.SetParent(_flowersSlots[_tSelectedFlower]); // setting parent
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0); // new Vector2(left, bottom); 
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0); // new Vector2(right, top);
                    tSelectedItem.transform.GetComponent<RectTransform>().localPosition = Vector3.zero;  // to set z Pos 
                    tSelectedItem.transform.localScale = Vector3.one;
                    _flowersSlots.RemoveAt(_tSelectedFlower); // removing selected slot

                    yield return null;

                    _tSelectedFlower = Random.Range(0, _flowersSlots.Count);
                    tSelectedItem = Instantiate(_raChallenges[_tSelectedChallenge]._item2); // instantiate item2
                    Debug.Log(tSelectedItem.name);
                    // re-size
                    tSelectedItem.transform.SetParent(_flowersSlots[_tSelectedFlower]); // setting parent
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0); // new Vector2(left, bottom); 
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0); // new Vector2(right, top);
                    tSelectedItem.transform.GetComponent<RectTransform>().localPosition = Vector3.zero;  // to set z Pos 
                    tSelectedItem.transform.localScale = Vector3.one;
                    _flowersSlots.RemoveAt(_tSelectedFlower);
                    break;

                case 9: // RE
                    _tSelectedChallenge = Random.Range(0, _reChallenges.Count); // selecting random challenge of this type

                    _tSelectedFlower = Random.Range(0, _flowersSlots.Count); // selecting random slot
                    tSelectedItem = Instantiate(_reChallenges[_tSelectedChallenge]._item1); // instantiate item1
                    Debug.Log(tSelectedItem.name);
                    // re-size
                    tSelectedItem.transform.SetParent(_flowersSlots[_tSelectedFlower]); // setting parent
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0); // new Vector2(left, bottom); 
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0); // new Vector2(right, top);
                    tSelectedItem.transform.GetComponent<RectTransform>().localPosition = Vector3.zero;  // to set z Pos 
                    tSelectedItem.transform.localScale = Vector3.one;
                    _flowersSlots.RemoveAt(_tSelectedFlower); // removing selected slot

                    yield return null;

                    _tSelectedFlower = Random.Range(0, _flowersSlots.Count);
                    tSelectedItem = Instantiate(_reChallenges[_tSelectedChallenge]._item2); // instantiate item2
                    Debug.Log(tSelectedItem.name);
                    // re-size
                    tSelectedItem.transform.SetParent(_flowersSlots[_tSelectedFlower]); // setting parent
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0); // new Vector2(left, bottom); 
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0); // new Vector2(right, top);
                    tSelectedItem.transform.GetComponent<RectTransform>().localPosition = Vector3.zero;  // to set z Pos 
                    tSelectedItem.transform.localScale = Vector3.one;
                    _flowersSlots.RemoveAt(_tSelectedFlower);
                    break;

                case 10: // TE
                    _tSelectedChallenge = Random.Range(0, _teChallenges.Count); // selecting random challenge of this type

                    _tSelectedFlower = Random.Range(0, _flowersSlots.Count); // selecting random slot
                    tSelectedItem = Instantiate(_teChallenges[_tSelectedChallenge]._item1); // instantiate item1
                    Debug.Log(tSelectedItem.name);
                    // re-size
                    tSelectedItem.transform.SetParent(_flowersSlots[_tSelectedFlower]); // setting parent
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0); // new Vector2(left, bottom); 
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0); // new Vector2(right, top);
                    tSelectedItem.transform.GetComponent<RectTransform>().localPosition = Vector3.zero;  // to set z Pos 
                    tSelectedItem.transform.localScale = Vector3.one;
                    _flowersSlots.RemoveAt(_tSelectedFlower); // removing selected slot

                    yield return null;

                    _tSelectedFlower = Random.Range(0, _flowersSlots.Count);
                    tSelectedItem = Instantiate(_teChallenges[_tSelectedChallenge]._item2); // instantiate item2
                    Debug.Log(tSelectedItem.name);
                    // re-size
                    tSelectedItem.transform.SetParent(_flowersSlots[_tSelectedFlower]); // setting parent
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0); // new Vector2(left, bottom); 
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0); // new Vector2(right, top);
                    tSelectedItem.transform.GetComponent<RectTransform>().localPosition = Vector3.zero;  // to set z Pos 
                    tSelectedItem.transform.localScale = Vector3.one;
                    _flowersSlots.RemoveAt(_tSelectedFlower);
                    break;

                case 11: // ZA
                    _tSelectedChallenge = Random.Range(0, _zaChallenges.Count); // selecting random challenge of this type

                    _tSelectedFlower = Random.Range(0, _flowersSlots.Count); // selecting random slot
                    tSelectedItem = Instantiate(_zaChallenges[_tSelectedChallenge]._item1); // instantiate item1
                    Debug.Log(tSelectedItem.name);
                    // re-size
                    tSelectedItem.transform.SetParent(_flowersSlots[_tSelectedFlower]); // setting parent
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0); // new Vector2(left, bottom); 
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0); // new Vector2(right, top);
                    tSelectedItem.transform.GetComponent<RectTransform>().localPosition = Vector3.zero;  // to set z Pos 
                    tSelectedItem.transform.localScale = Vector3.one;
                    _flowersSlots.RemoveAt(_tSelectedFlower); // removing selected slot

                    yield return null;

                    _tSelectedFlower = Random.Range(0, _flowersSlots.Count);
                    tSelectedItem = Instantiate(_zaChallenges[_tSelectedChallenge]._item2); // instantiate item2
                    Debug.Log(tSelectedItem.name);
                    // re-size
                    tSelectedItem.transform.SetParent(_flowersSlots[_tSelectedFlower]); // setting parent
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0); // new Vector2(left, bottom); 
                    tSelectedItem.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0); // new Vector2(right, top);
                    tSelectedItem.transform.GetComponent<RectTransform>().localPosition = Vector3.zero;  // to set z Pos 
                    tSelectedItem.transform.localScale = Vector3.one;
                    _flowersSlots.RemoveAt(_tSelectedFlower);
                    break;
            }
        }
    }

    // restart the options and results
    public void RestartChallenge()
    {
        if (!_win)
        {
            //if (_option1.childCount > 0)
            //{
            //    _instantiatedItems.Remove(_option1.GetChild(0));
            //    Destroy(_option1.GetChild(0).gameObject);
            //}

            //if (_option2.childCount > 0)
            //{
            //    _instantiatedItems.Remove(_option2.GetChild(0));
            //    Destroy(_option2.GetChild(0).gameObject);
            //}

            //if (_option3.childCount > 0)
            //{
            //    _instantiatedItems.Remove(_option3.GetChild(0));
            //    Destroy(_option3.GetChild(0).gameObject);
            //}

            //if (_flower1.transform.childCount > 0)
            //{
            //    _instantiatedItems.Remove(_flower1.transform.GetChild(0));
            //    Destroy(_flower1.transform.GetChild(0).gameObject);
            //}

            //if (_flower2.transform.childCount > 0)
            //{
            //    _instantiatedItems.Remove(_flower1.transform.GetChild(0));
            //    Destroy(_flower2.transform.GetChild(0).gameObject);
            //}

            StartCoroutine(PlayChallenge());
        }        
    }

    // check result of the actual question
    public void CheckResult()
    {
        //if (_readyToPlay)
        //{
        //    if (_flower1.GetItem() != null && _flower2.GetItem() != null) // if both result slots are filled
        //    {
        //        _resultButton.DOPlayBackwards();
        //        _optionsContainer.DOPlayBackwards();
        //        if (_flower1.GetItem().GetItemID() == _flower2.GetItem().GetItemID()) // if the ids of the answers are the same
        //        {
        //            _rabbitAnimator.SetTrigger("Success");
        //            _successCount++;
        //            //_challengesOnThisLevel.RemoveAt(_actualChallenge);
        //            if (_successCount >= _goodAnswersToWin)
        //            {
        //                _win = true;
        //                WinGame();
        //            }
        //            Debug.Log("son iguales");
        //        }
        //        else // if the ids are different
        //        {
        //            _rabbitAnimator.SetTrigger("Fail");
        //            Debug.Log("son diferentes");
        //        }
        //    }
        //    else // if the player doesn't select two items
        //    {
        //        Debug.Log("debe poner dos objetos");
        //    }
        //}        
    }
}
