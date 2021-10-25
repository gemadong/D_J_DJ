using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnManager : MonoBehaviour
{
  
    [SerializeField] private GameObject[] ZombieSort = null;
    [SerializeField] private GameObject[] ZombieBossSort = null;
    [SerializeField] private Transform[] ZombieSpawnPos = null;
    // [SerializeField] private GameObject Player = null;
    [SerializeField] private Transform Player = null;

    public bool SpawnFinish = false;
    private int StageNum = 0;

    List<Jombie> Zombies = new List<Jombie>();

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Zombies.Count == 0 && SpawnFinish) GameManager.instance.StageClear();
    }

    public void ZombieSpawnStart()
    {
        StartCoroutine("ZombieSpawn");
    }

    IEnumerator ZombieSpawn()
    {
        StageNum = GameManager.instance.StageNum();
        for (int i = 0; i < (StageNum*5); i++)
        {
            int ZombieNum = Random.Range(0, StageNum%5);

            Debug.Log("좀비출현!"+i);
            Debug.Log("stage" + StageNum);
            int SpawnNum = Random.Range(0, 8);

            Vector3 SpawnPos = ZombieSpawnPos[SpawnNum].position;
            SpawnPos.y = 0;

            GameObject Zom = Instantiate(ZombieSort[ZombieNum], SpawnPos, Quaternion.identity);
            
            Zombies.Add(Zom.GetComponent<Jombie>());
            Zom.GetComponent<Jombie>().onDeath += () => Zombies.Remove(Zom.GetComponent<Jombie>());
            yield return new WaitForSeconds(1.0f);
        }

        if(StageNum%3==0)
        {
            GameObject ZombieBoss = Instantiate(ZombieBossSort[(StageNum/3)-1], ZombieSpawnPos[3].position, Quaternion.identity);
            Zombies.Add(ZombieBoss.GetComponent<Jombie>());
            ZombieBoss.GetComponent<Jombie>().onDeath += () => Zombies.Remove(ZombieBoss.GetComponent<Jombie>());
        }

        SpawnFinish = true;
    }


}
