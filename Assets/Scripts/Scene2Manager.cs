using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene2Manager : MonoBehaviour
{
    private AudioSource source;
    public AudioClip minigameEnd;

    private Animator buttonAnim;

    public GameObject cameraControl;
    public GameObject walkControl1;
    public GameObject walkControl2;
    public GameObject welcome;

    private int[] count = new int[3] { 0, 0, 0 };
    public GameObject miniGame;
    public GameObject MinigameController;
    public GameObject TurnAround;
    public Transform mainCamera;
    private GameObject TurnAroundClone;
    private MiniGameManager mn;
    private Transform temp;
    private List<Transform> talk = new List<Transform>();

    private bool toyMove = false;
    private Vector2 startPos, endPos, direction;
    RaycastHit hit;

    private int text;
    private int textCount = -1;

    public GameObject Scene3;

    List<GameObject> realList = new List<GameObject>();

    public MissionCheck mc;

    private void Start()
    {
        welcome.SetActive(true);
        source = GetComponent<AudioSource>();
        Object[] tempList = Resources.FindObjectsOfTypeAll(typeof(GameObject));
        GameObject temp;
        Scene3.SetActive(false);
        miniGame.SetActive(false);

        cameraControl.SetActive(false);
        walkControl1.SetActive(true);
        walkControl2.SetActive(false);

        mainCamera.position += new Vector3(0f, 0.3f, 0f);

        foreach (Object obj in tempList)
        {
            if (obj is GameObject)
            {
                temp = (GameObject)obj;
                if (temp.tag == "AdultDukong" || temp.tag == "KidDukong") realList.Add(temp);
                if (temp.name == "MissionCheck") mc = temp.GetComponent<MissionCheck>();
            }
        }
    }

    void Update()
    {
        if(Time.time > 600)
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetMouseButtonDown(0))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "AdultDukong")
                {
                    count[0]++;
                    StartCoroutine(Talk(hit));
                }
                else if (hit.collider.tag == "KidDukong")
                {
                    count[1]++;
                    StartCoroutine(Talk(hit));
                }
                else if (hit.collider.tag == "Toy")
                {
                    count[2]++;
                    startPos = Input.mousePosition;
                    toyMove = true;
                }
                else if(hit.collider.tag == "Button")
                {
                    mc.mission[11] = true;
                    buttonAnim = hit.transform.gameObject.GetComponent<Animator>();
                    StartCoroutine(Next());
                }
            }
        }

        else if (Input.GetMouseButtonUp(0) && toyMove) toyMove = false;

        else if (toyMove)
        {
            endPos = Input.mousePosition;
            direction = endPos - startPos;

            hit.transform.position += new Vector3(direction.x, 0, direction.y) / 30;

            startPos = Input.mousePosition;
        }

        for (int i = 0; i < 3; i++)
        {
            if (count[i] == 3)
            {
                mn = MinigameController.GetComponent<MiniGameManager>();
                MinigameController.SetActive(true);
                mn.start[i] = 1;
                count[i]++;
                mc.mission[i] = true;
            }

            if (count[i] > 3)
            {
                if (mn.win[i] == 1 && mn.start[i] == 2)    // 게임 끝나고 이긴거
                {
                    source.PlayOneShot(minigameEnd, 1f);
                    mn.start[i] = 0;
                    mc.mission[i+3] = true;
                    Vector3 campos = new Vector3(mainCamera.position.x, 0.7424f, mainCamera.position.z );
                    TurnAroundClone = (GameObject)Instantiate(TurnAround, campos, Quaternion.identity);

                    StartCoroutine(Des(TurnAroundClone));
                }
                else if (mn.win[i] == 2)    // 게임 끝나고 진거
                {
                    source.PlayOneShot(minigameEnd, 1f);
                    foreach (GameObject gob in realList)
                    {
                        gob.SetActive(true);
                    }
                    mn.start[i] = 0;
                }
            }
        }
    }

    IEnumerator Next()
    {
        yield return new WaitForSeconds(0.3f);

        Scene3.SetActive(true);
        buttonAnim.SetBool("start", true);

        yield return new WaitForSeconds(1f);
        hit.transform.gameObject.SetActive(false);
    }

    IEnumerator Des(GameObject a)
    {
        yield return new WaitForSeconds(10f);

        Destroy(a);
    }

    IEnumerator Talk(RaycastHit hit)
    {
        textCount++;
        text = (int)Random.Range(1.0f, 9.9f);

        temp = hit.transform.parent;
        talk.Add(temp.GetChild(text));

        int index = textCount;

        talk[index].gameObject.SetActive(true);
        
        yield return new WaitForSeconds(5f);

        if(index > textCount)
        {
            index = index - (index - textCount);
        }

        talk[index].gameObject.SetActive(false);
        talk.RemoveAt(index);

        textCount--;
    }

    public void walk()
    {
        cameraControl.SetActive(true);
        walkControl1.SetActive(false);
        walkControl2.SetActive(true);
    }

    public void cont()
    {
        cameraControl.SetActive(false);
        walkControl1.SetActive(true);
        walkControl2.SetActive(false);
    }
}
