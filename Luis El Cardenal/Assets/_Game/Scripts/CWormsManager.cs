using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWormsManager : MonoBehaviour {

    #region SINGLETON PATTERN
    public static CWormsManager _instance = null;
    #endregion

    private void Start()
    {
        //Replace Singleton
        _instance = this;
    }

    // collect a worm
    public void Collect()
    {
        this.GetComponent<Animator>().SetTrigger("Collect");
    }
}
