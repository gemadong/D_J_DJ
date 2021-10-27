using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGun : Weapon
{

    void Awake()
    {
        this.type = Type.Gun;
        AtkDamage = 3;
        AtkDelay = 0.6f;
        currentbullet = 10;
        CanhaveMaxCount = 10;
        HaveBulletInPocket = 0;
        UpgradeDamage = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void Attack()
    {
        if (currentbullet > 0)
        {
            StartCoroutine("Shoot");
            currentbullet--;
        }
    }
    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.1f);
        Pr.Play();
        GameObject bullet = Instantiate(Bullet, BulletPos.position, BulletPos.rotation);
        bullet.GetComponent<Bullet>().Damage = AtkDamage;
    }
}
