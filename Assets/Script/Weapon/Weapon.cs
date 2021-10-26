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

    protected float NuckbackPower = 0;
    protected float NuckbackUpPower = 0;

    public ParticleSystem Pr = null;

    protected int UpgradeDamage = 0;

    virtual public void Attack()
    { }

    private void Update()
    {
        Debug.Log("데미지"+AtkDamage);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            other.GetComponent<Jombie>().Damage(AtkDamage);

            Rigidbody ERb = other.GetComponent<Rigidbody>();
            Vector3 reacVec = -other.transform.forward*NuckbackPower+other.transform.up*NuckbackUpPower;
            ERb.AddForce(reacVec.normalized*NuckbackPower, ForceMode.Impulse);
        }
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

    public void DamageUp()
    {
        Debug.Log("공격력 업글");
        AtkDamage += UpgradeDamage;
    }

    public void BuyBullet()
    {
        HaveBulletInPocket += CanhaveMaxCount;
    }


}
