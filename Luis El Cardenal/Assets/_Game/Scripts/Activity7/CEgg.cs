using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CEgg : MonoBehaviour {

    // egg state
    bool _isPressed;

    // to modify the egg sprite
    Image _eggImage;

    // egg sprites
    [SerializeField]
    Sprite _selected, _unselected;

    private void Start()
    {
        _eggImage = GetComponent<Image>();
    }

    // when the player touch the egg
    public void PressEgg()
    {
        if (CActivityManager7._instance._readyToPlay)
        {
            if (!_isPressed)
            {
                CActivityManager7._instance.AddWordCount();
                _eggImage.sprite = _selected;
                _isPressed = true;
            }
            else
            {
                CActivityManager7._instance.RemoveWordCount();
                _eggImage.sprite = _unselected;
                _isPressed = false;
            }
        }        
    }
}
