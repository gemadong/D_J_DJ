using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
public enum Type { Hand,SwingWeapon,Electric,HandGun,MachineGun,FireGun,ShootGun,LazerGun,bazukar};
public Type type =0;

protected float AtkDamage = 0;
public float AtkDelay = 0f;
protected BoxCollider AtkRange = null;

    public GameObject Bullet = null;
    public Transform BulletPos = null;

    protected int HasMaxBullet = 0;
    protected int bulletCount = 0;

   

    virtual public void Attack()
    {    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            other.GetComponent<Jombie>().Attacked();
            Debug.Log("АјАн!");
        }
    }

    protected virtual void EnomyAttack()
    {

    }
}
