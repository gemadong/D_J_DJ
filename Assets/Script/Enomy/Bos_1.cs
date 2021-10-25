using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bos_1 : Jombie
{
    protected override void Awake()
    {
        base.Awake();
        sight = 3.1f;
        atkRng = 3.0f;
        hp = 50f;
        speed = 6f;
        attackPower = 30;
    }
    protected override void Update()
    {
        FindClosestPlayer();
        switch (state)
        {
            case JombieState.Follow:
                Follow();
                break;
            case JombieState.Attack:
                Attack();
                break;
            case JombieState.Die:
                StartCoroutine("Die");
                break;
            case JombieState.Jump:
                Jump();

                break;

        }
    }
    protected override void Follow()
    {
        Vector3 moveVector = player.transform.position - transform.position;
        moveVector.y = 0;
        // Debug.Log(moveVector.magnitude);
        ZomAni.SetBool("Walk", true);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(moveVector), 5 * Time.deltaTime);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (moveVector.magnitude <= 15f&&moveVector.magnitude > 11f)
        {
            ZomAni.SetBool("Walk", false);
            StartCoroutine("Jump");
            //state = JombieState.Jump;
            ZomAni.SetBool("Walk", true);
        }
        if (moveVector.magnitude < atkRng)
        {
            ZomAni.SetBool("Walk", false);
            state = JombieState.Attack;
        }

    }
    IEnumerator Jump()
    {
        Debug.Log("점프!!");
        //Rb.isKinematic = true;
        ZomAni.Play("Jump");
       // ZomAni.SetBool("Jump",true);
        //Rb.AddForce(transform.up, ForceMode.Impulse);
        //Rb.AddForce(transform.forward, ForceMode.Impulse);
        //yield return new WaitForSeconds(1.0f);
        //Rb.AddForce(-transform.up * 3, ForceMode.Impulse);
        yield return new WaitForSeconds(2.0f);
        //Rb.isKinematic = false;
        //ZomAni.SetBool("Jump", false);
    }
}




//public class Bos_1 : MonoBehaviour
//{
//    int hp = 100;
//    Animator BosAni;
//    public Transform target;
//    private float bosSpeed = 5f;
//    private Rigidbody rigid;
//    bool Actswitch; //액션 스위치
//    float delaytime = 0; //점프 딜레이


//    private void Start()
//    {
//        BosAni = GetComponent<Animator>();
//        rigid = GetComponent<Rigidbody>();
//        Actswitch = true;
//    }
//    void RotateBos()
//    {
//        Vector3 dir = target.position - transform.position;
//        //타겟방향으로 slerp을 이용해 회전시킴
//        //transform.localRotation에서 LookRotation(dir)까지 중 Time.deltaTime에 해당하는 각도를 반환함
//        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(dir), 5 * Time.deltaTime);

//    }
//    void MoveBos()
//    {
//        //magnitude : 벡터의 정확한 길이 반환
//        if((target.position - transform.position).magnitude >= 3)
//        {
//            BosAni.SetBool("Walk", true);
//            //Translate : 객체를 z축을 따라 1단위 앞으로 이동함. 
//            transform.Translate(Vector3.forward * bosSpeed * Time.deltaTime);

//        }
//        if ((target.position - transform.position).magnitude < 3)
//        {
//            BosAni.SetBool("Walk", false);
//        }
//    }
//    private void Update()
//    {
//        delaytime += Time.deltaTime;
//        Jump();
//        if (Actswitch)
//        {
//            RotateBos();
//            MoveBos();
//        }
//    }
//    void BosAtk()
//    {
//        if ((target.position - transform.position).magnitude < 3)
//        {
//            BosAni.Play("Attack");
//        }
//    }


//    //(ani 이벤트로 들어가 있음.)공격할때 못 움직이게 하는 함수
//    void FreezeBos()
//    {
//        Actswitch = false;
//    }
//    //(ani 이벤트로 들어가 있음.)공격 끝나고 다시 움직임 받아오는 함수.
//    void UnFreezeBos()
//    {
//        Actswitch = true;
//    }
//    void Jump()
//    {
//        if (delaytime >= 10f)
//        {
//            BosAni.Play("Jump");
//            rigid.AddForce(transform.up * 15, ForceMode.Impulse);
//            delaytime = 0;
//        }
//    }
//    public void Damage(int _dmg)
//    {
//        hp -= _dmg;
//        if (hp <= 0)
//        {
//            Destroy(gameObject);
//            //if (onDeath != null) onDeath();
//        }
//    }
//}
