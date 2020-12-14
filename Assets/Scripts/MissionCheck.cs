using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionCheck : MonoBehaviour {

    public bool[] mission = new bool[12];
    private static bool created = false;

    private void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
    }

    void Start()
    {
        for (int i = 0; i < 12; i++) mission[i] = false;
    }
}
