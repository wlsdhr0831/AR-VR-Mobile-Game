using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dissolve : MonoBehaviour {

    public Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > 1f) anim.SetBool("start", true);
    }
}

