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
                CLevelsManager._instance._level1 = true;
                CActivityManager1._instance.GoToMainMenu();
                break;
            case 2:
                CLevelsManager._instance._level2 = true;
                CActivityManager2._instance.GoToMainMenu();
                break;
            case 3:
                CLevelsManager._instance._level3 = true;
                CActivityManager3._instance.GoToMainMenu();
                break;
            case 4:
                CLevelsManager._instance._level4 = true;
                CActivityManager4._instance.GoToMainMenu();
                break;
            case 5:
                CLevelsManager._instance._level5 = true;
                CActivityManager5._instance.GoToMainMenu();
                break;
            case 6:
                CLevelsManager._instance._level6 = true;
                CActivityManager6._instance.GoToMainMenu();
                break;
            case 7:
                CLevelsManager._instance._level7 = true;
                CActivityManager7._instance.GoToMainMenu();
                break;
            case 8:
                CLevelsManager._instance._level8 = true;
                CActivityManager8._instance.GoToMainMenu();
                break;
        }
    }
}
