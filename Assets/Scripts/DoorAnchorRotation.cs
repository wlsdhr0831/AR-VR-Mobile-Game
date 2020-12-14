using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnchorRotation : MonoBehaviour {

    Animator anim;
    public GameObject window;

	void Start () {
        anim = GetComponent<Animator>();
        window.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MainCamera")
        {
            if (Time.time > 7f)
            {
                anim.SetTrigger("OpenDoor");
                window.SetActive(true);
            }
        }
    }

    void pauseAnimationEvent()
    {
        anim.enabled = false;
    }
}