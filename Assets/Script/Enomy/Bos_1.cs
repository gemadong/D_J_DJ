using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bos_1 : Jombie
{
    [SerializeField] private Slider hpbar_boss;
    [SerializeField] private Text hptext_boss;

    private float curhp = 50f;
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
        hpbar_boss.value = hp / curhp;
        hptext_boss.text = "보스HP : " + hp;
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
        if (moveVector.magnitude <= 15f && moveVector.magnitude > 11f)
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
