using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazukar : Weapon
{
    // Start is called before the first frame update
    void Awake()
    {
        this.type = Type.bazukar;
        AtkDelay = 3.5f;
    }

    public override void Attack()
    {
        StartCoroutine("bazukarshoot");
    }
    IEnumerator bazukarshoot()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject bullet = Instantiate(Bullet, BulletPos.position, BulletPos.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 20f;
    }
}
