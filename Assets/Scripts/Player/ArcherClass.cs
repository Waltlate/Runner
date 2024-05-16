using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArcherClass : BaseClass
{
    public ArcherClass()
    {
        ClassName = "Archer";
        Level = 1;
        Health = 5;
        Damage = 1;
        Speed = 1.5f;
        Distance = 2.5f;
    }
}
