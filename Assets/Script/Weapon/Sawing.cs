using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sawing : Weapon
{

    private bool isAtk = false;
        void Awake()
        {
            this.type = Type.Electric;
            AtkDamage = 1;
            AtkDelay = 1.0f;
            AtkRange = GetComponent<BoxCollider>();
            AtkRange.enabled = false;
        }
    

    // Update is called once per frame
    void Update()
    {
    }

    public override void Attack()
    {
        StartCoroutine("SawingAtk");

    }

    IEnumerator SawingAtk()
    {

        AtkRange.enabled = true;
        yield return new WaitForSeconds(0.6f);
        AtkRange.enabled = false ;
    }




}
