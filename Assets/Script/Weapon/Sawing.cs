using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sawing : Weapon
{

        void Awake()
        {
            this.type = Type.Electric;
            AtkDamage = 0.3f;
            AtkDelay = 0.1f;
            AtkRange = GetComponent<BoxCollider>();
        }
    

    // Update is called once per frame
    void Update()
    {
        Attack();    
    }

    public override void Attack()
    {
        if (Input.GetMouseButtonDown(0)) AtkRange.enabled = true;
        else if (Input.GetMouseButtonUp(0)) AtkRange.enabled = false;
    }

}
