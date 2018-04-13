using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Följande script instansierar en wipe som tar bort kulor som befinner sig inom det område den har instansierats.
//Skapat av Moa Lindgren.

public class WipeScript : MonoBehaviour
{
    [SerializeField]
    GameObject wipePrefab, startObject, directionObject, wipeEffect;
    GameObject wipe;
    bool wipeDestroyed;
    bool wipeActive;
    [SerializeField]
    float wipeLifeTime, wipeCooldown;
    float cooldownTimer;

    //Sätter wipe till aktiv vid start, ta bort det här om den inte ska vara aktiv vid start. Men då bör ett annat condition implementeras.
    void Start()
    {
        wipeDestroyed = true;
        wipeActive = true;
        cooldownTimer = wipeCooldown;
    }

    //Wipen instansieras på ett objekt som befinner sig något framför GunBarrel.
    //Den har samma riktning som ShoulderAim.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && wipeDestroyed && wipeActive)
        {
            wipeActive = false;
            wipeDestroyed = false;
            wipe = Instantiate(wipePrefab,
                              new Vector3(startObject.transform.position.x, startObject.transform.position.y, startObject.transform.position.z),
                              Quaternion.identity);

            wipeEffect = Instantiate(wipeEffect,
                              new Vector3(startObject.transform.position.x, startObject.transform.position.y, startObject.transform.position.z),
                              Quaternion.identity);

            wipe.transform.rotation = directionObject.transform.rotation;
            wipeEffect.transform.rotation = directionObject.transform.rotation;
            StartCoroutine(WipeLifetime());
        }
        if(!wipeActive)
        {
            cooldownTimer -= Time.deltaTime;
            if(cooldownTimer <= 0)
            {
                wipeActive = true;
                cooldownTimer = wipeCooldown;
            }
        }
    }

    //Hur länge wipen existerar:
    IEnumerator WipeLifetime()
    {
        yield return new WaitForSeconds(wipeLifeTime);
        Destroy(wipe);
        wipeDestroyed = true;
    }
}
