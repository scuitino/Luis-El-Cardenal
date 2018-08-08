using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMolino : MonoBehaviour {

    // called from animation
	public void NextChallenge()
    {
        StartCoroutine(CActivityManager8._instance.NextChallenge());
    }
}
