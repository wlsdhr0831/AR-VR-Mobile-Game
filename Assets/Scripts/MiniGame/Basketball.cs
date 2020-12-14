using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basketball : MonoBehaviour {
    RaycastHit hit;
    private AudioSource source;
    public AudioClip ballSound;

    public int goal = 0;
    public int ground = 0;
    private bool sw = false;

    Vector2 startPos, endPos, direction;
    float touchTimeStart, touchTimeFinish, timeInterval;

    private float throwForceInZ = 1f;
    private float throwForceInY = 1f;
    private float throwForceInX = 1000f;

    private Rigidbody rb;
    
    void Start()
    {
        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "BasketBall")
                {
                    touchTimeStart = Time.time;
                    startPos = Input.mousePosition;
                    sw = true;
                }
            }
        }
        
        if (Input.GetMouseButtonUp(0) && sw)
        {
            touchTimeFinish = Time.time;
            timeInterval = touchTimeFinish - touchTimeStart;
            
            endPos = Input.mousePosition;

            direction = startPos - endPos;

            rb.isKinematic = false;
            rb.AddForce(-throwForceInX * timeInterval, -direction.y * throwForceInY, -direction.x * throwForceInZ);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Goal")
        {
            goal = 0;
        }

        if (collider.gameObject.tag == "Ground" && sw)
        {
            ground++;
        }

    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Goal")
        {
            goal++;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" && sw)
        {
            ground = 0;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" && sw)
        {
            ground++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" && sw)
        {
            source.PlayOneShot(ballSound, 1f);
        }
    }
}
