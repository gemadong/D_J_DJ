using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jombie_S : Jombie
{
    protected override void Awake()
    {
        base.Awake();
        speed = 12f;
        DieCoin = 300;
    }
    
}
