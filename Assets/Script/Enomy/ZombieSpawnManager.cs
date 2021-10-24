using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnManager : MonoBehaviour
{
  
    public GameObject _jombie;


    private bool SpawnFinish = false;
    private int ZombieNcount = 20;
    private float ZombieNProbablity = 0;
    private float ZombieSProbablity = 0;
    private float ZombieTProbablity = 0;
    private float ZombieMProbablity = 0;
    private float ZombieJProbablity = 0;

    private bool NightStart = false;

    List<Jombie> Zombies = new List<Jombie>();

    void Start()
    {
       Invoke("SpawnJombie",2f);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("좀비개수" + Zombies.Count);
        if (Zombies.Count == 0 && NightStart == true) GameManager.instance.StageClear();
    }


    private void SpawnJombie()
    {
        GameObject Zom = Instantiate(_jombie, this.transform.position, Quaternion.identity);
        Zombies.Add(Zom.GetComponent<Jombie>());

        Zom.GetComponent<Jombie>().onDeath += () => Zombies.Remove(Zom.GetComponent<Jombie>());
        NightStart = true;
    }
}
