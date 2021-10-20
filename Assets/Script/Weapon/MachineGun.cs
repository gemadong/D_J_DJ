using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon
{

    void Awake()
    {
        currentbullet = 27;
        CanhaveMaxCount = 27;
        HaveBulletInPocket = 0;
        this.type = Type.Gun;
        AtkDelay = 0.15f;
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
        GameObject bullet = Instantiate(Bullet, BulletPos.position, BulletPos.rotation);
    }
}
