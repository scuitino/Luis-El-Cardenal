using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CChicken : MonoBehaviour {

	public void CallNextChallenge()
    {
        CActivityManager7._instance.NextChallenge();
    }
}
