using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFrog : MonoBehaviour {

    // notify the manager that the jump animation is finished
    public void LastJumpCallBack()
    {
        CActivityManager2._instance.StartSyllablesAnimation();
    }

    // notify the manager that the fail animation is finished
    public void FailJumpCallBack()
    {
        CActivityManager2._instance.StartFailAnimation();
    }
}
