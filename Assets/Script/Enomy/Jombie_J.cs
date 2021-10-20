using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jombie_J : Jombie
{
    private float delaytime;

    private Rigidbody rigid;
    private void Awake()
    {
        delaytime = 4f;
        rigid = GetComponent<Rigidbody>();
    }
    protected override void Update()
    {
        base.Update();
        Jump();
    }
    protected void Jump()
    {
        if (Vector3.Distance(player.transform.position, this.transform.position) <= 10.0f)
        {
            delaytime += Time.deltaTime;
            if (delaytime >= 4.0f)
            {
                rigid.velocity = this.transform.up * 15f;
                delaytime = 0;
            }
        }
    }
}
