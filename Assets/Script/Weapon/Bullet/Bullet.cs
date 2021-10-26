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
        //Destroy(this, 3.0f);
        StartCoroutine("Destroybull");
    }
    void Update()
    {
        Shoot();
    }

    IEnumerator Destroybull()
    {
        yield return new WaitForSeconds(1.0f);
        BulletPool.Returnbullet(this.gameObject);
        Debug.Log("Return");
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
