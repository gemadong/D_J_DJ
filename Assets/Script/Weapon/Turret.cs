using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    //터렛이 회전할 바디부분
    [SerializeField] private Transform TurretBody = null;

    // 터렛의 사정거리
    [SerializeField] private float TurretRange = 0f;

    //포탑이 탐색할 적
    [SerializeField] LayerMask tLayerMask = 0;

    //공격할 대상의 위치
    private Transform FinalTarget = null;

    [SerializeField]private float SpinSpeed = 50.0f;

    void Start()
    {
        InvokeRepeating("SearchEnomy", 0f, 0.5f);
    }


    void Update()
    {
        if(FinalTarget == null)
        {
            TurretBody.Rotate((new Vector3(0, 45f, 0)*Time.deltaTime));
        }
        else
        {
            Quaternion LookTarget = Quaternion.LookRotation(FinalTarget.position-this.transform.position);
            Vector3 t_euler = Quaternion.RotateTowards(TurretBody.rotation, LookTarget, SpinSpeed*Time.deltaTime).eulerAngles;
            TurretBody.rotation = Quaternion.Euler(t_euler.x, t_euler.y, 0);
        }

    }

    private void SearchEnomy()
    {
        Collider[] S_cols = Physics.OverlapSphere(this.transform.position, TurretRange, tLayerMask);
        //OverlapSphere : 객체 주변의 Collider를 검출
        Transform ShortestTarget = null;
        if (S_cols.Length > 0)
        {
            float s_shortestDistance = Mathf.Infinity;
            foreach(Collider s_coltaget in S_cols)
            {
                float s_distance = Vector3.SqrMagnitude(this.transform.position - s_coltaget.transform.position);
                if (s_shortestDistance > s_distance)
                {
                    s_shortestDistance = s_distance;
                    ShortestTarget = s_coltaget.transform;
                }
            }
        }
        FinalTarget = ShortestTarget;
    }


}
