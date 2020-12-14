using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartArrow : MonoBehaviour {

    private AudioSource source;
    public AudioClip dart;

    public GameObject effect;

    public int score = 0;
    public int ground = 0;
    private bool sw = false;

    public GameObject endArrow;

    Vector2 startPos, endPos, direction;
    float touchTimeStart, touchTimeFinish, timeInterval;

    private float throwForceInZ = 0.5f;
    private float throwForceInY = 0.5f;
    private float throwForceInX = 2000f;

    private Rigidbody rb;

    void Start()
    {
        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        effect.SetActive(true);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "DartArrow")
                {
                    touchTimeStart = Time.time;
                    startPos = Input.mousePosition;
                    sw = true;
                    effect.SetActive(false);
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ground")
        {
            ground++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Dart")
        {
            source.PlayOneShot(dart, 1f);
            rb.constraints = RigidbodyConstraints.FreezeAll;
            
            Vector2 a = new Vector2(16.332f - endArrow.transform.position.x,
                        2.388f - endArrow.transform.position.y);
            score = (int)((1/(a.x * a.x + a.y * a.y)));

            if (score > 1000) score = 10;
            else if (score > 500) score = 9;
            else if (score > 100) score = 8;
            else if (score >= 10) score = 7;
            else if (score < 10) score -= 3;

            if (score < 0) score = 0;
            
            ground++;
        }
    }
}
