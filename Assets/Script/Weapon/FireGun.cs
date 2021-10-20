using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : Weapon
{
    private CapsuleCollider CC =null;
    [SerializeField] private ParticleSystem Ps = null;
    [SerializeField] private GameObject ParticleOb = null;
    void Awake()
    {
        ParticleOb.SetActive(true);
        CC = Ps.GetComponent<CapsuleCollider>();
        Ps.Stop();
        CC.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        if (Input.GetMouseButtonDown(0))
        {
            CC.enabled = true;
            Ps.Play();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Ps.Stop();
            CC.enabled = false;
        }

    }
    public override void Attack()
    {
       
    }

}
