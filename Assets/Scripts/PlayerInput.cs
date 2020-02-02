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
    //持续输入类型信号
    public float Dmag;//动画的forward值,代表玩家是否有输入,玩家输入量
    public Vector3 Dvec;//目标朝向值

    //Trigger类型信号
    public bool attack;//当前attack值,如果不等于上一次lastAttack,则代表触发attack
    public bool lastAttack;//上一次的attack值

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

        Vector2 temp = SquareToCircle(Dright,Dup);//将直角坐标改为球形坐标

        Dmag = Mathf.Sqrt(temp.x * temp.x + temp.y * temp.y);//计算玩家输入量
        Dvec = temp.x * transform.right + temp.y * transform.forward;//计算目标朝向

        //攻击
        bool newAttack = Input.GetKey(KeyCode.Q);
        if (newAttack!=lastAttack&&newAttack==true) {
            attack = true;
        }
        else {
            attack = false;
        }
        lastAttack = newAttack;
    }
    //球形函数
    Vector2  SquareToCircle(float x, float y) {
        Vector2 circleVector;
        //球坐标x
        circleVector.x = x * Mathf.Sqrt(1 - (y * y) / 2.0f);
        //球坐标y
        circleVector.y = y * Mathf.Sqrt(1 - (x * x) / 2.0f);
        return circleVector;
    }

}
