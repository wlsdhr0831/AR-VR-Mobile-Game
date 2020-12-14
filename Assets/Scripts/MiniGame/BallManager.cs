using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{

    public int goal = 0;
    public int ground = 0;
    private bool sw = false;

    Vector2 startPos, endPos, direction;
    float touchTimeStart, touchTimeFinish, timeInterval;

    private float throwForceInZandY = 1.2f;
    private float throwForceInX = 1000f;

    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        if (Input.GetMouseButtonDown(0))
        {
            touchTimeStart = Time.time;
            //startPos = Input.GetTouch(0).position;
            startPos = Input.mousePosition;
            sw = true;
        }

        //if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && sw)
        if (Input.GetMouseButtonUp(0) && sw)
        {
            touchTimeFinish = Time.time;
            timeInterval = touchTimeFinish - touchTimeStart;

            //endPos = Input.GetTouch(0).position;
            endPos = Input.mousePosition;

            direction = startPos - endPos;

            rb.isKinematic = false;
            //rb.AddForce(-direction.x * throwForceInZandY, -direction.y * throwForceInZandY, throwForceInX * timeInterval);
            rb.AddForce(throwForceInX * timeInterval, direction.y * throwForceInZandY, direction.x * throwForceInZandY);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Goal")
        {
            goal = 0;
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
}
