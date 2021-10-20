using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jombie : MonoBehaviour
{
    public float speed = 8f;
    private float currentTime = 0; //누적시간
    private float attackDelay = 0.1f; //공격 딜레이
    public int attackPower = 5; //공격력
    public int hp = 15; //좀비 체력
    public Player player;
    public float sight=1f;
    public float atkRng=1f;

    private Rigidbody Rb = null;
    public enum JombieState
    {
        Follow, Attack, Die
    }
    public JombieState state;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
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
                Die();
                break;
        }
    }
    private void FindClosestPlayer()
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
        if (moveVector.magnitude > atkRng)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(moveVector), 5 * Time.deltaTime);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        { state = JombieState.Attack; }
    }
    protected virtual void Attack()
    {
        //플레이어가 공격 범위라면 공격
        if (Vector3.Distance(transform.position, player.transform.position) < sight)
        {
            //1초 마다 플레이어 공격
            currentTime += Time.deltaTime;
            if (currentTime > attackDelay)
            {
                // 플레이어에 맞게 수정 
                player.Damage(attackPower);
                currentTime = 0;
            }
        }
        //아니면 다시 플레이어에게 이동
        else
        {
            state = JombieState.Follow;
            currentTime = 0;
        }
    }
    
    protected virtual void Damage(int _dmg)
    {
        hp -= _dmg;
        if (hp == 0) Destroy(gameObject);
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
            Die();
        }
    }
    protected virtual IEnumerator Die()
    {
        //2초가 지난 후 사라짐.  
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    
    public void Attacked()
    {
        Vector3 Back = -Vector3.forward;
        Vector3 Pos = (Vector3.up/2 + Back).normalized;
        Rb.AddForce(Pos*10.0f, ForceMode.Impulse);
        Debug.Log("공격 받았다!!");
    }
    
}
