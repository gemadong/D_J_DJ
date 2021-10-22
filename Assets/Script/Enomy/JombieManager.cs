using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JombieManager : MonoBehaviour
{
    [SerializeField] private GameObject[] jombieprefab = null;
    public Player pplayer = null;
    float StageTime = 7f;
    float delayTime = 1f;
    int X;
    float Xrange = 0;
    float Zrange = 0;
    GameObject[] jombie = null;

    public enum Stage
    {
        A,B,C,D,E
    }
    public Stage stage;
    private void Start()
    {
        if (GameManager.isNight)
        {
           delayTime -= Time.deltaTime;
            switch (stage)
            {
                case Stage.A:
                    A();
                    break;
                case Stage.B:
                    B();
                    break;
                case Stage.C:
                    C();
                    break;
                case Stage.D:
                    D();
                    break;
                case Stage.E:
                    E();
                    break;
            }
        }
    }

    private void A()
    {
        for(int i =0;i<5; i++)
        {
            if(delayTime <= 0)
            {
                ZomN();
                delayTime = 1f;
            }
        }

        if (StageTime <= 0)
        { 
            GameManager.isNight = false;
            StageTime = 7f;
            stage = Stage.B;
        }
    }
    private void B()
    {

    }
    private void C()
    {

    }
    private void D()
    {

    }
    private void E()
    {

    }
    void ZomN()
    {
        X = Random.Range(0, 4);
        if (X < 2)
        {
            if (X == 0) Xrange = 50f;
            else Xrange = -50f;
            GameObject jombie = Instantiate(jombieprefab[0]);
            jombie.GetComponent<Jombie>().player = pplayer;
            float posZ = Random.Range(-50f, 50f);
            Vector3 pos = new Vector3(Xrange, 0f, posZ);
            jombie.transform.position = pos;
            X = Random.Range(0, 4);
        }
        else
        {
            if (X == 2) Zrange = 50f;
            else Zrange = -50f;
            GameObject jombie = Instantiate(jombieprefab[0]);
            jombie.GetComponent<Jombie>().player = pplayer;
            float posX = Random.Range(-50f, 50f);
            Vector3 pos = new Vector3(posX, 0f, Zrange);
            jombie.transform.position = pos;
            X = Random.Range(0, 4);
        }
    }


}
