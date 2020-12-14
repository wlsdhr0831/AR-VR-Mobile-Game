using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUI : MonoBehaviour {

    public GameObject buttonUI;
    private bool check = false;

    private void Start()
    {
        buttonUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCamera" && !check)
        {
            buttonUI.SetActive(true);
            check = true;
        }
    }

    public void clickOK()
    {
        buttonUI.SetActive(false);
    }
}
