using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChange : MonoBehaviour {
    public Shader shader1;
    public Shader shader2;
    public Renderer rend;
    private float nextStep;
    
	void Start () {
        rend = GetComponent<Renderer>();
        nextStep = 7.0f;
    }
	
	void Update () {
        if (Time.time > nextStep)
        {
            if (rend.material.shader == shader1)
                rend.material.shader = shader2;
        }
	}
}
