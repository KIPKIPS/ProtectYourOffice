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

    public float Dmag;//动画的forward值,代表玩家是否有输入,玩家输入量
    public Vector3 Dvec;//目标朝向值

    void Awake() {
        inputEnable = true;
    }

    void Start() {

    }

    void Update() {
        
        //玩家输入目标值
        targetDup = (Input.GetKey(keyUp) ? 1.0f : 0) - (Input.GetKey(keyDown) ? 1.0f : 0);
        targetDright = (Input.GetKey(keyRight) ? 1.0f : 0) - (Input.GetKey(keyLeft) ? 1.0f : 0);
        if (inputEnable==false) {
            targetDup = 0;
            targetDright = 0;
        }
        //对输入值进行插值计算
        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);
        
        Dmag = Mathf.Sqrt(Dup * Dup + Dright * Dright);//计算玩家输入量
        Dvec =  Dright * transform.right+ Dup * transform.forward;//计算目标朝向
    }
    
}
