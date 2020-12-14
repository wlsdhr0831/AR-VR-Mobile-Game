using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    private AudioSource source;
    public AudioClip minigameStart;
    
    public GameObject[] minigameUI = new GameObject[3];
    public Button[] minigameUIE = new Button[3]
;
    public int[] win = new int[] { 0, 0, 0 };
    public int[] start = new int[3] { 0,0,0 };
    public GameObject[] gameStage = new GameObject[3];
    private GameObject[] gameClone = new GameObject[3];

    private BasketballGame basketball;
    private BowlingGame bowling;
    private DartGame dart;

    public GameObject dukongsPrefabs;
    private GameObject dukongs;
    
    private int nowPlaying = 4;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        for(int i = 0; i < 3; i++)  minigameUI[i].SetActive(false);
        dukongs = (GameObject)Instantiate(dukongsPrefabs, new Vector3(6.2f, 0.721f, 7.86f), Quaternion.identity);
        dukongs.SetActive(false);
    }

    private void Update()
    {
        if (start[0] == 2) win[0] = basketball.win;
        if (start[1] == 2) win[1] = bowling.win;
        if (start[2] == 2) win[2] = dart.win;

        for (int i = 0; i < 3; i++)
        {
            if (win[i] != 0)
            {
                Destroy(gameClone[i], 2f);
                StartCoroutine(End(i));
            }

            if (start[i] == 1)
            {
                minigameUI[i].SetActive(true);
            }
        }

        if(start[0] + start[1] + start[2] == 0)
        {
            dukongs.SetActive(false);
            nowPlaying = 4;
            Debug.Log("두콩이가 사라집니다");
        }
    }

    IEnumerator End(int i)
    {
        yield return new WaitForSeconds(1f);
        win[i] = 0;
    }

    IEnumerator DestroyGame(int i)
    {
        yield return new WaitForSeconds(0.5f);
        if (i != 4) {
            Destroy(gameClone[i]);
            start[i] = 0;
        }


        minigameUIE[i].enabled = true;
    }

    public void basketballStart()
    {
        minigameUIE[0].enabled = false;
        source.PlayOneShot(minigameStart, 1f);
        StartCoroutine(DestroyGame(nowPlaying));
        nowPlaying = 0;

        gameClone[0] = (GameObject)Instantiate(gameStage[0], new Vector3(9.55f, 0.721f, 7.85f), Quaternion.identity);
        basketball = gameClone[0].GetComponent<BasketballGame>();
        start[0] = 2;
        dukongs.SetActive(true);
    }

    public void bowlingStart()
    {
        minigameUIE[1].enabled = false;
        source.PlayOneShot(minigameStart, 1f);
        StartCoroutine(DestroyGame(nowPlaying));
        nowPlaying = 1;

        gameClone[1] = (GameObject)Instantiate(gameStage[1], new Vector3(9.59f, 0.721f, 7.83f), new Quaternion(0, 180, 0, 0));
        bowling = gameClone[1].GetComponent<BowlingGame>();
        start[1] = 2;
        dukongs.SetActive(true);
    }

    public void dartStart()
    {
        minigameUIE[2].enabled = false;
        source.PlayOneShot(minigameStart, 1f);
        StartCoroutine(DestroyGame(nowPlaying));
        nowPlaying = 2;

        gameClone[2] = (GameObject)Instantiate(gameStage[2], new Vector3(9.53f, 0.721f, 7.85f), new Quaternion(0, 180, 0, 0));
        dart = gameClone[2].GetComponent<DartGame>();
        start[2] = 2;
        dukongs.SetActive(true);
    }
}