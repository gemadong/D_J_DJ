using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : Weapon
{
    private CapsuleCollider CC = null;
    [SerializeField] private ParticleSystem Ps = null;
    [SerializeField] private GameObject ParticleOb = null;

    private bool isFire = false;
    private float Time_ = 0;

    void Awake()
    {
        ParticleOb.SetActive(true);
        CC = Ps.GetComponent<CapsuleCollider>();
        Ps.Stop();
        CC.enabled = false;
        this.type = Type.Gun;
        AtkDelay = 0.10f;
        currentbullet = 30;
        CanhaveMaxCount = 30;
        HaveBulletInPocket = 0;
        AtkDamage = 3;
        UpgradeDamage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentbullet);
        if (isFire)
        {
            StartCoroutine("FireShoot");
            Time_ += Time.deltaTime;
            currentbullet -= (int)Time_;
        }
        else if(!isFire)
        {
            StopCoroutine("FireShoot");
            Time_= 0;
        }

      
            Attack();
            if (Input.GetMouseButtonDown(0))
            {
                isFire = true;
                Ps.Play();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Ps.Stop();
                isFire = false;
            }
        

    }

    IEnumerator FireShoot()
    {
        CC.enabled = true;
   
        yield return new WaitForSeconds(0.5f);
        CC.enabled = false;
     
        yield return new WaitForSeconds(0.5f);

    }

    public override void Attack()
    {

    }

}
