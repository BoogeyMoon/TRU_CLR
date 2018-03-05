using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Av Timmy Alvelöv

//Används för att skapa projektilerna som MC skjuter.
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
        if(Input.GetKeyDown(KeyCode.E))
        {
            activeColor = (activeColor +1) % 3;
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            activeColor--;
            if(activeColor < 0)
            {
                activeColor = colorsBullets.Length -1;
            }
        }
    }

    void Shoot()
    {
        currentBullet = Instantiate(colorsBullets[activeColor], 
            new Vector3(rifleBarrel.transform.position.x,rifleBarrel.transform.position.y,rifleBarrel.transform.position.z), Quaternion.identity);
    }
}
