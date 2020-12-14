using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUI2 : MonoBehaviour {

    public GameObject buttonUI;
    public GameObject tellButtonUI;
    private bool check = false;

    private void Start()
    {
        buttonUI.SetActive(false);
        tellButtonUI.SetActive(false);
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
        tellButtonUI.SetActive(true);
    }
}
