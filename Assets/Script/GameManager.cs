using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform Sun = null;
    public GameObject[] Cam = null;
    public static GameManager instance;
    public List<Player> players;
    public static bool isNight = true;
    public float DayTime = 5f;
    public int stage=1;
    public bool isBattle;
    public float Zrange;
    public float Xrange;
    public int CntN;
    public int CntS;
    public int CntT;
    public int CntJ;
    public int CntM;
    public int CntBos;

    public GameObject[] zombies;
    public GameObject[] boss;
    public List<int> zombieList;
    public List<int> bossList;


    
    private void Awake()
    {
        //<<<<<<< Updated upstream
        //     //   Cursor.lockState = CursorLockMode.Locked;
        //=======
        //        //Cursor.lockState = CursorLockMode.Locked;
        //>>>>>>> Stashed changes
        if (instance == null) instance = this;
        else instance = this;
        //isNight = true;
        zombieList = new List<int>();
        Night();
    }
    void Night()
    {
        Cam[0].SetActive(true);
        Cam[1].SetActive(false);
        Vector3 rotNight = new Vector3(-90, 0, 0);
        Sun.transform.rotation = Quaternion.Euler(rotNight);
        StageStart();
    }
    void Day()
    {
        Cam[0].SetActive(false);
        Cam[1].SetActive(true);
        Vector3 rotDay = new Vector3(45, 45, 0);
        Sun.transform.rotation = Quaternion.Euler(rotDay);
        if (DayTime < 0)
        {
            Night();
            DayTime = 5f;
        }
    }

    public void StageStart()
    {
        StartCoroutine(InBattle());
    }
    public void StageEnd()
    {
        Day();
        stage++;
    }
    IEnumerator InBattle()
    {
        if (stage % 3 == 0)
        {
            CntBos++;
            int ranZone = Random.Range(0, 4);
            if (ranZone < 2)
            {
                Debug.Log("社発");
                if (ranZone == 0) Xrange = 50f;
                else Xrange = -50f;
                GameObject Bos = Instantiate(boss[0]);
                Bos.GetComponent<Bos_1>().target = instance.transform;
                float posZ = Random.Range(-50f, 50f);
                Vector3 pos = new Vector3(Xrange, 0f, posZ);
                Bos.transform.position = pos;
                yield return new WaitForSeconds(2f);
            }
            else
            {
                Debug.Log("社発");
                if (ranZone == 2) Zrange = 50f;
                else Zrange = -50f;
                GameObject Bos = Instantiate(boss[0]);
                Bos.GetComponent<Bos_1>().target = instance.transform;
                float posX = Random.Range(-50f, 50f);
                Vector3 pos = new Vector3(posX, 0f, Zrange);
                Bos.transform.position = pos;
                yield return new WaitForSeconds(2f);
            }

        }
        else
        {
            for (int i = 0; i < stage*5; i++)
            {
                int ran = Random.Range(0, 5);
                zombieList.Add(ran);

                switch (ran)
                {
                    case 0:
                        CntN++;
                        break;
                    case 1:
                        CntS++;
                        break;
                    case 2:
                        CntT++;
                        break;
                    case 3:
                        CntJ++;
                        break;
                    case 4:
                        CntM++;
                        break;
                }
            }

            while (zombieList.Count > 0)
            {
                int ranZone = Random.Range(0, 4);
                if (ranZone < 2)
                {
                    Debug.Log("社発");
                    if (ranZone == 0) Xrange = 50f;
                    else Xrange = -50f;
                    GameObject jombie = Instantiate(zombies[zombieList[0]]);
                    jombie.GetComponent<Jombie>().player = players[0];
                    float posZ = Random.Range(-50f, 50f);
                    Vector3 pos = new Vector3(Xrange, 0f, posZ);
                    jombie.transform.position = pos;
                    zombieList.RemoveAt(0);
                    yield return new WaitForSeconds(2f);
                }
                else
                {
                    Debug.Log("社発");
                    if (ranZone == 2) Zrange = 50f;
                    else Zrange = -50f;
                    GameObject jombie = Instantiate(zombies[zombieList[0]]);
                    jombie.GetComponent<Jombie>().player = players[0];
                    float posX = Random.Range(-50f, 50f);
                    Vector3 pos = new Vector3(posX, 0f, Zrange);
                    jombie.transform.position = pos;
                    zombieList.RemoveAt(0);
                    yield return new WaitForSeconds(2f);
                }
            }
            while (CntN + CntS + CntT + CntJ + CntM + CntBos > 0)
            {
                yield return null;
            }
            yield return new WaitForSeconds(4f);
            StageEnd();
        }
    }
}
