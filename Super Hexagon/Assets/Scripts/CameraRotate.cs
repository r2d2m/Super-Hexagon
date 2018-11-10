/*
Copyright (c) Shubham Saudolla
https://github.com/shubham-saudolla
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    void FixedUpdate()
    {
        if (GameManager.instance.rotatePermission == true)
        {
            transform.Rotate(Vector3.forward, Time.deltaTime * 30f);
        }
    }
}
