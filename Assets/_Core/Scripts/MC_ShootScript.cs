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
    GameObject rifleBarrel, shoulderAim;
    protected enum ColorProjectiles { Blue, Yellow, Red };
    protected int activeColor;
    public int ActiveColor
    {
        get
        {
            return activeColor;
        }
    }
    GameObject currentBullet;
    float fireRate;
    [SerializeField]
    float cooldown;

    void Start()
    {
        fireRate = 0f;
        cooldown = 0.5f;
        activeColor = (int)ColorProjectiles.Blue;
    }
    void Update()
    {
        if (fireRate > 0)
        {
            fireRate -= Time.deltaTime;
        }
        if (Input.GetMouseButton(0))
        {
            if (fireRate <= 0)
            {
                Shoot();
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            activeColor = (activeColor + 1) % 3;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            activeColor--;
            if (activeColor < 0)
            {
                activeColor = colorsBullets.Length - 1;
            }
        }
    }

    void Shoot()
    {
        currentBullet = Instantiate(colorsBullets[activeColor],
        new Vector3(rifleBarrel.transform.position.x, rifleBarrel.transform.position.y, rifleBarrel.transform.position.z), Quaternion.identity);

        fireRate = cooldown;
    }
}
