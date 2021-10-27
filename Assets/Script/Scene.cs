using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public GameObject KeyI = null;
    bool Life = true;

    public void KeyLife()
    {
        if (Life == true)
        {
            KeyI.SetActive(true);
            Life = false;
        }
        else
        {
            KeyI.SetActive(false);
            Life = true;
        }

    }
    public void OnclickS1()
    {
        SceneManager.LoadScene(1);
    }
}
