using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    private void OnTriggerEnter(Collider _col)
    {
        if (_col.CompareTag("Player"))
        {
            Player player = _col.GetComponent<Player>();
            player.Damage(15);
        }
    }
}
