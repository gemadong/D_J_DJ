using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   
    public Transform Sun = null;
    public GameObject[] Cam = null;
    public static GameManager instance;
    public List<Player> players;
    public static bool isNight = true;
    public int stage=0;
    [SerializeField] private Text zombiecount;
    [SerializeField] private Text Stagecount;
    [SerializeField] private Text DayTime_T;

    /// <summary>
    /// //////////////////////////////////////
    /// </summary>

    public GameObject[] zombies;
    public GameObject[] boss;
    [SerializeField] private GameObject ZombieSpawnManager = null;
    public List<int> zombieList;
    public List<int> bossList;
    private bool DayStart_ = false;
    private float DayTime_ = 0;

    private void Awake()
    {
        Invoke("Day_", 3.0f);
        Day();

        if (null == instance) instance = this;
        else instance = this;

        zombieList = new List<int>();

    }

    private void Update()
    {
        if(DayStart_)DayStart();

        zombiecount.text = ZombieSpawnManager.GetComponent<ZombieSpawnManager>().Zombies.Count.ToString();
        Stagecount.text = "Stage : " + stage.ToString();
        DayTime_T.text = string.Format("{0:N0}", DayTime_);
    }

    
    /// /////////////////////////////////
    void Night()
    {
        Cam[0].SetActive(true);
        Cam[1].SetActive(false);
        Vector3 rotNight = new Vector3(-90, 0, 0);
        Sun.transform.rotation = Quaternion.Euler(rotNight);
        //StageStart();
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Day()
    {
        Cursor.lockState = CursorLockMode.None;
        Cam[0].SetActive(false);
        Cam[1].SetActive(true);
        Vector3 rotDay = new Vector3(45, 45, 0);
        Sun.transform.rotation = Quaternion.Euler(rotDay);
    }

    /// ///////////////////////////////

    public void StageClear() 
    {

        if (stage == 6) SceneManager.LoadScene(3);


        DayStart_ = true;
        Day();
        Debug.Log("Ŭ����!");
        Debug.Log("�Ϸ����");
    }

    public void DayStart()
    {
        DayTime_ += Time.deltaTime;
        DayTime_T.gameObject.SetActive(true);
        if(DayTime_ >= 20)
        {
            DayStart_ = false;
            stage++;
            ZombieSpawnManager.GetComponent<ZombieSpawnManager>().ZombieSpawnStart();
            ZombieSpawnManager.GetComponent<ZombieSpawnManager>().SpawnFinish = false;
            Night();
            DayTime_T.gameObject.SetActive(false);
            DayTime_ = 0;
        }
    }

    public void Day_()
    {
        DayStart_ = true;
        
    }

    public int StageNum()
    {
        return stage;
    }
    
  
}
