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

    public int StageNum = 1;

    private void Awake()
    {
//<<<<<<< Updated upstream
//     //   Cursor.lockState = CursorLockMode.Locked;
//=======
//        //Cursor.lockState = CursorLockMode.Locked;
//>>>>>>> Stashed changes
        if (instance == null)instance = this;
        else instance = this;
        //isNight = true;
    }
   

    private void Update()
    {
        
        if (isNight)
        {
            Debug.Log("¹ã!");
            Night();
        }
        if (!isNight)
        {
            Debug.Log(DayTime);
            Debug.Log("³·!");
            
            Day();
        }
    }
    void Night()
    {
        Cam[0].SetActive(true);
        Cam[1].SetActive(false);
        Vector3 rotNight = new Vector3(-90, 0, 0);
        Sun.transform.rotation = Quaternion.Euler(rotNight);
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
}
