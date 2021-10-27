using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazukarBullet : Bullet
{
    [SerializeField] private GameObject boomb = null;
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

    private void OnDestroy()
    {
        Instantiate(boomb, this.transform.position,Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Plane"))
        {
            Instantiate(boomb, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject, 0.5f);
        }
        if (other.CompareTag("Zombie"))
        {
            Instantiate(boomb, this.transform.position, Quaternion.identity);
            other.GetComponent<Jombie>().Damage(Damage);
            Destroy(this.gameObject, 0.5f);
        }
    }

}
