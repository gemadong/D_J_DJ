using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazukarBullet : Bullet
{

    void Awake()
    {
        Damage = 20;
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    protected override void Shoot()
    {
        Shoot();
    }
}
