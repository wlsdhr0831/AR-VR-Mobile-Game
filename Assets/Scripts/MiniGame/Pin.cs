using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {
    public bool sw = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Score" && !sw) {
            sw = true;
        }
    }
}
