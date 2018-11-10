/*
Copyright (c) Shubham Saudolla
https://github.com/shubham-saudolla
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool rotatePermission = true;

    public float slowDownFactor = 10f;
    public float slowMotionTime = 1f;

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
        instance.rotatePermission = true;
    }

    void Update()
    {
        if (instance.rotatePermission == false)
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
    }
}
