using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Jombie : MonoBehaviour
{
    public float speed = 8f;
    public float currentTime = 1.0f; //누적시간
    public float attackDelay = 1.0f; //공격 딜레이
    public int attackPower = 5; //공격력
    public float hp = 15; //좀비 체력
    public Player player;
    public float sight = 2.6f;
    public float atkRng = 2.5f;
    public Animator ZomAni;
    public Rigidbody Rb = null;
    private BoxCollider BC = null;
    protected int DieCoin = 200;


    public event Action onDeath;
    //좀비의 사망카운트

    public bool isAttack = true;
    public enum JombieState
    {
        Follow, Attack, Die, Jump
    }
    public JombieState state;

    protected virtual void Awake()
    {
        ZomAni = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody>();
        BC = GetComponent<BoxCollider>();
    }

    protected virtual void Update()
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

        }
    }
    //멀티중 가장 가까이 있는 플레이어 찾는 함수
    public void FindClosestPlayer()
    {
        float tempDis = float.PositiveInfinity;
        Player tempPlayer = null;

        for (int i = 0; i < GameManager.instance.players.Count; i++)
        {
            float dis = (GameManager.instance.players[i].transform.position - transform.position).magnitude;
            if (dis < tempDis)
            {
                tempDis = dis;
                tempPlayer = GameManager.instance.players[i];
            }
        }

        if (tempPlayer != null)
            player = tempPlayer;
    }

    protected virtual void Follow()
    {
        Vector3 moveVector = player.transform.position - transform.position;
        moveVector.y = 0;
        if (moveVector.magnitude > atkRng)
        {
            state = JombieState.Follow;
            ZomAni.SetBool("isWalk", true);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(moveVector), 5 * Time.deltaTime);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

        }
        else
        {
            ZomAni.SetBool("isWalk", false);
            state = JombieState.Attack;
            isAttack = true;
        }
    }
    protected virtual void Attack()
    {
        //플레이어가 공격 범위라면 공격

        if (Vector3.Distance(transform.position, player.transform.position) < sight)
        {
            if (isAttack) currentTime = 2.0f;
            //1초 마다 플레이어 공격
            currentTime += Time.deltaTime;
            if (currentTime > attackDelay)
            {
                ZomAni.SetBool("isAtt", true);
                player.Damage(attackPower);
                currentTime = 0;
                isAttack = false;
            }
        }
        //아니면 다시 플레이어에게 이동
        else
        {
            ZomAni.SetBool("isAtt", false);
            state = JombieState.Follow;
            currentTime = 0;
            isAttack = true;

        }
    }

    public void Damage(int _dmg)
    {
        hp -= _dmg;
        Debug.Log(hp);
        if (hp <= 0)
        {
            state = JombieState.Die;

            if (onDeath != null) onDeath();
        }
    }

    //플레이어에게 데미지 받는 함수
    public void HitJombie(int hitPower)
    {
        if (state == JombieState.Die) { return; }
        hp -= hitPower;
        if (hp > 0)
        {
            state = JombieState.Follow;
            Follow();
        }
        else
        {
            state = JombieState.Die;

        }
    }
    protected virtual IEnumerator Die()
    {
 //       player.CoinPP(DieCoin);
        BC.enabled = false;
        ZomAni.SetTrigger("Die");
        Debug.Log("좀비 Die");
        //2초가 지난 후 사라짐.  
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }

    public void Attacked()
    {
        Vector3 Back = -Vector3.forward;
        Vector3 Pos = (Vector3.up / 2 + Back).normalized;
        Rb.AddForce(Pos * 10.0f, ForceMode.Impulse);
        Debug.Log("공격 받았다!!");
    }

    public void OnDestroy()
    {
        player.CoinPP(DieCoin);

    }

}
