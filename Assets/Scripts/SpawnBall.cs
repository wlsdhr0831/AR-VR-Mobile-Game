using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBall : MonoBehaviour {

    [SerializeField]
    GameObject ball;
	
	
    public void Spawn()
    {
        Instantiate(ball, new Vector3(0f, 0.5f, -12f), Quaternion.identity);
    }
}
