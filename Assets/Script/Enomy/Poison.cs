using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour
{
    private Vector3 dir = Vector3.zero;
    private float speed = 20f;
    private bool IsShoot = false;
    private float ElapsedTime = 0f;
    private float lifeTime = 1f;
    
    public void Shoot(Vector3 _dir)
    {
        dir = _dir;
        IsShoot = true;
        Destroy(gameObject, lifeTime);
    }
    private void Update()
    {
        if (!IsShoot) return;
         transform.position = transform.position + (dir * speed * Time.deltaTime);
        

        ElapsedTime += Time.deltaTime;
        if (ElapsedTime >= lifeTime)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider _col)
    {
        if (_col.CompareTag("Player"))
        {
            Destroy(gameObject);
            Player player = _col.GetComponent<Player>();
            player.Damage(10);
            player.StartCoroutine("poison");   
        }
    }
    
}
