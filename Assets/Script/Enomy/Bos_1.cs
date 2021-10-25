using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bos_1 : MonoBehaviour
{
    int hp = 100;
    Animator BosAni;
    public Transform target;
    private float bosSpeed = 5f;
    private Rigidbody rigid;
    bool Actswitch; //액션 스위치
    float delaytime = 0; //점프 딜레이


    private void Start()
    {
        BosAni = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        Actswitch = true;
    }
    void RotateBos()
    {
        Vector3 dir = target.position - transform.position;
        //타겟방향으로 slerp을 이용해 회전시킴
        //transform.localRotation에서 LookRotation(dir)까지 중 Time.deltaTime에 해당하는 각도를 반환함
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(dir), 5 * Time.deltaTime);

    }
    void MoveBos()
    {
        //magnitude : 벡터의 정확한 길이 반환
        if((target.position - transform.position).magnitude >= 3)
        {
            BosAni.SetBool("Walk", true);
            //Translate : 객체를 z축을 따라 1단위 앞으로 이동함. 
            transform.Translate(Vector3.forward * bosSpeed * Time.deltaTime);
            
        }
        if ((target.position - transform.position).magnitude < 3)
        {
            BosAni.SetBool("Walk", false);
        }
    }
    private void Update()
    {
        delaytime += Time.deltaTime;
        Jump();
        if (Actswitch)
        {
            RotateBos();
            MoveBos();
        }
    }
    void BosAtk()
    {
        if ((target.position - transform.position).magnitude < 3)
        {
            BosAni.Play("Attack");
        }
    }


    //(ani 이벤트로 들어가 있음.)공격할때 못 움직이게 하는 함수
    void FreezeBos()
    {
        Actswitch = false;
    }
    //(ani 이벤트로 들어가 있음.)공격 끝나고 다시 움직임 받아오는 함수.
    void UnFreezeBos()
    {
        Actswitch = true;
    }
    void Jump()
    {
        if (delaytime >= 10f)
        {
            BosAni.Play("Jump");
            rigid.AddForce(transform.up * 15, ForceMode.Impulse);
            delaytime = 0;
        }
    }
    public void Damage(int _dmg)
    {
        hp -= _dmg;
        if (hp <= 0)
        {
            Destroy(gameObject);
            //if (onDeath != null) onDeath();
        }
    }
}
