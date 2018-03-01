using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Av Timmy Alvelöv
public class MC_ShootScript : MonoBehaviour
{
    Projectile[] Colors;
    [SerializeField]
    Transform rifleBarrel;
    
    void Start()
    {
        Colors = new Projectile[3];
    }



}
