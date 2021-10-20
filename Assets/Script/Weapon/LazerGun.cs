using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerGun : Weapon
{
    
  
    private LineRenderer lr = null;
    void Awake()
    {
        AtkDelay = 0f;
        lr = this.GetComponent<LineRenderer>();
        lr.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        if (Input.GetMouseButtonDown(0)) {
            Bullet.SetActive(true);
            lr.enabled = true;
                }
        else if (Input.GetMouseButtonUp(0))
        {
            Bullet.SetActive(false);
            lr.enabled = false;
        }
       
    }

    public override void Attack()
    {
        lr.SetPosition(0, BulletPos.position);
        RaycastHit hit;
        if (Physics.Raycast(BulletPos.transform.position, BulletPos.transform.forward, out hit))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
                Debug.Log(hit.collider.tag);
            }
        }
        else lr.SetPosition(1, BulletPos.transform.forward * 5000);
    }
}
    //public override void Attack()
    //{
    //    lr.SetPosition(0, BulletPos.position);
    //    RaycastHit hit;
    //    if (Physics.Raycast(BulletPos.position, BulletPos.forward, out hit))
    //    {
    //        if (hit.collider)
    //        {
    //            Bullet.transform.position = hit.point;
    //            lr.SetPosition(1, hit.point);
    //        }
    //    }
    //    else lr.SetPosition(1, BulletPos.forward * 5000);
    //    Bullet.transform.position = hit.point;
    //}

