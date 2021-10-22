using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jombie_T : Jombie
{
    protected override void Awake()
    {
        base.Awake();
        hp = 30;
    }
}
