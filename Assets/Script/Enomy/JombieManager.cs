using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JombieManager : MonoBehaviour
{
    [SerializeField] private GameObject[] jombieprefab = null;
    private float interval = 3f;
    public Player pplayer = null;
    int _X;
    float Xrange = 0;
    int _Z;
    float Zrange = 0;

    private void Start()
    {
        _X = Random.Range(0, 2);
        _Z = Random.Range(0, 2);
        InvokeRepeating("Instan_x", interval, interval);
        InvokeRepeating("Instan_z", interval, interval);
    }


    public void Instan_x()
    {
        for (int i = 0; i < 2; i++)
        {
            if (_X == 0) Xrange = 50f;
            else Xrange = -50f;
            int index = Random.Range(0, 5);
            GameObject jombie = Instantiate(jombieprefab[index]);
            jombie.GetComponent<Jombie>().player = pplayer;
            float posZ = Random.Range(-50f, 50f);
            Vector3 pos = new Vector3(Xrange, 0f, posZ);
            jombie.transform.position = pos;
            _X = Random.Range(0, 2);
        }
    }
    public void Instan_z()
    {
        for (int i = 0; i < 2; i++)
        {
            if (_Z == 0) Zrange = 50f;
            else Zrange = -50f;
            int index = Random.Range(0, 5);
            GameObject jombie = Instantiate(jombieprefab[index]);
            jombie.GetComponent<Jombie>().player = pplayer;

            float posX = Random.Range(-50f, 50f);
            Vector3 pos = new Vector3(posX, 0f, Zrange);
            jombie.transform.position = pos;
            _Z = Random.Range(0, 2);
        }
    }
}
