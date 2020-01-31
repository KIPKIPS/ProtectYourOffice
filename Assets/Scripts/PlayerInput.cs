using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    public float speed;

    public Animator animator;
    // Start is called before the first frame update
    void Start() {
        speed = 5;
        animator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.D)) {
            animator.SetBool("move",true);
            animator.SetBool("idle", false);
        }

        if (!Input.anyKey) {
            animator.SetBool("idle", true);
            animator.SetBool("move", false);
        }
        //控制物体的移动
        this.transform.Translate(new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0, Input.GetAxis("Vertical") * Time.deltaTime * speed));
    }
}
