using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerGun : Weapon
{

    private float LazerTime = 0;
    private LineRenderer lr = null;
    private bool LazerOnGoing = false;
    [SerializeField] private GameObject LazerBullet = null;

    private Vector3 LazerPos;
    void Awake()
    {
        this.type = Type.Gun;
        AtkDelay = 0f;
        lr = this.GetComponent<LineRenderer>();
        lr.enabled = false;
        currentbullet = 5;
        CanhaveMaxCount = 30;
        HaveBulletInPocket = 90;
        LazerBullet.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        LazerBullet.transform.position = LazerPos;
        if (currentbullet > 0)
        {
            Attack();
            if (Input.GetMouseButtonDown(0))
            {
                LazerBullet.SetActive(true) ;
                lr.enabled = true;
                LazerOnGoing = true;
                Pr.Play();
               
            }
            else if (Input.GetMouseButtonUp(0))
            {
                LazerBullet.SetActive(false);
                lr.enabled = false;
                LazerOnGoing = false;
                LazerTime = 0;
                Pr.Stop();
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
                LazerPos = hit.point;
              

            }
        }
        else
        {
            lr.SetPosition(1, BulletPos.transform.forward * 5000);
            LazerPos = BulletPos.transform.forward * 5000;

        }
    }

}


