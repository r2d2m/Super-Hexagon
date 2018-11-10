/*
Copyright (c) Shubham Saudolla
https://github.com/shubham-saudolla
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // whether or not to shift colors
    public bool giveAnEpilepticAttack;

    [HideInInspector]
    public bool rotatePermission = true;

    public float slowDownFactor = 10f;
    public float slowMotionTime = 1f;

    public GameObject gameOverText;
    public GameObject restartText;
    public GameObject scoreText;
    public GameObject topScoreText;

    [HideInInspector]
    public float score = 0f;
    public int increaseEverySecond = 1;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        AudioManager.instance.Play("Start");
        score = 0;
        instance.rotatePermission = true;
        instance.gameOverText.SetActive(false);
        instance.restartText.SetActive(false);
        instance.topScoreText.SetActive(false);
    }

    void Update()
    {
        if (instance.rotatePermission == true)
        {
            score += increaseEverySecond * Time.deltaTime;
            instance.SetScore();
        }
        else
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                RestartLevel();
            }
        }
    }

    public void RevokePermission()
    {
        instance.rotatePermission = false;
    }

    public void GrantPermission()
    {
        instance.rotatePermission = true;
    }

    public void EndLevel()
    {
        StartCoroutine(SlowDownAndStop());
        AudioManager.instance.Play("GameOver");
    }

    private void RestartLevel()
    {
        instance.GrantPermission();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator SlowDownAndStop()
    {
        // before 1 second
        Time.timeScale = 1 / slowDownFactor;
        Time.fixedDeltaTime = Time.fixedDeltaTime / slowDownFactor;

        yield return new WaitForSeconds(slowMotionTime / slowDownFactor);

        // after 1 second
        instance.RevokePermission();
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.fixedDeltaTime * slowDownFactor;

        instance.TopScore();
        instance.gameOverText.SetActive(true);
        instance.restartText.SetActive(true);
    }

    private void SetScore()
    {
        Text dummytext = scoreText.GetComponent<Text>();
        dummytext.text = "Your  Score: " + ((int)score).ToString();
    }

    private void TopScore()
    {
        if ((int)instance.score > PlayerPrefs.GetInt("Top Score", 0))
        {
            PlayerPrefs.SetInt("Top Score", (int)instance.score);
            Text dummyText = topScoreText.GetComponent<Text>();
            dummyText.text = "Top  Score: " + PlayerPrefs.GetInt("Top Score", 0).ToString();
        }
        else
        {
            Text dummyText = topScoreText.GetComponent<Text>();
            dummyText.text = "Top  Score: " + PlayerPrefs.GetInt("Top Score", 0).ToString();
        }
        instance.topScoreText.SetActive(true);
    }
}
