using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Hand, SwingWeapon, Electric, Rebound, Gun };
    public Type type = 0;

    protected int AtkDamage = 0;
    public float AtkDelay = 0f;
    protected BoxCollider AtkRange = null;

    public GameObject Bullet = null;
    public Transform BulletPos = null;

    public int HaveBulletInPocket = 0;
    public int currentbullet = 0;
    protected int CanhaveMaxCount = 0;



    virtual public void Attack()
    { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            other.GetComponent<Jombie>().Damage(AtkDamage);
            Debug.Log("АјАн!");
        }
    }

    protected virtual void EnomyAttack()
    {

    }

    public void ReLoad()
    {
        for (int i = currentbullet; i <= CanhaveMaxCount; i++)
        {
            if (HaveBulletInPocket > 0)
            {
                currentbullet++;
                HaveBulletInPocket--;
            }
        }
    }

    public void DamageUp(int AtkDamageUp)
    {
        AtkDamage += AtkDamageUp;
    }
}
