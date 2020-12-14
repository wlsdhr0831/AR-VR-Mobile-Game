using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowlingball : MonoBehaviour {
    private AudioSource source;
    public AudioClip ballSound;

    private bool sw = false;
    public int ground = 0;

    Vector2 startPos, endPos, direction;
    float touchTimeStart, touchTimeFinish, timeInterval;

    private float throwForceInZ = 1.2f;
    private float throwForceInX = 3000f;

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
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "BowlingBall")
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
            rb.AddForce(-throwForceInX * timeInterval,0, -direction.x * throwForceInZ);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Ground" && sw)
        {
            ground++;
        }

        if (collision.gameObject.tag == "Pins")
        {
            source.PlayOneShot(ballSound, 0.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Wall")
        {
            ground++;
        }
    }
}
