using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour {

    public Animator m;
    public KidControl kc;
    public int priority = 1;

    void Start ()
    {
        m = GetComponent<Animator>();
    }
	
	void Update ()
    {
        if (kc.jump)
        {
            m.SetBool("jump", true);
        }
        else
        {
            m.SetBool("jump", false);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Dukong") || other.gameObject.CompareTag("Toy") || other.gameObject.CompareTag("Wall"))
        {
            kc.isTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Dukong") || other.gameObject.CompareTag("Toy") || other.gameObject.CompareTag("Wall"))
        {
            kc.isTrigger = false;
        }
    }
}
