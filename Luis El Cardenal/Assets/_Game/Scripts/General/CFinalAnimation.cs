using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFinalAnimation : MonoBehaviour {

    [SerializeField]
    int _activityNumber;

	public void RestartGame()
    {
        switch (_activityNumber)
        {
            case 1:
                CActivityManager1._instance.RestartActivity();
                break;
            case 2:
                CActivityManager2._instance.RestartActivity();
                break;
            case 3:
                CActivityManager3._instance.RestartActivity();
                break;
            case 4:
                CActivityManager4._instance.RestartActivity();
                break;
            case 5:
                CActivityManager5._instance.RestartActivity();
                break;
            case 6:
                CActivityManager6._instance.RestartActivity();
                break;
            case 7:
                CActivityManager7._instance.RestartActivity();
                break;
            case 8:
                //CActivityManager8._instance.RestartActivity();
                break;
        }
    }
}
