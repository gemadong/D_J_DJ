using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bos_1 : MonoBehaviour
{
    int hp = 100;
    Animator BosAni;
    public Transform target;
    private float bosSpeed = 5f;
    private Rigidbody rigid;
    bool Actswitch; //�׼� ����ġ
    float delaytime = 0; //���� ������


    private void Start()
    {
        BosAni = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        Actswitch = true;
    }
    void RotateBos()
    {
        Vector3 dir = target.position - transform.position;
        //Ÿ�ٹ������� slerp�� �̿��� ȸ����Ŵ
        //transform.localRotation���� LookRotation(dir)���� �� Time.deltaTime�� �ش��ϴ� ������ ��ȯ��
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(dir), 5 * Time.deltaTime);

    }
    void MoveBos()
    {
        //magnitude : ������ ��Ȯ�� ���� ��ȯ
        if((target.position - transform.position).magnitude >= 3)
        {
            BosAni.SetBool("Walk", true);
            //Translate : ��ü�� z���� ���� 1���� ������ �̵���. 
            transform.Translate(Vector3.forward * bosSpeed * Time.deltaTime);
            
        }
        if ((target.position - transform.position).magnitude < 3)
        {
            BosAni.SetBool("Walk", false);
        }
    }
    private void Update()
    {
        delaytime += Time.deltaTime;
        Jump();
        if (Actswitch)
        {
            RotateBos();
            MoveBos();
        }
    }
    void BosAtk()
    {
        if ((target.position - transform.position).magnitude < 3)
        {
            BosAni.Play("Attack");
        }
    }


    //(ani �̺�Ʈ�� �� ����.)�����Ҷ� �� �����̰� �ϴ� �Լ�
    void FreezeBos()
    {
        Actswitch = false;
    }
    //(ani �̺�Ʈ�� �� ����.)���� ������ �ٽ� ������ �޾ƿ��� �Լ�.
    void UnFreezeBos()
    {
        Actswitch = true;
    }
    void Jump()
    {
        if (delaytime >= 10f)
        {
            BosAni.Play("Jump");
            rigid.AddForce(transform.up * 15, ForceMode.Impulse);
            delaytime = 0;
        }
    }
    public void Damage(int _dmg)
    {
        hp -= _dmg;
        if (hp <= 0)
        {
            Destroy(gameObject);
            //if (onDeath != null) onDeath();
        }
    }
}
