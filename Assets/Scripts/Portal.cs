using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Portal : MonoBehaviour
{
    private AudioSource source;
    public AudioClip enter;

    public Material[] materials_outside;
    public Material[] materials_inside;
    public Transform device;

    bool wasInFront;
    bool inOtherWorld;

    bool hasCollided;
    
    void Start()
    {
        source = GetComponent<AudioSource>();
        SetMaterialsIn(false);
        SetMaterialsOut(true);
    }

    void SetMaterialsIn(bool fullRender)
    {
        var stencilTest = fullRender ? CompareFunction.NotEqual : CompareFunction.Equal;

        foreach (var mat in materials_inside)
        {
            mat.SetInt("_StencilTest", (int)stencilTest);
        }
    }

    void SetMaterialsOut(bool fullRender)
    {
        var stencilTest = fullRender ? CompareFunction.NotEqual : CompareFunction.Equal;

        foreach (var mat in materials_outside)
        {
            mat.SetInt("_StencilTest", (int)stencilTest);
        }
    }

    bool GetIsInFront()
    {
        Vector3 worldPos = device.position + device.up * Camera.main.nearClipPlane;

        Vector3 pos = transform.InverseTransformPoint(worldPos);
        
        return pos.y >= 0 ? true : false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform != device) return;

        source.PlayOneShot(enter, 1f);
        wasInFront = GetIsInFront();
        hasCollided = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform != device) return;

        hasCollided = false;
    }

    void WhileCameraColliding()
    {
        if (!hasCollided) return;

        bool isInFront = GetIsInFront();
        if ((isInFront && !wasInFront) || (wasInFront && !isInFront))
        {
            inOtherWorld = !inOtherWorld;
            SetMaterialsIn(inOtherWorld);
            SetMaterialsOut(!inOtherWorld);
        }
        wasInFront = isInFront;
    }

    void OnDestroy()
    {
        SetMaterialsIn(false);
        SetMaterialsOut(true);
    }
    
    void Update()
    {
        WhileCameraColliding();
    }
}
