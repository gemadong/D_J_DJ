using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGun : Weapon
{

    void Awake()
    {
        this.type = Type.ShootGun;
        AtkDelay = 1.5f;
        bulletCount = 2;
    }

    public override void Attack()
    {
        StartCoroutine("ShootGun_");
    }

    IEnumerator ShootGun_()
    {
        yield return new WaitForSeconds(0.1f);
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                GameObject bullet = Instantiate(Bullet, BulletPos.position, BulletPos.rotation);
                bullet.transform.Rotate(i * 4, j * 4, 0);

            }
        }
    }

}
