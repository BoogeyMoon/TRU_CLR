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
    enum ColorProjectiles { Blue, Yellow, Red };
    int activeColor;
    GameObject currentBullet;
    float fireRate;
    [SerializeField]
    float cooldown;
    Vector3 redProjectileDir; //Tillagt
    RaycastHit objectHit; //Tillagt
    GameObject target; //Tillagt

    void Start()
    {
        fireRate = 0;
        cooldown = 0.5f;
        activeColor = (int)ColorProjectiles.Blue;
    }
    void Update()
    {
        if(activeColor == (int)ColorProjectiles.Red)
        {
            redProjectileDir = rifleBarrel.transform.TransformDirection(Vector3.forward); //Tillagt
        }
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
        //Tillagt
        if(activeColor == (int)ColorProjectiles.Red)
        {
            Debug.DrawRay(rifleBarrel.transform.position, redProjectileDir * 50);

            if(Physics.Raycast(rifleBarrel.transform.position, redProjectileDir, out objectHit, 50))
            {
                target = objectHit.transform.gameObject;
                if(target.tag == "WeakPoint")
                {
                    print("du träffade en weakpoint");
                }
            }
        }
        //_
            currentBullet = Instantiate(colorsBullets[activeColor],
            new Vector3(rifleBarrel.transform.position.x, rifleBarrel.transform.position.y, rifleBarrel.transform.position.z), Quaternion.identity);
            fireRate = cooldown;
    }
}
