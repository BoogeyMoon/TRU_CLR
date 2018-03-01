using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Av Timmy Alvelöv
public class MC_ShootScript : MonoBehaviour
{
    [SerializeField]
    GameObject[] colorsBullets;
    [SerializeField]
    GameObject rifleBarrel;
    enum ColorProjectiles {Blue, Yellow, Red};
    int activeColor;
    GameObject currentBullet;

    void Start()
    {
        activeColor = (int) ColorProjectiles.Blue;
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        currentBullet = Instantiate(colorsBullets[activeColor], 
            new Vector3(rifleBarrel.transform.position.x,rifleBarrel.transform.position.y,rifleBarrel.transform.position.z), Quaternion.identity);
    }
}
