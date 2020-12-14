using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene3Manager : MonoBehaviour {
    private AudioSource source;
    public AudioClip doorAppear;
    public AudioClip firework;

    public GameObject[] itemUI = new GameObject[4];    

    public GameObject ARCamera;
    private float height;

    private Animator airplaneAnim;
    private Animator pearlAnim;
    private Animator keyAnim;

    public GameObject ob;
    public GameObject Door;
    private bool st = false;

    private float startTime;
    private float nowTime;

    public Material skyDay;
    public Material skyNight;
    private bool sw = false;

    private int flight = 0;

    public ParticleSystem fw1;
    public ParticleSystem fw2;

    RaycastHit hit;

    List<GameObject> StreetLight = new List<GameObject>();
    public GameObject DayLight;
    public GameObject NightLight;

    private int pearlCount = 0;
    public MissionCheck mc;

    void Start () {
        source = GetComponent<AudioSource>();

        height = ARCamera.transform.position.y;

        Object[] tempList = Resources.FindObjectsOfTypeAll(typeof(GameObject));
        GameObject temp;
        RenderSettings.skybox = skyNight;

        startTime = Time.time;
        
        Door.SetActive(false);
        source.PlayOneShot(doorAppear, 1f);
        StartCoroutine(Next());

        foreach (Object obj in tempList)
        {
            if (obj is GameObject)
            {
                temp = (GameObject)obj;
                if (temp.tag == "StreetLight") StreetLight.Add(temp);
                if (temp.name == "MissionCheck") mc = temp.GetComponent<MissionCheck>();
            }
        }

        for (int i = 0; i < 51; i++) StreetLight[i].SetActive(true);
        NightLight.SetActive(true);
        DayLight.SetActive(false);
    }
	
	void Update () {
        nowTime = Time.time;

        if (nowTime - startTime > 3f && !st)
        {
            Door.SetActive(true);
            st = true;
            if (!fw1.isPlaying) fw1.Play();
            if (!fw2.isPlaying) fw2.Play();
            source.PlayOneShot(firework, 1f);
        }
        
        if (sw)
        {
            RenderSettings.skybox = skyDay;
            NightLight.SetActive(false);
            DayLight.SetActive(true);
            mc.mission[7] = true;
            for (int i = 0; i < 51; i++) StreetLight[i].SetActive(false);
        }

        if (flight == 1)
        {
            ARCamera.transform.position += new Vector3(0f, 5f, 0f);
            mc.mission[9] = true;
        }

        else if(flight == 2)
        {
            ARCamera.transform.position -= new Vector3(0f, 5f, 0f);
            flight = 0;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.tag);
                if (hit.collider.tag == "Key")
                {
                    keyAnim = hit.transform.gameObject.GetComponent<Animator>();

                    if (!fw1.isPlaying) fw1.Play();
                    if (!fw2.isPlaying) fw2.Play();
                    source.PlayOneShot(firework, 1f);
                    mc.mission[8] = true;
                }
                else if (hit.collider.tag == "Pearl")
                {
                    pearlAnim = hit.transform.gameObject.GetComponent<Animator>();
                    pearlAnim.SetBool("start", true);
                    StartCoroutine(Next2());
                    itemUI[pearlCount].SetActive(false);
                    pearlCount++;            
                    itemUI[pearlCount].SetActive(true);
                }
                else if (hit.collider.tag == "Airplane")
                {
                    airplaneAnim = hit.transform.gameObject.GetComponent<Animator>();
                    airplaneAnim.SetBool("start", true);
                    StartCoroutine(Next2());
                    flight = 1;
                    itemUI[0].SetActive(true);
                }
                else if (hit.collider.tag == "RoadDamage")
                {
                    if (!fw1.isPlaying) fw1.Play();
                    if (!fw2.isPlaying) fw2.Play();
                    source.PlayOneShot(firework, 1f);
                }
            }

            if(pearlCount >= 3)
            {
                sw = true;
            }
        }
    }

    IEnumerator Next()
    {
        yield return new WaitForSeconds(.1f);
        ob.SetActive(false);
    }

    IEnumerator Next2()
    {
        yield return new WaitForSeconds(0.7f);
        hit.transform.gameObject.SetActive(false);
    }

    public void airplaneButton()
    {
        flight = 2;
        itemUI[0].SetActive(false);
    }
}
