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
    }
    void Update()
    {
        Shoot();
    }

    private void OnEnable()
    {
        StartCoroutine("Destroybull");
    }

    IEnumerator Destroybull()
    {
        
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);


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
            Destroy(this.gameObject);
            Attack();
        }


    }


    protected virtual void Attack() { }

}
