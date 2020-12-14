using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class tutorialManager : MonoBehaviour {

    public GameObject button1, button2, button3, button4, button5, button6, button7, button81, button82, button9;
    public GameObject walk, control, conButton;
    public GameObject buttonEffect, nextstage;
    public Button walk2, control2;
    private int walkbuttonCnt = 0;
    private int contbuttonCnt = 0;
    private bool effect = false;

    public GameObject[] itemUI = new GameObject[4];
    public GameObject ARCamera;
    public Collider ARCameraCol;
    private float height;

    private Animator airplaneAnim;
    private Animator pearlAnim;
    
    public Material skyDay;
    public Material skyNight;
    public GameObject ef;
    private bool sw = false;
    
    private int flight = 0;
    RaycastHit hit;
    
    private int pearlCount = 0;

    public MissionCheck mc;

    // Use this for initialization
    void Start () {
        button1.SetActive(true);
        button2.SetActive(false);
        button3.SetActive(false);
        button4.SetActive(false);
        button5.SetActive(false);
        button6.SetActive(false);
        button7.SetActive(false);
        button81.SetActive(false);
        button82.SetActive(false);
        button9.SetActive(false);
        walk.SetActive(true);
        control.SetActive(false);
        conButton.SetActive(false);
        buttonEffect.SetActive(false);
        nextstage.SetActive(false);
        
        walk2.enabled = false;
        control2.enabled = false;
        ef.SetActive(true);
        
        RenderSettings.skybox = skyNight;
    }

    private void Update()
    {
        if (sw)
        {
            RenderSettings.skybox = skyDay;
            ef.SetActive(false);

            mc.mission[10] = true;
        }

        if (flight == 1)
        {
            height = ARCamera.transform.position.y;
            if (ARCamera.transform.position.y < 10f)
                ARCamera.transform.position += new Vector3(0f, 0.1f, 0f);
            ARCameraCol.attachedRigidbody.useGravity = false;

            mc.mission[6] = true;
        }

        else if (flight == 2)
        {
             flight = 0;
             ARCameraCol.attachedRigidbody.useGravity = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.tag);
                if (hit.collider.tag == "Pearl")
                {
                    pearlAnim = hit.transform.gameObject.GetComponent<Animator>();
                    pearlAnim.SetBool("start", true);
                    StartCoroutine(Next2());
                    itemUI[pearlCount].SetActive(false);
                    pearlCount++;
                    itemUI[pearlCount].SetActive(true);
                }
                else if (hit.collider.tag == "AirPlane")
                {
                    airplaneAnim = hit.transform.gameObject.GetComponent<Animator>();
                    airplaneAnim.SetBool("start", true);
                    StartCoroutine(Next2());
                    flight = 1;
                    itemUI[0].SetActive(true);
                }
            }

            if (pearlCount >= 3)
            {
                sw = true;
            }
        }
    }


    public void button1cick()
    {
        button2.SetActive(true);
        button1.SetActive(false);
        walk2.enabled = true;
        StartCoroutine(Effect());
    }

    public void button3cick()
    {
        button4.SetActive(true);
        button3.SetActive(false);
    }

    public void button4cick()
    {
        StartCoroutine(Effect());
        control2.enabled = true;
        button5.SetActive(true);
        button4.SetActive(false);
    }

    public void button6cick()
    {
        button7.SetActive(true);
        button6.SetActive(false);
        button81.SetActive(true);
        button82.SetActive(true);
    }

    public void button7cick()
    {
        button9.SetActive(true);
        button7.SetActive(false);
        button81.SetActive(false);
        button82.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void button8cick()
    {
        button7.SetActive(false);
        button81.SetActive(false);
        button82.SetActive(false);
        nextstage.SetActive(true);
    }

    public void buttonNext()
    {
        button9.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void walkclick()
    {
        walkbuttonCnt++;
        if(walkbuttonCnt == 1)
        {
            buttonEffect.SetActive(false);
            button2.SetActive(false);
            button3.SetActive(true);
        }
        conButton.SetActive(true);
        walk.SetActive(false);
        control.SetActive(true);
    }

    public void contclick()
    {
        contbuttonCnt++;
        if (contbuttonCnt == 1)
        {
            button5.SetActive(false);
            button6.SetActive(true);
            buttonEffect.SetActive(false);
        }
        conButton.SetActive(false);
        walk.SetActive(true);
        control.SetActive(false);
    }

    IEnumerator Effect()
    {
        if (!effect) {
            buttonEffect.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            buttonEffect.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            buttonEffect.SetActive(true);
        }
    }

    public void airplaneButton()
    {
        flight = 2;
        itemUI[0].SetActive(false);
    }

    IEnumerator Next2()
    {
        yield return new WaitForSeconds(0.7f);
        hit.transform.gameObject.SetActive(false);
    }
}
