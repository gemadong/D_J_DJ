using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform Sun = null;
    public static GameManager instance;
    public List<Player> players;
    //bool isNight;

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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("¹ã!");
            Night();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("³·!");
            Day();
        }
    }
    void Night()
    {
        //isNight = true;
        Vector3 rotNight = new Vector3(-90, 0, 0);
        Sun.transform.rotation = Quaternion.Euler(rotNight);
    }
    void Day()
    {
        //isNight = false;
        Vector3 rotDay = new Vector3(45, 45, 0);
        Sun.transform.rotation = Quaternion.Euler(rotDay);
    }
}
