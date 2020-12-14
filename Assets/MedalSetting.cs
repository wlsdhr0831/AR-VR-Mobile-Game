using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedalSetting : MonoBehaviour {

    public MissionCheck mc;
    public GameObject[] medal = new GameObject[12];
    public GameObject[] none = new GameObject[12];
    
    void Start () {

        Object[] tempList = Resources.FindObjectsOfTypeAll(typeof(GameObject));
        GameObject temp;

        foreach (Object obj in tempList)
        {
            if (obj is GameObject)
            {
                temp = (GameObject)obj;
                if (temp.name == "MissionCheck") mc = temp.GetComponent<MissionCheck>();
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < 12; i++)
        {
            if (mc.mission[i])
            {
                medal[i].SetActive(true);
                none[i].SetActive(false);
            }
            else
            {
                medal[i].SetActive(false);
                none[i].SetActive(true);
            }
        }
	}
}
