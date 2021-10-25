using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    [SerializeField] private GameObject Player = null;
    [SerializeField] private GameObject[] Weapon = null;
    [SerializeField] private Text[] PlayerUpgradeState = null;

    /// ////////////////////////////
    private int PlayerCoin = 0;

    /// /////////////////////////////
    private int UpgradePrice_Cha_Speed = 100;
    private int UpgradePrice_Cha_Hp = 100;
    private int UpgradePrice_Cha_Jump = 100;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerCoin = Player.GetComponent<Player>().Coin;
        CurrentChaUpgrade();
    }

    private void CurrentChaUpgrade()
    {
        PlayerUpgradeState[0].text = UpgradePrice_Cha_Hp + "¿ø";
        PlayerUpgradeState[1].text = UpgradePrice_Cha_Jump + "¿ø";
        PlayerUpgradeState[2].text = UpgradePrice_Cha_Speed + "¿ø";
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

    public void BuyBullet(int WeaponNum, int BullCount)
    {
        Weapon[WeaponNum].GetComponent<Weapon>().BuyBullet(BullCount);
    }

    public void WeaponDamageUp(int WeaponNum, int UpDamage)
    {
        Weapon[WeaponNum].GetComponent<Weapon>().AtkDamageUp(UpDamage);
    }

}
