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

    public bool rotatePermission = true;

    public float slowDownFactor = 10f;
    public float slowMotionTime = 1f;

    public GameObject gameOverText;
    public GameObject restartText;
    public GameObject scoreText;

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
        score = 0;
        instance.rotatePermission = true;
        instance.gameOverText.SetActive(false);
        instance.restartText.SetActive(false);
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

        instance.gameOverText.SetActive(true);
        instance.restartText.SetActive(true);
    }

    private void SetScore()
    {
        Text dummytext = scoreText.GetComponent<Text>();
        dummytext.text = "Score: " + ((int)score).ToString();
    }
}
