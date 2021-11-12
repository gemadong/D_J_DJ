using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sawing : Weapon
{
    [SerializeField] private ParticleSystem PS = null;
    private bool isAtk = false;
    private bool isParticle = false;

        void Awake()
        {
            this.type = Type.Electric;
            AtkDamage = 3;
            AtkDelay = 1.0f;
            AtkRange = GetComponent<BoxCollider>();
            AtkRange.enabled = false;
            PS.Stop();
        UpgradeDamage = 2;

    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) PS.Play();
        if (Input.GetMouseButtonUp(0)) PS.Stop();
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
