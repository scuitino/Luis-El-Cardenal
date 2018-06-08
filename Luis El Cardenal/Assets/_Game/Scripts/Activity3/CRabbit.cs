using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRabbit : MonoBehaviour {

    // to restart when the animation finish
	public void RestartChallenge()
    {
        CActivityManager3._instance.RestartChallenge();
    }

    // to change leaf sprite
    public void EatLeaf()
    {
        CActivityManager3._instance.CutLeaf();
    }
}
