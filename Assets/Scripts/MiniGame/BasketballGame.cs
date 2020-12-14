using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasketballGame : MonoBehaviour {

    private int gameScore = 0;
    private int gameTime = 0;
    public int win = 0;
    private Basketball bm;
    public bool start = false;
    private bool changeGoal = true;
    private bool changeGround = true;
    
    public Text scoreText;

    public GameObject ball;
    private GameObject gameBall;

    public Button ui;

    private void Start()
    {
        StartCoroutine(Next());
        SetCountText();
        scoreText.text = "0";
    }

    private void Update()
    {
        SetCountText();
        if (gameTime == 5)
        {
            if (gameScore >= 1) win = 1;
            else win = 2;

            Destroy(gameBall, 2f);
        }

        if (bm.ground >= 1 && changeGoal && changeGround)
        {
            gameTime++;
            changeGround = false;

            Destroy(gameBall, 1.5f);

            if (gameTime < 5)
            {
                gameBall = (GameObject)Instantiate(ball, new Vector3(9.711f, 0.825f, 7.741f), Quaternion.identity);
                bm = gameBall.GetComponent<Basketball>();
            }
        }

        else if (bm.ground == 0 && changeGoal && !changeGround)
        {
            changeGround = true;
        }

        if (bm.goal >= 1 && changeGoal && changeGround)
        {
            gameScore++;
            changeGoal = false;
        }

        else if (bm.goal == 0 && !changeGoal && changeGround)
        {
            changeGoal = true;
        }
    }

    void OnDestroy()
    {
        ui.enabled = true;
        Destroy(gameBall, 1f);
    }

    IEnumerator Next()
    {
        yield return new WaitForSeconds(2.5f);

        gameBall = (GameObject)Instantiate(ball, new Vector3(9.711f, 0.825f, 7.741f), Quaternion.identity);
        bm = gameBall.GetComponent<Basketball>();
    }

    void SetCountText()
    {
        scoreText.text = gameScore.ToString();
    }
}