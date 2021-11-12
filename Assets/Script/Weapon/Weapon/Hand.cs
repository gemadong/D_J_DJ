using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : Weapon
{
    // Start is called before the first frame update

    void Awake()
    {
        this.type = Type.SwingWeapon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
