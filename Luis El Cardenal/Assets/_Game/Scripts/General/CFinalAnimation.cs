using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CFinalAnimation : MonoBehaviour {

    [SerializeField]
    int _activityNumber;

	public void RestartGame()
    {               
        switch (_activityNumber)
        {
            case 1:
                CLevelsManager._instance._level1 = true;                
                break;
            case 2:
                CLevelsManager._instance._level2 = true;
                break;
            case 3:
                CLevelsManager._instance._level3 = true;
                break;
            case 4:
                CLevelsManager._instance._level4 = true;
                break;
            case 5:
                CLevelsManager._instance._level5 = true;
                break;
            case 6:
                CLevelsManager._instance._level6 = true;
                break;
            case 7:
                CLevelsManager._instance._level7 = true;
                break;
            case 8:
                CLevelsManager._instance._level8 = true;
                break;
        }
        CheckIfIsTheLast();
    }

    public void CheckIfIsTheLast()
    {
        bool tIsAllComplete = true;

        // check if all levels are completed
        if (CLevelsManager._instance._level1 != true)
        {
            tIsAllComplete = false;
        }
        if (CLevelsManager._instance._level2 != true)
        {
            tIsAllComplete = false;
        }
        if (CLevelsManager._instance._level3 != true)
        {
            tIsAllComplete = false;
        }
        if (CLevelsManager._instance._level4 != true)
        {
            tIsAllComplete = false;
        }
        if (CLevelsManager._instance._level5 != true)
        {
            tIsAllComplete = false;
        }
        if (CLevelsManager._instance._level6 != true)
        {
            tIsAllComplete = false;
        }
        if (CLevelsManager._instance._level7 != true)
        {
            tIsAllComplete = false;
        }
        if (CLevelsManager._instance._level8 != true)
        {
            tIsAllComplete = false;
        }

        // not all levels are completed
        if (!tIsAllComplete)
        {
            CLevelsManager._instance.GoToMainMenu();
        }
        else // all levels are completed
        {
            if (!CLevelsManager._instance._congratulation) // first time after win all levels
            {
                CLevelsManager._instance._congratulation = true;
                SceneManager.LoadScene(10);
            }
            else
            {
                CActivityManager8._instance.GoToMainMenu();
            }
        }
    }
}
