using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeWeapon : Weapon
{
 
    void Awake()
    {
        this.type = Type.SwingWeapon;
        AtkDamage = 3f;
        AtkDelay = 1.3f;
        AtkRange = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
