using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pins : MonoBehaviour {
    public GameObject[] pin = new GameObject[10];
    public bool check = false;

    private int count = 0;
    public int score = 0;

    private Pin[] stay = new Pin[10];

    private void Start()
    {
        for(int i = 0;i < 10; i++)
        {
            stay[i] = pin[i].GetComponent<Pin>();
        }
    }

    void Update () {
        if (check)
        {
            for (int i = 0; i < 10; i++)
            {
                if (stay[i].sw)
                {
                    count++;
                }
            }
            check = false;
        }
        else
        {
            score = 10 - count;
        }
	}
}
