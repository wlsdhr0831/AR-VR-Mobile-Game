using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effect : MonoBehaviour {
    
    public GameObject stage;

	void Start () {
        stage.SetActive(false);
        StartCoroutine(Next());
    }

    IEnumerator Next()
    {
        yield return new WaitForSeconds(2.5f);

        stage.SetActive(true);
    }

}
