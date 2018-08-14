using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CAnswer : MonoBehaviour {

    // true when is the correct answer
    public bool _correctAnswer;

    public Text _text;

    public Button _button;
    
    // config the answer
    public void SetAnswer(bool aIsCorrect, string aText)
    {
        _correctAnswer = aIsCorrect;
        _text.text = aText;
    }

    // check result
    public void CheckResult()
    {
        if (_correctAnswer)
        {
            CActivityManager8._instance.CorrectAnswer();
        }
        else
        {
            CActivityManager8._instance.IncorrectAnswer();
        }
        
    }

    public void EnableButton()
    {
        _button.enabled = true;
    }

    public void DisableButton()
    {
        _button.enabled = false;
    }    
}
