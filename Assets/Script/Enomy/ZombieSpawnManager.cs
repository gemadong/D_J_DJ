using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] ZombieSort = null;
    private bool SpawnFinish = false;
    private int ZombieNcount = 20;
    private float ZombieNProbablity = 0;
    private float ZombieSProbablity = 0;
    private float ZombieTProbablity = 0;
    private float ZombieMProbablity = 0;
    private float ZombieJProbablity = 0;

    void Start()
    {
        SpawnJombie();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnJombie()
    {
        GameObject Zombie = Instantiate(ZombieSort[0], this.transform.position, Quaternion.identity);

    }
}
