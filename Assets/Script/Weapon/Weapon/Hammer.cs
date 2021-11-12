using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Weapon
{
    [SerializeField] TrailRenderer Tr = null;
    void Awake()
    {
        this.type = Type.SwingWeapon;
        AtkDamage = 3;
        AtkDelay = 1.3f;
        AtkRange = GetComponent<BoxCollider>();
        AtkRange.enabled = false;
        NuckbackPower = 250.0f;
        NuckbackUpPower = 3.0f;
        Tr.enabled = false;
        UpgradeDamage = 1;
    }

      override public void Attack()
    {
        StartCoroutine("SwingAttack");
    }
    IEnumerator SwingAttack()
    {
        Tr.enabled = false;
        this.AtkRange.enabled = false;
        yield return new WaitForSeconds(0.4f);
        this.AtkRange.enabled = true;
        Tr.enabled = true;
        yield return new WaitForSeconds(0.5f);
        this.AtkRange.enabled = false;
        Tr.enabled = false;
    }

 
}

