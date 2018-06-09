using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDNDAudiosSource : MonoBehaviour {

    #region SINGLETON PATTERN
    public static CDNDAudiosSource _instance = null; //static - the same variable is shared by all instances of the class that are created
    #endregion

    private void Awake()
    {
        //Singleton check
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);
    }
}
