using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleController : MonoBehaviour {
    public PlayerInput pi;
    public float rotateSpeed;
    public Animator animator;
    // Start is called before the first frame update
    void Start() {
        rotateSpeed = 8;
        pi = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("move",pi.move);
        Vector3 tempForward = Vector3.Slerp(transform.forward, pi.targetVector, Time.deltaTime * rotateSpeed);
        
        transform.forward = tempForward;
        if (pi.move) {
            //transform.Translate(transform.forward);
        }

//        if (pi.left&&tempForward!=transform.forward) {
//            animator.SetBool("left", pi.left);
//        }
    }
}
