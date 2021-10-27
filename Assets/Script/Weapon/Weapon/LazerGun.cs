using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerGun : Weapon
{

    private float LazerTime = 0;
    private LineRenderer lr = null;
    private bool LazerOnGoing = false;
    [SerializeField] private GameObject LazerBullet = null;
    private SphereCollider Sc = null;

    private bool isFire = false;
    private float TIme_ = 0;

    private Vector3 LazerPos;
    void Awake()
    {
        Sc = LazerBullet.GetComponent<SphereCollider>();
        this.type = Type.Gun;
        AtkDelay = 0f;
        lr = this.GetComponent<LineRenderer>();
        lr.enabled = false;
        currentbullet = 5;
        CanhaveMaxCount = 30;
        HaveBulletInPocket = 90;
        LazerBullet.SetActive(false);
        AtkDamage = 3;
        AtkDamage = 2;
    }

    // Update is called once per frame
    void Update()
    {

        if (isFire)
        {
            StartCoroutine("LazerShoot");
            TIme_ += Time.deltaTime;
           
        }
        else
        {
            StopCoroutine("LazerShoot");
            TIme_ = 0;
        }


        LazerBullet.transform.position = LazerPos;
        if (currentbullet > 0)
        {
            Attack();
            if (Input.GetMouseButtonDown(0))
            {
                LazerBullet.SetActive(true) ;
                lr.enabled = true;
                isFire = true;
                LazerOnGoing = true;
                Pr.Play();
               
            }
            else if (Input.GetMouseButtonUp(0))
            {
                LazerBullet.SetActive(false);
                lr.enabled = false;
                isFire = false;
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

    IEnumerator LazerShoot()
    {
        Sc.enabled = true;

        yield return new WaitForSeconds(0.5f);
        Sc.enabled = false;

        yield return new WaitForSeconds(0.5f);

    }

}


