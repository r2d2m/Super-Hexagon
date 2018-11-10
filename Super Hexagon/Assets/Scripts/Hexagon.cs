/*
Copyright (c) Shubham Saudolla
https://github.com/shubham-saudolla
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : MonoBehaviour
{
    public Rigidbody2D rb;

    public float shrinkSpeed = 3f;

    void Start()
    {
        rb.rotation = Random.Range(0, 360);
        transform.localScale = Vector3.one * 17f;
    }

    void FixedUpdate()
    {
        if (GameManager.instance.rotatePermission == true)
        {
            transform.localScale -= Vector3.one * shrinkSpeed * Time.deltaTime;
        }

        if (transform.localScale.x <= 0.05f)
        {
            Destroy(this.gameObject);
        }
    }
}
