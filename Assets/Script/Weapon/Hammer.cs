using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Weapon
{
    
    void Awake()
    {
        this.type = Type.SwingWeapon;
        AtkDamage = 3f;
        AtkDelay = 1.3f;
        AtkRange = GetComponent<BoxCollider>();
    }
      override public void Attack()
    {
        StartCoroutine("SwingAttack");
    }
    IEnumerator SwingAttack()
    {
        this.AtkRange.enabled = false;
        yield return new WaitForSeconds(0.2f);
        this.AtkRange.enabled = true;
        yield return new WaitForSeconds(0.5f);
        this.AtkRange.enabled = false;
    }

    protected override void EnomyAttack()
    {
        
    }
}

