/*
Copyright (c) Shubham Saudolla
https://github.com/shubham-saudolla
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float movementSpeed = 600f;

    float movement = 0f;

    public float slowDownFactor = 10f;
    public float slowMotionTime = 1f;

    void Update()
    {
        movement = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.rotatePermission == true)
        {
            transform.RotateAround(Vector3.zero, Vector3.forward, movement * Time.deltaTime * -movementSpeed);
        }
        else if (GameManager.instance.rotatePermission == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RestartLevel();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(SlowDownAndStop());
    }

    private void RestartLevel()
    {
        GameManager.instance.GrantPermission();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator SlowDownAndStop()
    {
        // before 1 second
        Time.timeScale = 1 / slowDownFactor;
        Time.fixedDeltaTime = Time.fixedDeltaTime / slowDownFactor;

        yield return new WaitForSeconds(slowMotionTime / slowDownFactor);

        GameManager.instance.RevokePermission();
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.fixedDeltaTime * slowDownFactor;

        yield return new WaitForSeconds(0f);
    }
}
