using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CEgg : MonoBehaviour {

    // egg state
    public bool _isPressed;

    // to modify the egg animations
    public Animator _eggAnimator;

    // egg sprites
    [SerializeField]
    Sprite _selected, _unselected;

    private void Start()
    {
        _eggAnimator = GetComponent<Animator>();
    }

    // when the player touch the egg
    public void PressEgg()
    {
        if (CActivityManager7._instance._readyToPlay)
        {
            if (!_isPressed)
            {
                CActivityManager7._instance.AddWordCount();
                _eggAnimator.SetTrigger("Press");
                _isPressed = true;
            }
            else
            {
                CActivityManager7._instance.RemoveWordCount();
                _eggAnimator.SetTrigger("Unpress");
                _isPressed = false;
            }
        }        
    }
}
