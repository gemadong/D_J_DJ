using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jombie_J : Jombie
{
    private float delaytime;

    // private Rigidbody rigid;
    protected override void Awake()
    {
        base.Awake();
        delaytime = 4f;
        //rigid = GetComponent<Rigidbody>();
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
        ZomAni.SetBool("isWalk", true);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(moveVector), 5 * Time.deltaTime);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (moveVector.magnitude <= 9f&& moveVector.magnitude >= 5f)
        {
            ZomAni.SetBool("isWalk", false);
            ZomAni.Play("Zombie_J_Jump");
            ZomAni.SetBool("isWalk", true);
        }
        if(moveVector.magnitude <atkRng)
        {
            ZomAni.SetBool("isWalk", false);
            state = JombieState.Attack;
        }

    }
    protected void Jump()
    {
        ZomAni.Play("Zombie_J_Jump");
        //ZomAni.SetBool("isJump", true);
        //Rb.velocity = this.transform.up * 15f;
        //Rb.velocity = this.transform.forward * 15f;
        //delaytime = 0;
        state = JombieState.Follow;
       // ZomAni.SetBool("isJump", false);
        //if (Vector3.Distance(player.transform.position, this.transform.position) <= 10.0f)
        //{
        //    delaytime += Time.deltaTime;
        //    if (delaytime >= 4.0f)
        //    {
        //        ZomAni.SetBool("isJump", true);
        //        Rb.velocity = this.transform.up * 15f;
        //        delaytime = 0;
        //        state = JombieState.Follow;
        //        ZomAni.SetBool("isJump", false);
        //    }
        //}
    }
}
