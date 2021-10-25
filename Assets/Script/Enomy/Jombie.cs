using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Jombie : MonoBehaviour
{
    public float speed = 8f;
    public float currentTime = 1.0f; //´©Àû½Ã°£
    public float attackDelay = 1.0f; //°ø°Ý µô·¹ÀÌ
    public int attackPower = 5; //°ø°Ý·Â
    public float hp = 15; //Á»ºñ Ã¼·Â
    public Player player;
    public float sight=2.5f;
    public float atkRng=2.5f;
    public Animator ZomAni;
    public Rigidbody Rb = null;

    public event Action onDeath;
    //Á»ºñÀÇ »ç¸ÁÄ«¿îÆ®

    private bool isAttack = true;
    public enum JombieState
    {
        Follow, Attack, Die, Jump
    }
    public JombieState state;

    protected virtual void Awake()
    {
        ZomAni = GetComponent<Animator>();
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
    //¸ÖÆ¼Áß °¡Àå °¡±îÀÌ ÀÖ´Â ÇÃ·¹ÀÌ¾î Ã£´Â ÇÔ¼ö
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
            ZomAni.SetBool("isWalk", true);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(moveVector), 5 * Time.deltaTime);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            
        }
        else
        {
            ZomAni.SetBool("isWalk", false);
            state = JombieState.Attack; 
        }
    }
    protected virtual void Attack()
    {
        //ÇÃ·¹ÀÌ¾î°¡ °ø°Ý ¹üÀ§¶ó¸é °ø°Ý
        
        if (Vector3.Distance(transform.position, player.transform.position) < sight)
        {
            if (isAttack) currentTime = 1.0f;
            //1ÃÊ ¸¶´Ù ÇÃ·¹ÀÌ¾î °ø°Ý
            currentTime += Time.deltaTime;
            if (currentTime > attackDelay)
            {
                ZomAni.SetBool("isAtt", true);
                // ÇÃ·¹ÀÌ¾î¿¡ ¸Â°Ô ¼öÁ¤ 
                player.Damage(attackPower);
                currentTime = 0;
                isAttack = false;
            }
        }
        //¾Æ´Ï¸é ´Ù½Ã ÇÃ·¹ÀÌ¾î¿¡°Ô ÀÌµ¿
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
<<<<<<< HEAD
        Debug.Log(hp);
=======
     
>>>>>>> parent of d25f02d (ìˆ˜ì •ì¤‘)
        if (hp <= 0)
        {
            state = JombieState.Die;
            Destroy(gameObject);
            if (onDeath != null) onDeath();
        }
     }

    //ÇÃ·¹ÀÌ¾î¿¡°Ô µ¥¹ÌÁö ¹Þ´Â ÇÔ¼ö
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
        ZomAni.SetTrigger("Die");
        //2ÃÊ°¡ Áö³­ ÈÄ »ç¶óÁü.  
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);

    }
    
    public void Attacked()
    {
        Vector3 Back = -Vector3.forward;
        Vector3 Pos = (Vector3.up/2 + Back).normalized;
        Rb.AddForce(Pos*10.0f, ForceMode.Impulse);
        Debug.Log("°ø°Ý ¹Þ¾Ò´Ù!!");
    }



}
