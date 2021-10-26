using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject Player = null;
    [SerializeField] private GameObject[] Weapon = null;
    [SerializeField] private Text[] PlayerUpgradeState = null;
    [SerializeField] private int[] BuyPrice;

    [SerializeField] private int[] UpgradePrice = null;
    [SerializeField] private int[] BuyBulletUpgrade = null;

   [SerializeField] private bool[] WeaponBuy;

    private int PlayerCoin = 0;

    public int UpAmount = 0;

    private int UpgradePrice_Cha_Speed = 100;
    private int UpgradePrice_Cha_Hp = 100;
    private int UpgradePrice_Cha_Jump = 100;



    private void Update()
    {
        PlayerCoin = Player.GetComponent<Player>().SetCoin();
        //CurrentChaUpgrade();
    }

    private void CurrentChaUpgrade()
    {
        PlayerUpgradeState[0].text = UpgradePrice_Cha_Hp + "¿ø";
        PlayerUpgradeState[1].text = UpgradePrice_Cha_Jump + "¿ø";
        PlayerUpgradeState[2].text = UpgradePrice_Cha_Speed + "¿ø";
    }

    public void OnClickBuyWeapon(int Num)
    {
        if (WeaponBuy[Num] == false)
        {
            if (PlayerCoin >=BuyPrice[Num])
            {
                Player.GetComponent<Player>().BuyWeapon(Num+1);
                WeaponBuy[Num] = true;
                Player.GetComponent<Player>().GetCoin(BuyPrice[Num]);
            }
        }
     }



    public void OnClickWeaponDamageUp(int index)
    {
        Weapon[index].GetComponent<Weapon>().DamageUp(UpAmount);
    }

    public void BuyBullet(int WeaponNum, int BullCount)
    {
        Weapon[WeaponNum].GetComponent<Weapon>().BuyBullet(BullCount);
    }

    public void PlayerSpeedUp()
    {
        if (PlayerCoin >= UpgradePrice_Cha_Speed)
        {
            Player.GetComponent<Player>().SpeedUp();
        }
        UpgradePrice_Cha_Speed += 100;
    }

    public void PlayerHpUp()
    {
        if (PlayerCoin >= UpgradePrice_Cha_Speed)
        {
            Player.GetComponent<Player>().HpUp();
        }
        UpgradePrice_Cha_Hp += 100;
    }

    public void PlayerJumpUp()
    {
        if (PlayerCoin >= UpgradePrice_Cha_Speed)
        {
            Player.GetComponent<Player>().JumpForceUp();
        }
        UpgradePrice_Cha_Jump += 100;
    }
}
