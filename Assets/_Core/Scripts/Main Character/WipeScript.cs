using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Följande script instansierar en wipe som tar bort kulor som befinner sig inom det område den har instansierats.
//Skapat av Moa Lindgren.

public class WipeScript : MonoBehaviour
{
    [SerializeField]
    GameObject wipePrefab, startObject, directionObject;
    ParticleSystem wipeEffect;
    GameObject wipe, wipeCDIndicator;
    Image wipeImage;
    bool wipeDestroyed;
    bool wipeActive;
    [SerializeField]
    float wipeLifeTime, wipeCooldown;
    float cooldownTimer;
    Animation animWipeReady;
    MenuScript menu;
    PlayerStats player;
    AudioManager sound;

    //Sätter wipe till aktiv vid start, ta bort det här om den inte ska vara aktiv vid start. Men då bör ett annat condition implementeras.
    void Start()
    {
        menu = GameObject.FindGameObjectWithTag("MenuCanvas").GetComponent<MenuScript>();
        wipeCDIndicator = GameObject.Find("Canvas UI").transform.GetChild(2).gameObject;
        wipeImage = wipeCDIndicator.transform.GetChild(1).GetComponent<Image>();
        animWipeReady = wipeCDIndicator.transform.GetChild(2).GetComponent<Animation>();
        wipeDestroyed = true;
        wipeActive = true;
        cooldownTimer = wipeCooldown;
        wipeEffect = startObject.GetComponentInChildren<ParticleSystem>();
        player = GetComponent<PlayerStats>();
        sound = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    //Wipen instansieras på ett objekt som befinner sig något framför GunBarrel.
    //Den har samma riktning som ShoulderAim.
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && wipeDestroyed && wipeActive && !menu.Paused && !player.Dead)
        {
            wipeActive = false;
            wipeDestroyed = false;
            wipe = Instantiate(wipePrefab,
                              new Vector3(startObject.transform.position.x, startObject.transform.position.y, startObject.transform.position.z),
                              Quaternion.identity);

            wipeEffect.Play(true);
            sound.Play("wipe2");
            wipe.transform.rotation = directionObject.transform.rotation;
            wipeEffect.transform.rotation = directionObject.transform.rotation;
            StartCoroutine(WipeLifetime());
            wipeImage.fillAmount = 0;
        }

        if (!wipeActive)
        {
            cooldownTimer -= Time.deltaTime;
            wipeImage.fillAmount = wipeImage.fillAmount + Time.deltaTime * (1/wipeCooldown);


            if (cooldownTimer <= 0) 
            {
                wipeActive = true;
                animWipeReady.Play("Anim_WipeIcon", PlayMode.StopAll);
                cooldownTimer = wipeCooldown;
                wipeImage.fillAmount = 1;
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
