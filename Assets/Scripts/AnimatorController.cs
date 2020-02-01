using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour {
    public GameObject model;
    public PlayerInput pi;
    [SerializeField]
    public Animator animator;

    public float walkSpeed = 2.0f;

    public Rigidbody rigid;
    private Vector3 movingVec;//玩家移动值

    void Awake() {
        //初始化
        pi = GetComponent<PlayerInput>();
        model = GameObject.Find("Model");
        animator = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        //设置动画混合值
        animator.SetFloat("forward", pi.Dmag);
        if (pi.Dmag > 0.1f) {
            Vector3 tempDir = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.1f);//转向的平滑插值
            Debug.DrawRay(model.transform.position, tempDir, Color.red, 200);
            model.transform.forward = tempDir;
        }
        movingVec = pi.Dmag * model.transform.forward * walkSpeed;
    }

    void FixedUpdate() {
        //rigid.position+=movingVec*Time.fixedDeltaTime;
        rigid.velocity = movingVec;
    }
}
