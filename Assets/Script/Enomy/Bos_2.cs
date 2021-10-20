using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bos_2 : MonoBehaviour
{
    [SerializeField] private GameObject PoisonPrefab = null;
    float delayTime = 0;
    float delayTime_A = 0;
    public Transform target;
    private float bosSpeed=5f;
    bool isattack;

    private void Start()
    {
        isattack = true;
    }
    private void Update()
    {
        delayTime += Time.deltaTime;
        delayTime_A += Time.deltaTime;
        if(delayTime >= 5f) { Fire(); delayTime = 0; }
        if (isattack == true) { RotateBos(); }
        MoveBos();
    }

    public void Fire()
    {
        GameObject poison = Instantiate(PoisonPrefab, transform.position, Quaternion.identity);
        poison.GetComponent<Poison>().Shoot(transform.forward);
    }
    void RotateBos()
    {
        Vector3 dir = target.position - transform.position;
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(dir), 5 * Time.deltaTime);

    }
    void MoveBos()
    {
        if ((target.position - transform.position).magnitude >= 10)
        {
            transform.Translate(Vector3.forward * bosSpeed * Time.deltaTime);

        }
        if ((target.position - transform.position).magnitude < 10)
        {
            if (delayTime_A >= 5f) { StartCoroutine("Attack");; delayTime_A = 0; }
        }
    }
    IEnumerator Attack()
    {
        isattack = false;
        for (int i =-10;i<120; i+=3)
        {
            Vector3 roR = new Vector3(0, i, 0);
            GameObject poison = Instantiate(PoisonPrefab, transform.position, Quaternion.identity);
            Vector3 dir = transform.forward + roR*Time.deltaTime;
            poison.GetComponent<Poison>().Shoot(dir);
            yield return new WaitForSeconds(0.01f);
        }
        isattack = true;
    }
}
