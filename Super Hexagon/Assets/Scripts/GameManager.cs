/*
Copyright (c) Shubham Saudolla
https://github.com/shubham-saudolla
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool rotatePermission = true;

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
        rotatePermission = true;
    }

    void Update()
    {

    }

    public void RevokePermission()
    {
        rotatePermission = false;
    }

    public void GrantPermission()
    {
        rotatePermission = true;
    }
}
