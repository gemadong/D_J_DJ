using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeWeapon : Weapon
{
    [SerializeField] private TrailRenderer Tr = null;
    void Awake()
    {
        this.type = Type.SwingWeapon;
        AtkDamage = 3;
        AtkDelay = 1.3f;
        AtkRange = GetComponent<BoxCollider>();
        AtkRange.enabled = false;
        Tr.enabled = false;
        UpgradeDamage = 3;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
    override public void Attack()
    {
        StartCoroutine("SwingAttack");
    }
    IEnumerator SwingAttack()
    {
        this.AtkRange.enabled = false;
        Tr.enabled = false;
        yield return new WaitForSeconds(0.3f);
        this.AtkRange.enabled = true;
        Tr.enabled = true ;
        yield return new WaitForSeconds(0.5f);
        this.AtkRange.enabled = false;
        Tr.enabled = false;
    }
}
