using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    [SerializeField] private int Damage =0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            other.GetComponent<Jombie>().Damage(Damage);
        }
    }

}
