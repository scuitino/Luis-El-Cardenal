using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DoozyUI;

public class CActivity : MonoBehaviour {        

    // to know if the player can play
    public bool _readyToPlay;

    // is the game finished?
    public bool _win;

    // to animate win panel
    [SerializeField]
    UIElement _winPanel;

    // to control win animation
    [SerializeField]
    Animator _winAnimator;

    // to control help animation
    public Animator _helpAnimator;

    // placeholder start flag
    public GameObject _startFlag;

    // skips button
    [SerializeField]
    GameObject _skipButton;

    // to skip replay tutorial
    public GameObject _skipReplayButton;

    // to stop skip replay coroutine
    public Coroutine _stopTalkingCo;

    // audio source to replay tutorial
    public AudioSource _replayTutorialASource;


    // to control when the player can play
    public virtual void ChangeReady(bool aOption)
    {
        _readyToPlay = aOption;
    }

    // call when the player win
    public void WinGame()
    {
        _winPanel.Show(false);
    }

    // to start win animation
    public void StartWinAnimation()
    {
        _winAnimator.SetTrigger("Win");
    }   

    // restart the activity
    public void RestartActivity()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // return to main menu
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void TurnOffSkipButton()
    {
        _skipButton.SetActive(false);
    }
}
