using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdultControl2 : MonoBehaviour {

    public float moveSpeed = 3f;
    public float rotSpeed = 100f;
    public float wingRotSpeed = 1000f;

    public Transform wing;

    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;
    public bool isTrigger = false;

    private void Start()
    {

    }

    private void Update()
    {
        if (isTrigger)
        {
            isWalking = false;
            isWandering = false;
        }
        if (!isWandering)
        {
            StartCoroutine(Wander());
            isTrigger = false;
        }
        if (isRotatingRight)
        {
            transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
        }
        if (isRotatingLeft)
        {
            transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
        }
        if (isWalking)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }

        wing.transform.Rotate(transform.up * Time.deltaTime * wingRotSpeed);
    }

    IEnumerator Wander()
    {
        int rotTime = Random.Range(1, 3);
        int rotateWait = Random.Range(1, 4);
        int rotateLorR = Random.Range(1, 2);
        int walkWait = Random.Range(1, 4);
        int walkTime = Random.Range(1, 5);

        isWandering = true;

        yield return new WaitForSeconds(walkWait);
        isWalking = true;
        yield return new WaitForSeconds(walkTime);
        isWalking = false;
        yield return new WaitForSeconds(rotateWait);
        if (rotateLorR == 1)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingRight = false;
        }
        if (rotateLorR == 2)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingLeft = false;
        }
        isWandering = false;
    }
}
