using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boomb : MonoBehaviour
{
    private int Damage = 6;
    void Start()
    {
        Destroy(this.gameObject, 0.8f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            other.GetComponent<Jombie>().Damage(Damage);
            Destroy(this.gameObject,0.5f);
        }

    }
}
