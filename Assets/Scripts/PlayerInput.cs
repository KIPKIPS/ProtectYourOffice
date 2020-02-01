using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    public string keyUp = "w";
    public string keyDown = "s";
    public string keyLeft = "a";
    public string keyRight = "d";

    //输入信号值
    public float Dup;
    public float Dright;
    //平滑输入值
    private float targetDup;
    private float targetDright;
    private float velocityDup;
    private float velocityDright;
    //输入开关
    public bool inputEnable = true;
    void Awake() {

    }

    void Start() {

    }

    void Update() {
        //目标值
        targetDup = inputEnable?(Input.GetKey(keyUp) ? 1.0f : 0) - (Input.GetKey(keyDown) ? 1.0f : 0):0;
        targetDright = inputEnable?(Input.GetKey(keyRight) ? 1.0f : 0) - (Input.GetKey(keyLeft) ? 1.0f : 0):0;
        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);
    }
}
