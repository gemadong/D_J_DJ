using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerGun : Weapon
{

    private float LazerTime = 0;
    private LineRenderer lr = null;
    private bool LazerOnGoing = false;
    void Awake()
    {
        this.type = Type.Gun;
        AtkDelay = 0f;
        lr = this.GetComponent<LineRenderer>();
        lr.enabled = false;
        currentbullet = 5;
        CanhaveMaxCount = 30;
        HaveBulletInPocket = 90;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentbullet);

        if (currentbullet > 0)
        {
            Attack();
            if (Input.GetMouseButtonDown(0))
            {
                Bullet.SetActive(true);
                lr.enabled = true;
                LazerOnGoing = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Bullet.SetActive(false);
                lr.enabled = false;
                LazerOnGoing = false;
                LazerTime = 0;
            }
        }

    }
    private void LazerBulletDown()
    {
        currentbullet--;
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
            }
        }
        else lr.SetPosition(1, BulletPos.transform.forward * 5000);
    }

}


