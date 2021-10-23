using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
public float Speed = 0;
public int Damage = 0;
private Rigidbody Brb = null;

    private void Awake()
    {
        Brb = this.GetComponent<Rigidbody>();
        Destroy(this, 3.0f);
    }
    void Update()
    {
        Shoot();
    }

    protected virtual void Shoot()
    {
        Brb.velocity = this.transform.forward * Speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie")) 
        {
            other.GetComponent<Jombie>().Damage(Damage);
            Attack();
        }

    }

    protected virtual void Attack() { }

}
