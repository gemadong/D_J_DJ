using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGun : Weapon
{

    void Awake()
    {
        this.type = Type.HandGun;
        AtkDelay = 0.6f;
        bulletCount = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Attack()
    {
        if (bulletCount > 0)
        {
            StartCoroutine("Shoot");
            bulletCount--;
        }
    }
    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject bullet = Instantiate(Bullet, BulletPos.position, BulletPos.rotation);
    }
}
