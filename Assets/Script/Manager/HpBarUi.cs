using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarUi : MonoBehaviour
{
    public static HpBarUi Hpbarui;

    [SerializeField] private Text[] Hptext = null;
    [SerializeField] private Slider[] HpSlider = null;
    [SerializeField] private GameObject[] BossHp = null;

    private float Boss1Hp = 0;
    private float Boss2Hp = 0;

    private bool isBoss1 = false;
    private bool isBoss2 = false;

    void Awake()
    {
        if (null == Hpbarui) Hpbarui = this;
        else Hpbarui = this;

    }
    // Update is called once per frame
    void Update()
    {
        Hptext[0].text = "보스1 HP : " + Boss1Hp.ToString();
        Hptext[1].text = "보스2 HP : " + Boss2Hp.ToString();
        HpSlider[0].value = Boss1Hp / 50;
        Debug.Log(Boss2Hp);
        HpSlider[1].value = Boss2Hp / 100;
    }

    public void GetBoss1Hp(float value)
    {
        Boss1Hp = value;
    }

    public void GetBoss2Hp(float value)
    {
        Boss2Hp = value;
    }

    public void OpenBoss1Hp()
    {
        BossHp[0].SetActive(true);
    }

    public void OpenBoss2Hp()
    {
        BossHp[1].SetActive(true);
    }

    public void CloseBoss1Hp()
    {
        BossHp[0].SetActive(false);
    }

    public void CloseBoss2Hp()
    {
        BossHp[1].SetActive(false);
    }

}
