using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Skapat av Moa Lindgren. Ärver från Projectile.
//Följande script lägger till en ökad gravity till det gula skottet.
public class YellowBullet : Projectile
{
    void Awake()
    {
        Physics.gravity = new Vector3(0, -45, 0);
    }
}
