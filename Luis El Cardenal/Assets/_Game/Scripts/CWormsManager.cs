using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWormsManager : MonoBehaviour {

    #region SINGLETON PATTERN
    public static CWormsManager _instance = null;
    #endregion

    private void Awake()
    {
        //Singleton check
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);
    }

    // collect a worm
    public void Collect()
    {
        this.GetComponent<Animator>().SetTrigger("Collect");
    }
}
