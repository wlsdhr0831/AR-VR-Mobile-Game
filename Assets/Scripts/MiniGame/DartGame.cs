using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class DartGame : MonoBehaviour {

    public int gameScore = 0;
    private int gameTime = 0;
    public int win = 0;
    private DartArrow bm;
    private DartArrow temp;
    private bool changeGoal = true;
    private bool changeGround = true;

    public Text scoreText;

    private bool endCal = false;

    public GameObject ball;
    private GameObject[] gameBall = new GameObject[5];

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
        if (gameTime == 5)
        {
            if (!endCal)
            {
                StartCoroutine(End());
                endCal = true;
            }

            for (int i = 0; i < 5; i++)
            {
                Destroy(gameBall[i], 2f);
            }
        }

        if (bm.ground >= 1 && changeGoal && changeGround)
        {
            gameTime++;
            changeGround = false;
            
            if (gameTime < 5)
            {
                temp = bm;
                StartCoroutine(Next());

                gameBall[gameTime] = (GameObject)Instantiate(ball, new Vector3(9.711f, 1.5f, 7.741f), Quaternion.identity);
                bm = gameBall[gameTime].GetComponent<DartArrow>();
            }
        }

        else if (bm.ground == 0 && changeGoal && !changeGround)
        {
            changeGround = true;
        }
    }

    void OnDestroy()
    {
        ui.enabled = true;
        for(int i = 0; i<=gameTime; i++)
            Destroy(gameBall[i], 1f);
    }

    IEnumerator Next()
    {
        yield return new WaitForSeconds(0.3f);

        gameScore += temp.score;
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(0.3f);

        gameScore += bm.score;

        if (gameScore >= 25) win = 1;
        else win = 2;

        yield return new WaitForSeconds(1f);

        gameScore = 0;
    }

    IEnumerator st()
    {
        yield return new WaitForSeconds(2.5f);

        gameBall[0] = (GameObject)Instantiate(ball, new Vector3(9.711f, 1.5f, 7.741f), new Quaternion(0, 0, 0, 0));
        bm = gameBall[0].GetComponent<DartArrow>();
    }

    void SetCountText()
    {
        scoreText.text = gameScore.ToString();
    }
}
