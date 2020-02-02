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
            //转向的平滑插值 防止转向过于突然
            model.transform.forward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.4f);//t值太大,转向太突然,太小导致转向发生位移
        }

        movingVec = pi.Dmag * model.transform.forward * walkSpeed;
        //动画控制
        if (pi.attack) {
            animator.SetTrigger("attack");
        }
    }

    void OnAttack_01_Enter() {
        //print("On Attack Enter");
        //攻击时关闭输入防止位移
        //pi.inputEnable = false;
        animator.SetLayerWeight(animator.GetLayerIndex("attack"),1.0f);
    }
    void OnAttack_01_Exit() {
        //print("On Attack Exit");
        //pi.inputEnable = true;
    }

    void OnAttackIdle() {
        animator.SetLayerWeight(animator.GetLayerIndex("attack"), 0f);
    }

    void FixedUpdate() {
        //rigid.position+=movingVec*Time.fixedDeltaTime;
        //防止对象坐标的y值受到移动值的影响
        rigid.velocity = new Vector3(movingVec.x, rigid.velocity.y, movingVec.z);
    }
}
