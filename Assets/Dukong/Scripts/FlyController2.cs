using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyController2 : MonoBehaviour {
    
    public AdultControl2 ac;

    private void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.CompareTag("Dukong") || other.gameObject.CompareTag("Toy") || other.gameObject.CompareTag("Wall"))
        {
            //부딪히면 할 행동을 설정
            ac.isTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Dukong") || other.gameObject.CompareTag("Toy") || other.gameObject.CompareTag("Wall"))
            ac.isTrigger = false;
    }
}
