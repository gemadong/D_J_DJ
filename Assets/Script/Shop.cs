using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject Player = null;          
    [SerializeField] private GameObject[] Weapon = null;    //���׷��̵� �� ���� ����

    [SerializeField] private Text[] PlayerUpgradeState = null; //�÷��̾� ���׷��̵� ���� ǥ��
    [SerializeField] private Text[] WeaponUpgradeState = null; // ���� ���׷��̵� ���� ǥ��

    [SerializeField] private int[] BuyPrice; //���� ���� ����

    [SerializeField] private int[] UpgradePrice; //���� ���ݷ� ���׷��̵� ���
    [SerializeField] private int[] BuyBulletPrice = null; //���⺰ �Ѿ� ���� ����

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
    // ���� ���� �� ���׷��̵�
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
            Debug.Log("���׷��̵�");
            Player.GetComponent<Player>().PlayerCoinMinus(UpgradePrice[index]);
            UpgradePrice[index] += 100;
            WeaponUpgradeState[index].text = UpgradePrice[index].ToString() +("��");
        }
    }

    public void BuyBullet(int WeaponNum)
    {
        Weapon[WeaponNum].GetComponent<Weapon>().BuyBullet();
        Player.GetComponent<Player>().PlayerCoinMinus(BuyBulletPrice[WeaponNum]);
    }

    /// /////////////////////////////////////////////////
    // �÷��̾� ���׷��̵�
    public void PlayerSpeedUp()
    {
        if (PlayerCoin >= UpgradePrice_Cha_Speed)
        {
            Player.GetComponent<Player>().SpeedUp();
            Player.GetComponent<Player>().PlayerCoinMinus(UpgradePrice_Cha_Speed);
        UpgradePrice_Cha_Speed += 100;
        }
        PlayerUpgradeState[1].text = UpgradePrice_Cha_Speed + "��";
    }

    public void PlayerHpUp()
    {
        
        if (PlayerCoin >= UpgradePrice_Cha_Hp)
        {
            Player.GetComponent<Player>().HpUp();
            Player.GetComponent<Player>().PlayerCoinMinus(UpgradePrice_Cha_Hp);
        UpgradePrice_Cha_Hp += 100;
        }
        PlayerUpgradeState[0].text = UpgradePrice_Cha_Hp + "��";
    }

    public void PlayerJumpUp()
    {
        if (PlayerCoin >= UpgradePrice_Cha_Jump)
        {
            Player.GetComponent<Player>().JumpForceUp();
            Player.GetComponent<Player>().PlayerCoinMinus(UpgradePrice_Cha_Jump);
        UpgradePrice_Cha_Jump += 100;
        }
        PlayerUpgradeState[2].text = UpgradePrice_Cha_Jump + "��";
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
