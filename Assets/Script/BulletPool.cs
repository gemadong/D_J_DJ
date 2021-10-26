using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Bullinstance;
    [SerializeField] private GameObject[] BulletPrefab = null;

    Queue<GameObject> MachineGunPolling = new Queue<GameObject>();
    Queue<GameObject> Turret1Polling = new Queue<GameObject>();
    Queue<GameObject> Turret2Polling = new Queue<GameObject>();

    private void Awake()
    {
        Bullinstance = this;
    }
    private void Start()
    {
    Initialize();
        
    }
    private void Update()
    { 
    }
    private GameObject CreatBullet(int value)
    {
        GameObject Bullet_ = Instantiate(BulletPrefab[value]); 
          Bullet_.gameObject.SetActive(false);
        return Bullet_;
    }
    private void Initialize()
    {
        for (int i = 0; i < 30; i++)
        {
            
            MachineGunPolling.Enqueue(CreatBullet(0));
            Turret1Polling.Enqueue(CreatBullet(1));
            Turret2Polling.Enqueue(CreatBullet(2));
        }
     }
    public static GameObject MachinGunIns()
    {
        
        if (Bullinstance.MachineGunPolling.Count > 0)
        {
            
            GameObject bull = Bullinstance.MachineGunPolling.Dequeue();
            bull.SetActive(true);
            return bull;
        }
        else
        {
           
            GameObject bull = Bullinstance.CreatBullet(0);
            bull.SetActive(true);
            return bull;
        }
    }
    public static GameObject Turret1Ins()
    {
        Debug.Log("Step1");
        if (Bullinstance.Turret1Polling.Count > 0)
        {
            Debug.Log("Step2");
            GameObject bull = Bullinstance.Turret1Polling.Dequeue();
            bull.SetActive(true);
            return bull;
        }
        else
        {
            Debug.Log("Step3");
            GameObject bull = Bullinstance.CreatBullet(1);
            bull.SetActive(true);
            return bull;
        }
    }

    public static GameObject Turret2Ins()
    {
        Debug.Log("Step1");
        if (Bullinstance.Turret2Polling.Count > 0)
        {
            Debug.Log("Step2");
            GameObject bull = Bullinstance.Turret2Polling.Dequeue();
            bull.SetActive(true);
            return bull;
        }
        else
        {
            Debug.Log("Step3");
            GameObject bull = Bullinstance.CreatBullet(2);
            bull.SetActive(true);
            return bull;
        }
    }

            

    public static void Returnbullet(GameObject bullet)
    {
        bullet.gameObject.SetActive(false);
        bullet.transform.position = BulletPool.Bullinstance.gameObject.transform.position;
        Debug.Log("∏Æ≈œ");
        Bullinstance.MachineGunPolling.Enqueue(bullet);
    }
}
