using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject Player = null;
    [SerializeField] private GameObject[] Weapon = null;
    [SerializeField] private int[] BuyWeaponPrice = null;

    [SerializeField] private int[] UpgradePrice = null;
    [SerializeField] private int[] BuyBulletUpgrade = null;


    public int UpAmount = 0;

public void OnClickBuyWeapon(int Num)
    {
        Player.GetComponent<Player>().BuyWeapon(Num);
    }

    public void OnClickWeaponDamageUp(int index)
    {
        Weapon[index].GetComponent<Weapon>().DamageUp(UpAmount);
    }
}
