using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Bullinstance;
    [SerializeField] private GameObject BulletPrefab = null;

    Queue<GameObject> pollingObjQueue = new Queue<GameObject>();

    private void Awake()
    {
        Bullinstance = this;
        Initialize();
        
    }

    private void Update()
    {
        Debug.Log(Bullinstance.pollingObjQueue.Count);
    }
    private GameObject CreatBullet()
    {
        GameObject Bullet_ = Instantiate(BulletPrefab); 
          Bullet_.gameObject.SetActive(false);
        return Bullet_;
    }
    private void Initialize()
    {
        for(int i=0;i<30;i++)
        pollingObjQueue.Enqueue(CreatBullet());
    }
    public static GameObject InsBullet()
    {
        Debug.Log("Step1");
        if (Bullinstance.pollingObjQueue.Count > 0)
        {
            Debug.Log("Step2");
            GameObject bull = Bullinstance.pollingObjQueue.Dequeue();
            bull.SetActive(true);
            return bull; 
        }       
        else
        {
            Debug.Log("Step3");
            GameObject bull = Bullinstance.CreatBullet();
            bull.SetActive(true);
            return bull;

        }
    }

    public static void Returnbullet(GameObject bullet)
    {
        bullet.gameObject.SetActive(false);
        Bullinstance.pollingObjQueue.Enqueue(bullet);
    }
}
