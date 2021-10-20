using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon
{

    void Awake()
    {
        bulletCount = 27;
        this.type = Type.MachineGun;
        AtkDelay = 0.15f;
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
