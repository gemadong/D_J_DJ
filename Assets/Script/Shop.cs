using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject Player = null;          
    [SerializeField] private GameObject[] Weapon = null;    //업그레이드 할 무기 변수

    [SerializeField] private Text[] PlayerUpgradeState = null; //플레이어 업그레이드 상태 표시
    [SerializeField] private Text[] WeaponUpgradeState = null; // 무기 업그레이드 상태 표시

    [SerializeField] private int[] BuyPrice; //무기 구매 가격

    [SerializeField] private int[] UpgradePrice; //무기 공격력 업그레이드 비용
    [SerializeField] private int[] BuyBulletPrice = null; //무기별 총알 구매 가격

    [SerializeField] private GameObject[] BuyWeaponButtom = null;
    [SerializeField] private GameObject[] BuyBulletButton = null;
    [SerializeField] private GameObject UIButtom = null;

   [SerializeField] private bool[] WeaponBuy;

    private int PlayerCoin = 0;

    public int UpAmount = 0;

    private int UpgradePrice_Cha_Speed = 100;
    private int UpgradePrice_Cha_Hp = 100;
    private int UpgradePrice_Cha_Jump = 100;



    
    private void Update()
    {
        PlayerCoin = Player.GetComponent<Player>().SetCoin();
        Debug.Log(PlayerCoin);
        //CurrentChaUpgrade();
       
    }



    /// ///////////////////////////////////
    // 무기 구매 및 업그레이드
    public void OnClickBuyWeapon(int Num)
    {
        if (WeaponBuy[Num] == false)
        {
            if (PlayerCoin >=BuyPrice[Num])
            {
                Player.GetComponent<Player>().BuyWeapon(Num+1);
                WeaponBuy[Num] = true;
                BuyWeaponButtom[Num].SetActive(false);
                BuyBulletButton[Num].SetActive(true);
                Player.GetComponent<Player>().PlayerCoinMinus(BuyPrice[Num]);
            }
        }
     }

    public void OnClickWeaponDamageUp(int index)
    {

        if (PlayerCoin >= UpgradePrice[index])
        {
            Weapon[index].GetComponent<Weapon>().DamageUp();
            Debug.Log("업그레이드");
            Player.GetComponent<Player>().PlayerCoinMinus(UpgradePrice[index]);
            UpgradePrice[index] += 100;
            WeaponUpgradeState[index].text = UpgradePrice[index].ToString() +("원");
        }
    }

    public void BuyBullet(int WeaponNum)
    {
        Weapon[WeaponNum].GetComponent<Weapon>().BuyBullet();
        Player.GetComponent<Player>().PlayerCoinMinus(BuyBulletPrice[WeaponNum]);
    }

    /// /////////////////////////////////////////////////
    // 플레이어 업그레이드
    public void PlayerSpeedUp()
    {
        if (PlayerCoin >= UpgradePrice_Cha_Speed)
        {
            Player.GetComponent<Player>().SpeedUp();
            Player.GetComponent<Player>().PlayerCoinMinus(UpgradePrice_Cha_Speed);
        UpgradePrice_Cha_Speed += 100;
        }
        PlayerUpgradeState[1].text = UpgradePrice_Cha_Speed + "원";
    }

    public void PlayerHpUp()
    {
        
        if (PlayerCoin >= UpgradePrice_Cha_Hp)
        {
            Player.GetComponent<Player>().HpUp();
            Player.GetComponent<Player>().PlayerCoinMinus(UpgradePrice_Cha_Hp);
        UpgradePrice_Cha_Hp += 100;
        }
        PlayerUpgradeState[0].text = UpgradePrice_Cha_Hp + "원";
    }

    public void PlayerJumpUp()
    {
        if (PlayerCoin >= UpgradePrice_Cha_Jump)
        {
            Player.GetComponent<Player>().JumpForceUp();
            Player.GetComponent<Player>().PlayerCoinMinus(UpgradePrice_Cha_Jump);
        UpgradePrice_Cha_Jump += 100;
        }
        PlayerUpgradeState[2].text = UpgradePrice_Cha_Jump + "원";
    }

    public void OnClickBuyturret(int Num)
    {
        if (Num == 0)
        {
            if (PlayerCoin >= 2500)
            {
                UIButtom.GetComponent<BuildManager>().TurretHasCountUp(Num);
                Player.GetComponent<Player>().PlayerCoinMinus(2500);
            }
        }
        else if (Num == 1)
        {
            if (PlayerCoin >= 3000)
            {
                UIButtom.GetComponent<BuildManager>().TurretHasCountUp(Num);
                Player.GetComponent<Player>().PlayerCoinMinus(3000);
            }
        }
    }
}
