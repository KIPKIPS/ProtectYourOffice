using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    public Vector3 targetVector;//x值为左右移动,z值为前后移动
    public bool move;//是否移动
    public bool up;
    public bool down;
    public bool left;
    public bool right;
    void Awake() {

    }

    void Start() {
        down=left=right=up = false;
        move = false;
    }

    void Update() {
        up = Input.GetKey(KeyCode.W);
        down = Input.GetKey(KeyCode.S);
        left = Input.GetKey(KeyCode.A);
        right = Input.GetKey(KeyCode.D);

        targetVector = SquareToCircle((right ? 1 : 0) - (left ? 1 : 0), (up ? 1 : 0) - (down ? 1 : 0));
        
        move = up || down || left || right;
    }

    Vector3 SquareToCircle(float x, float z) {
        Vector3 circleVector;
        //球坐标z
        circleVector.x= x * Mathf.Sqrt(1 - Mathf.Pow(z, 2) / 2);
        //球坐标x
        circleVector.z= z * Mathf.Sqrt(1 - Mathf.Pow(x, 2) / 2);

        circleVector.y = 0;
        return circleVector;
    }

}
