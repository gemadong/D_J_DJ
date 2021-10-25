using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bos_2 : MonoBehaviour
{
    [SerializeField] private GameObject PoisonPrefab = null;
    float delayTime = 0;
    float delayTime_A = 0;
    public Transform target;
    private float bosSpeed=5f;
    bool isattack;
    int hp = 100;
    Animator bosani_2;

    private void Start()
    {
        isattack = true;
        bosani_2 = GetComponent<Animator>();
    }
    private void Update()
    {
        delayTime += Time.deltaTime;
        delayTime_A += Time.deltaTime;
        if(delayTime >= 5f) { Fire(); delayTime = 0; }
        if (isattack == true) { RotateBos(); }
        MoveBos();
    }

    public void Fire()
    {
        bosani_2.SetBool("Att1", true);
        GameObject poison = Instantiate(PoisonPrefab, transform.position, Quaternion.identity);
        poison.GetComponent<Poison>().Shoot(transform.forward);
        bosani_2.SetBool("Att1", false);
    }
    void RotateBos()
    {
        Vector3 dir = target.position - transform.position;
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(dir), 5 * Time.deltaTime);

<<<<<<< HEAD
            isAttack = false;
            if (Vector3.Distance(transform.position, player.transform.position) < sight)
            {
                if (isAttack_1) currentTime = 4.0f;
                //1ÃÊ ¸¶´Ù ÇÃ·¹ÀÌ¾î °ø°Ý
                currentTime += Time.deltaTime;
                if (currentTime > attackDelay)
                {
                    ZomAni.SetBool("Att2", true);
                yield return new WaitForSeconds(A);
                    for (int i = -10; i < 120; i += 3)
                    {
                        Vector3 roR = new Vector3(0, i, 0);
                        GameObject poison = Instantiate(PoisonPrefab, transform.position, Quaternion.identity);
                        Vector3 dir = transform.forward + roR * Time.deltaTime;
                        poison.GetComponent<Poison>().Shoot(dir);
                        yield return new WaitForSeconds(0.01f);
                    }
                yield return new WaitForSeconds(0.1f);
                    isAttack = true;

                }
            }
        

        //¾Æ´Ï¸é ´Ù½Ã ÇÃ·¹ÀÌ¾î¿¡°Ô ÀÌµ¿
        else
=======
    }
    void MoveBos()
    {
        if ((target.position - transform.position).magnitude >= 10)
>>>>>>> parent of d25f02d (ìˆ˜ì •ì¤‘)
        {
            transform.Translate(Vector3.forward * bosSpeed * Time.deltaTime);
            bosani_2.SetBool("Walk", true);
        }
        if ((target.position - transform.position).magnitude < 10)
        {
            bosani_2.SetBool("Walk", false);
            if (delayTime_A >= 5f) { StartCoroutine("Attack");; delayTime_A = 0; }
        }
    }
    IEnumerator Attack()
    {
        isattack = false;
        bosani_2.SetBool("Att2", true);
        for (int i =-10;i<120; i+=3)
        {
            Vector3 roR = new Vector3(0, i, 0);
            GameObject poison = Instantiate(PoisonPrefab, transform.position, Quaternion.identity);
            Vector3 dir = transform.forward + roR*Time.deltaTime;
            poison.GetComponent<Poison>().Shoot(dir);
            yield return new WaitForSeconds(0.01f);
        }
        isattack = true;
        bosani_2.SetBool("Att2", false);
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
