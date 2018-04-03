using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Följande script instansierar en wipe som tar bort kulor som befinner sig inom det område den har instansierats.
//Skapat av Moa Lindgren.

public class WipeScript : MonoBehaviour
{
    [SerializeField]
    GameObject wipePrefab, startObject, directionObject;
    GameObject wipe;
    bool wipeReady;
    [SerializeField]
    float wipeLifeTime;

    //Sätter wipe till aktiv vid start, ta bort det här om den inte ska vara aktiv vid start. Men då bör ett annat condition implementeras.
    void Start()
    {
        wipeReady = true;
    }

    //Wipen instansieras på ett objekt som befinner sig något framför GunBarrel.
    //Den har samma riktning som ShoulderAim.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && wipeReady)
        {
            wipeReady = false;
            wipe = Instantiate(wipePrefab,
                              new Vector3(startObject.transform.position.x, startObject.transform.position.y, startObject.transform.position.z),
                              Quaternion.identity);

            wipe.transform.rotation = directionObject.transform.rotation;
            StartCoroutine(WipeLifetime());
        }
    }

    //Hur länge wipen existerar:
    IEnumerator WipeLifetime()
    {
        yield return new WaitForSeconds(wipeLifeTime);
        Destroy(wipe);
        wipeReady = true;
    }
}
