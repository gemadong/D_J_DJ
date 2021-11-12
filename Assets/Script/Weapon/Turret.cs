using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    //�ͷ��� ȸ���� �ٵ�κ�
    [SerializeField] private Transform TurretBody = null;

    // �ͷ��� ���� ȸ���� ���
    [SerializeField] private Transform TurretHead = null;

    // �ͷ��� �����Ÿ�
    [SerializeField] private float TurretRange = 0f;

    //��ž�� Ž���� ��
    [SerializeField] LayerMask tLayerMask = 0;

    [SerializeField] private Transform BulletPos1 = null;
    [SerializeField] private Transform BulletPos2 = null;

    [SerializeField] private GameObject Turretbullet = null;

    //������ ����� ��ġ
    private Transform FinalTarget = null;

    private bool isShoot = true;

    [SerializeField] private int bulletNum = 0;

    //�ͷ��� ��ǥ���� ���� ȸ���� ���ǵ�
    [SerializeField]private float SpinSpeed = 0;

    private bool FindTarget = false;

    void Start()
    {
        InvokeRepeating("SearchEnomy", 0f, 0.5f);
    }


    void Update()
    {
        if(FinalTarget == null)
        {

            TurretBody.Rotate((new Vector3(0, 45f, 0)*Time.deltaTime));
            TurretHead.Rotate((new Vector3(0, 45f, 0) * Time.deltaTime));
            //Ÿ���� ���� �� �ͷ� ȸ��
        }
        else
        {
            Quaternion LookTarget = Quaternion.LookRotation(FinalTarget.position-this.transform.position);
            //Ÿ�ٰ� �ڽ��� ��ġ ���� ���� ȸ������ �ش�
            Vector3 t_euler = Quaternion.RotateTowards(TurretBody.rotation, LookTarget, SpinSpeed*Time.deltaTime).eulerAngles;
            Vector3 t_eulerX = Quaternion.RotateTowards(TurretHead.rotation, LookTarget, SpinSpeed * Time.deltaTime).eulerAngles;
            float XRot = t_eulerX.x;
            if (XRot > 180f) XRot = Mathf.Clamp(XRot, 310f, 361f);
            else XRot = Mathf.Clamp(XRot, -1, 60);

            //�ͷ��� X�� ȸ���� ������ �ξ 360���� ���ư��� ���� ���Ҵ�.

            TurretBody.rotation = Quaternion.Euler(0, t_euler.y, 0);
           TurretHead.rotation =Quaternion.Euler(XRot, t_euler.y,0);
            // Debug.Log("y��" + TurretHead.rotation.y);

            if (isShoot) TurretShoot();
        }

    }

    private void SearchEnomy()
    {
        Collider[] S_cols = Physics.OverlapSphere(this.transform.position, TurretRange, tLayerMask);
        //OverlapSphere : ��ü �ֺ��� Collider�� ����
        //������ collider�� �迭�� ������ ����
      
        Transform ShortestTarget = null;
        //���� ª�� �Ÿ��� ������Ʈ ��ġ�� ���� ����

        if (S_cols.Length > 0) //�迭�� ���� ��, ������ collider�� �ϳ� �̻� �� �� �۵�
        {
            float s_shortestDistance = Mathf.Infinity;
            // �Ÿ���꿡 ����� ���� ����.
            foreach(Collider s_coltaget in S_cols) // foreach���� �̿��� �迭�� ���̸�ŭ ���̺� ����
            {  
                float s_distance = Vector3.SqrMagnitude(this.transform.position - s_coltaget.transform.position);
                // �ͷ��� ����� collider���� �Ÿ��� ���� ��������
                // Vector3.Distance�� Vector3.magnitude�� �Ÿ��񱳸� �� �� ������ �� ���� Root�� ���� ���� �Ÿ��� ����ϱ� ������ ������ �� ����.
                //SqrMagnitude�� �����Ÿ�*�����Ÿ��� Root�� ������ �ʴ� �Լ��� �ܼ� �Ÿ����� ���� �̰��� ���� �� ���� �ӵ��� ������.
               
                if (s_shortestDistance > s_distance)
                {
                    s_shortestDistance = s_distance;
                    ShortestTarget = s_coltaget.transform;
                }
            }
        }
        FinalTarget = ShortestTarget;
        //�ݺ����� �� ������ ���� ���� �Ÿ��� ª�� ����� ���� Ÿ������ �����Ѵ�.
    }

    public void TurretShoot()
    {
        Debug.Log("��");
        isShoot = false;
        if (bulletNum == 1)
        {
            //var bullet = BulletPool.Turret1Ins();
            //bullet.transform.position = BulletPos1.position;
            //bullet.transform.forward = BulletPos1.forward;
            var bullet = Instantiate(Turretbullet, BulletPos1.position, transform.rotation);
            bullet.GetComponent<Bullet>().transform.forward = BulletPos1.transform.forward;
            StartCoroutine("ShootDelay");
        }
        else
        {


            var bullet = Instantiate(Turretbullet, BulletPos1.position, transform.rotation);
            bullet.GetComponent<Bullet>().transform.forward = BulletPos1.transform.forward;
            var bullet2 = Instantiate(Turretbullet, BulletPos2.position, transform.rotation);
            bullet2.GetComponent<Bullet>().transform.forward = BulletPos2.transform.forward;
            //var bullet = BulletPool.Turret2Ins();
            //bullet.transform.position = BulletPos1.position;
            //bullet.transform.forward = BulletPos1.forward;

            //var bullet2 = BulletPool.Turret2Ins();
            //bullet2.transform.position = BulletPos2.position;
            //bullet2.transform.forward = BulletPos2.forward;
            StartCoroutine("ShootDelay2");
        }
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(0.8f);
        isShoot = true;
    }
    IEnumerator ShootDelay2()
    {
        yield return new WaitForSeconds(0.35f);
        isShoot = true;
    }

 


}
