using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CActivity : MonoBehaviour {

    // to know if the player can play
    public bool _readyToPlay;

    // is the game finished?
    public bool _win;

    // win Panel
    [SerializeField]
    GameObject _winPanel;
    
    // to control when the player can play
    public virtual void ChangeReady(bool aOption)
    {
        _readyToPlay = aOption;
    }

    // call when the playe win
    public void WinGame()
    {
        _winPanel.SetActive(true);
    }

    // restart the activity
    public void RestartActivity()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
