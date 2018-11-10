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

    private float _movement = 0f;

    void Update()
    {
        _movement = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.rotatePermission == true)
        {
            transform.RotateAround(Vector3.zero, Vector3.forward, _movement * Time.deltaTime * -movementSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.instance.EndLevel();
    }
}
