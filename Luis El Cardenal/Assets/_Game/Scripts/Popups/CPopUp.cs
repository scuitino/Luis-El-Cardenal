using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPopUp : MonoBehaviour {

    [SerializeField]
    List<GameObject> _popUpButtons;

    public void EnableButtons()
    {
        this.GetComponent<Animator>().SetBool("Active",true);
        foreach (GameObject tButton in _popUpButtons)
        {
            tButton.SetActive(true);
        }
    }

    public void DisableButtons()
    {
        this.GetComponent<Animator>().SetBool("Active", false);
        foreach (GameObject tButton in _popUpButtons)
        {
            tButton.SetActive(false);
        }
    }
}
