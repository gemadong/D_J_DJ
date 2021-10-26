using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazukar : Weapon
{
    // Start is called before the first frame update
    void Awake()
    {
        this.type = Type.Rebound;
        AtkDelay = 3.5f;
        currentbullet = 2;
        CanhaveMaxCount = 2;
        HaveBulletInPocket = 3;
        AtkDamage = 15;
        UpgradeDamage = 3;
    }

    public override void Attack()
    {
        if (currentbullet > 0)
        {
            StartCoroutine("bazukarshoot");
            currentbullet--;
        }
    }
    IEnumerator bazukarshoot()
    {
        yield return new WaitForSeconds(0.1f);
        Pr.Play();
        GameObject bullet = Instantiate(Bullet, BulletPos.position, BulletPos.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 20f;
    }
}
