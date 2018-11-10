/*
Copyright (c) Shubham Saudolla
https://github.com/shubham-saudolla
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    private Camera _mainCamera;

    // to change the color every blank seconds 
    public float changeColorEvery = 1f;
    private float colorstep;
    private Color[] colors = new Color[7];
    private int i;

    // the starting color to lerp with
    private Color lerpedColor = Color.magenta;

    void Awake()
    {
        colors[0] = Color.magenta;
        colors[1] = Color.red;
        colors[2] = Color.yellow;
        colors[3] = Color.green;
        colors[4] = Color.cyan;
        colors[5] = Color.blue;
        colors[6] = Color.magenta;

        _mainCamera = GetComponent<Camera>();

        if (_mainCamera == null)
        {
            Debug.Log("Main camera not found!");
        }
    }

    void FixedUpdate()
    {
        if (GameManager.instance.rotatePermission == true)
        {
            transform.Rotate(Vector3.forward, Time.deltaTime * 30f);
        }
    }

    void Update()
    {
        if (GameManager.instance.giveAnEpilepticAttack == true)
        {
            if (colorstep < changeColorEvery)
            {
                lerpedColor = Color.Lerp(colors[i], colors[i + 1], colorstep);
                _mainCamera.backgroundColor = lerpedColor;
                colorstep += 0.002f;
            }
            else
            {
                colorstep = 0;

                if (i < (colors.Length - 2))
                {
                    i++;
                }
                else
                {
                    i = 0;
                }
            }
        }
    }
}
