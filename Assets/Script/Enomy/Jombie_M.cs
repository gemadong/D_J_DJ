using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jombie_M : Jombie
{
    public GameObject jombie_N;
    Vector3 diepos;
    
    protected override void Update()
    {
        base.Update();
        if (state== JombieState.Die)
        {
            DieMotion();
        }
       
    }

    void DieMotion()
    {
        diepos = gameObject.transform.position;
        for (int i = 0; i < 4; i++)
        {
            Instantiate(jombie_N, diepos, Quaternion.identity);
        }
    }
}
