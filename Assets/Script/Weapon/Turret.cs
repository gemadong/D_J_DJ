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

    //터렛이 목표물을 향해 회전할 스피드
    [SerializeField]private float SpinSpeed = 0;

    void Start()
    {
        InvokeRepeating("SearchEnomy", 0f, 0.5f);
    }


    void Update()
    {
        if(FinalTarget == null)
        {
            TurretBody.Rotate((new Vector3(0, 45f, 0)*Time.deltaTime));
            //타겟이 없을 때 터렛 회전
        }
        else
        {
            Quaternion LookTarget = Quaternion.LookRotation(FinalTarget.position-this.transform.position);
            //타겟과 자신의 위치 값을 통해 회전값을 준다
            Vector3 t_euler = Quaternion.RotateTowards(TurretBody.rotation, LookTarget, SpinSpeed*Time.deltaTime).eulerAngles;

            float XRot = t_euler.x;
            if (XRot > 180f) XRot = Mathf.Clamp(XRot, 310f, 361f);
            else XRot = Mathf.Clamp(XRot, -1, 20);
            //터렛의 X축 회전에 제한을 두어서 360도로 돌아가는 것을 막았다.

            TurretBody.rotation = Quaternion.Euler(XRot, t_euler.y, 0);
        }

    }

    private void SearchEnomy()
    {
        Collider[] S_cols = Physics.OverlapSphere(this.transform.position, TurretRange, tLayerMask);
        //OverlapSphere : 객체 주변의 Collider를 검출
        //검출한 collider를 배열형 변수에 저장
      
        Transform ShortestTarget = null;
        //가장 짧은 거리의 오브젝트 위치를 담을 변수

        if (S_cols.Length > 0) //배열의 길이 즉, 검출한 collider가 하나 이상 일 때 작동
        {
            float s_shortestDistance = Mathf.Infinity;
            // 거리계산에 사용할 변수 선언.
            foreach(Collider s_coltaget in S_cols) // foreach문을 이용해 배열의 길이만큼 길이비교 실행
            {  
                float s_distance = Vector3.SqrMagnitude(this.transform.position - s_coltaget.transform.position);
                // 터렛과 검출된 collider와의 거리를 담을 변수선언
                // Vector3.Distance와 Vector3.magnitude도 거리비교를 할 수 있지만 이 둘은 Root을 통해 실제 거리를 계산하기 때문에 연산이 더 들어간다.
                //SqrMagnitude는 실제거리*실제거리로 Root가 계산되지 않는 함수로 단순 거리비교일 때는 이것을 쓰는 게 연산 속도가 빠르다.
               
                if (s_shortestDistance > s_distance)
                {
                    s_shortestDistance = s_distance;
                    ShortestTarget = s_coltaget.transform;
                }
            }
        }
        FinalTarget = ShortestTarget;
        //반복문을 다 돌리고 나서 가장 거리가 짧은 대상을 최종 타겟으로 설정한다.
    }


}
