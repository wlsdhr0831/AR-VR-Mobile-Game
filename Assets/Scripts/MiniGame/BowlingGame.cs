using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowlingGame : MonoBehaviour {

    private int gameScore = 0;
    private int gameTime = 0;
    public int win = 0;
    private Bowlingball bm;
    private Pins pm;
    public bool start = false;
    private bool changeGoal = true;
    private bool changeGround = true;
    public GameObject score;
    private bool end = false;

    public Text scoreText;

    public GameObject ball;
    private GameObject gameBall;
    public GameObject pins;
    private GameObject gamePins;

    public Button ui;

    private void Start()
    {
        StartCoroutine(st());
        SetCountText();
        scoreText.text = "0";
    }

    private void Update()
    {
        SetCountText();
        if (gameTime == 5 && !end)
        {
            end = true;
            StartCoroutine(Timedelay2());

            Destroy(gameBall, 5f);
            Destroy(gamePins, 5f);
        }

        if (bm.ground >= 1 && changeGoal && changeGround)
        {
            gameTime++;
            changeGround = false;
            
            Destroy(gameBall, 5f);
            Destroy(gamePins, 5f);

            if (gameTime < 5)
            {
                StartCoroutine(Timedelay());
            }
        }

        else if (bm.ground == 0 && changeGoal && !changeGround)
        {
            changeGround = true;
        }

        if (bm.ground > 0 && changeGoal && changeGround)
        {
            gameScore++;
            changeGoal = false;
        }

        else if (bm.ground == 0 && !changeGoal && changeGround)
        {
            changeGoal = true;
        }
    }

    void OnDestroy()
    {
        ui.enabled = true;
        Destroy(gameBall, 1f);
        Destroy(gamePins, 1f);
    }

    IEnumerator Timedelay()
    {
        yield return new WaitForSeconds(3f);
        score.SetActive(true);
        pm.check = true;
        yield return new WaitForSeconds(0.5f);
        gameScore += pm.score;
        yield return new WaitForSeconds(2.2f);
        score.SetActive(false);

        gameBall = (GameObject)Instantiate(ball, new Vector3(9.711f, 0.825f, 7.741f), Quaternion.identity);
        bm = gameBall.GetComponent<Bowlingball>();
        gamePins = (GameObject)Instantiate(pins, new Vector3(5.39f, 0.72f, 7.87f), Quaternion.identity);
        pm = gamePins.GetComponent<Pins>();
    }

    IEnumerator Timedelay2()
    {
        yield return new WaitForSeconds(3f);
        score.SetActive(true);
        pm.check = true;
        yield return new WaitForSeconds(0.5f);
        gameScore += pm.score;
        yield return new WaitForSeconds(2.2f);
        score.SetActive(false);
        
        if (gameScore >= 25) win = 1;
        else win = 2;
    }

    IEnumerator st()
    {
        yield return new WaitForSeconds(2.5f);

        gameBall = (GameObject)Instantiate(ball, new Vector3(9.711f, 0.825f, 7.741f), Quaternion.identity);
        bm = gameBall.GetComponent<Bowlingball>();
        gamePins = (GameObject)Instantiate(pins, new Vector3(5.39f, 0.72f, 7.87f), Quaternion.identity);
        pm = gamePins.GetComponent<Pins>();
    }

    void SetCountText()
    {
        scoreText.text = gameScore.ToString();
    }
}
