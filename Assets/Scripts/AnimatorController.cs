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
    private Vector3 planarVec;//玩家平面移动值

    public bool lockPlanar=false;//是否锁定平面移动量
    public Vector3 thrustVec;//冲量

    public bool slow=false;

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
        if (slow) {
            pi.Dup /= 8;
            pi.Dright /= 8;
        }
        animator.SetFloat("forward", pi.Dmag);
        if (pi.Dmag > 0.1f) {
            //转向的平滑插值 防止转向过于突然
            model.transform.forward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.4f);//t值太大,转向太突然,太小导致转向发生位移
        }
        //未锁定平面移动量时,进行位移
        if (lockPlanar==false) {
            planarVec = pi.Dmag * model.transform.forward * walkSpeed;
        }
        //动画控制
        if (pi.attack) {
            animator.SetTrigger("attack");
        }
    }

    void OnAttack_01_Enter() {
        print("On Attack Enter");
        //攻击时关闭输入防止位移
        //pi.inputEnable = false;
        slow = true;
        animator.SetLayerWeight(animator.GetLayerIndex("Attack"),1.0f);
        //lockPlanar = true;
    }
    void OnAttack_01_Exit() {
        print("On Attack Exit");
        //pi.inputEnable = true;
        slow = false;
        animator.SetLayerWeight(animator.GetLayerIndex("Attack"), 0f);
    }


    void OnAttack_Idle_Enter() {
        //lockPlanar = false;
    }

    void FixedUpdate() {
        //rigid.position+=movingVec*Time.fixedDeltaTime;
        //防止对象坐标的y值受到移动值的影响
        rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z)+thrustVec;
        thrustVec = Vector3.zero;
    }
}
