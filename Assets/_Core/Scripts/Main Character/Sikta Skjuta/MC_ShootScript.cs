using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Av Timmy Alvelöv
// Vissa tillägg ang. röda projektilen av Moa Lindgren.
// Soundactivation av Slavko Stojnic

//Används för att skapa projektilerna som MC skjuter.
public class MC_ShootScript : MonoBehaviour
{
    [SerializeField]
    GameObject[] colorsBullets;
    [SerializeField]
    GameObject rifleBarrel, shoulderAim, shield;
    GameObject currentBullet, mcCharacter, currentShield;

    enum ColorProjectiles { Blue, Yellow, Red };
    int activeColor;
    [SerializeField]
    float laserDamage, shieldCooldown;
    float cooldown, fireRate, offsetZ, laserLength, currentShieldCooldown;
    [SerializeField]
    float[] cooldowns;

    LineRenderer laserLineRenderer;
    Vector3 startPosition, direction;
    [SerializeField]
    Material material;
    Color[] colors;
    [SerializeField]
    AudioClip[] shotsBlue, shotsYellow, shotsMagenta;
    SoundManager soundManager;
    PlayerStats playerStats;


    void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SoundManager>();
        colors = new Color[] { Color.cyan, Color.yellow, Color.magenta };
        offsetZ = -0.85f;
        laserLength = 50f;
        laserLineRenderer = GetComponent<LineRenderer>();
        activeColor = (int)ColorProjectiles.Blue;
        mcCharacter = gameObject;
        playerStats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        if (!playerStats.Dead)
        {
            //Förrändrar material på karaktären beroende på vilken färg som är aktiv:
            material.color = colors[activeColor];
            material.SetColor("_EmissionColor", colors[activeColor]);
            material.SetColor("_MKGlowColor", colors[activeColor]);
            material.SetColor("_MKGlowTexColor", colors[activeColor]);

            //Cooldown är olika beroende på vilken färg som är aktiv:
            cooldown = cooldowns[activeColor];

            shoulderAim.transform.position = new Vector3(shoulderAim.transform.position.x, shoulderAim.transform.position.y, offsetZ);

            if (Input.anyKey || Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                KeyPress();
            }

            //Firerate räknar ner så länge den är över 0:
            if (fireRate > 0)
            {
                fireRate -= Time.deltaTime;
            }
                currentShieldCooldown -= Time.deltaTime;
        }
    }

    void KeyPress()
    {
        //Skjuter på vänster musklick:
        if (Input.GetMouseButton(0))
        {
            if (fireRate <= 0)
            {
                laserLineRenderer.enabled = false;
                switch (activeColor)
                {
                    case 0:
                        soundManager.RandomizeSfx(shotsBlue,0);
                        break;
                    case 1:
                        soundManager.RandomizeSfx(shotsYellow,0);
                        break;
                    case 2:
                        soundManager.RandomizeSfx(shotsMagenta,0);
                        break;
                }

                StartCoroutine(LaserLifeTime());
                Shoot();
            }
        }


        //Byter färg/egenskap på E och Q eller scroll:
        if (Input.GetKeyDown(KeyCode.E) || Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            activeColor = (activeColor + 1) % 3;
        }
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            activeColor--;
            if (activeColor < 0)
            {
                activeColor = colorsBullets.Length - 1;
            }
        }

        //Skickar en sköld på vänster Shift:
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (currentShieldCooldown <= 0)
            {
            currentShield = Instantiate(shield,
            new Vector3(rifleBarrel.transform.position.x, rifleBarrel.transform.position.y, rifleBarrel.transform.position.z), Quaternion.identity);
            currentShield.transform.rotation = rifleBarrel.transform.rotation;
                currentShieldCooldown = shieldCooldown;
            }
            
        }
    }

    //Shoot-metoden instansierar en prefab för en kula i den färg som är aktiv just nu.
    //Om den aktiva färgen är magenta så kommer en LineRenderer aktiveras, som baseras på en raycast från vapnet:
    void Shoot()
    {
        if (activeColor == (int)ColorProjectiles.Red)
        {
            startPosition = rifleBarrel.transform.position;
            direction = rifleBarrel.transform.forward;
            Ray ray = new Ray(startPosition, direction);
            RaycastHit raycastHit;
            Vector3 endPosition = startPosition + (laserLength * direction);

            if (Physics.Raycast(ray, out raycastHit, laserLength))
            {
                endPosition = raycastHit.point;
                if (raycastHit.transform.gameObject.tag == "Weakpoint")
                {
                    raycastHit.transform.gameObject.GetComponent<MobStats>().TakeDamage(laserDamage, activeColor);
                }
                else if (raycastHit.transform.tag == "Interactable")
                {
                    raycastHit.transform.GetComponent<SwitchInteract>().Trigger(2);
                }
            }
            laserLineRenderer.SetPosition(0, startPosition);
            laserLineRenderer.SetPosition(1, endPosition);
            laserLineRenderer.enabled = true;
        }

        currentBullet = Instantiate(colorsBullets[activeColor],
        new Vector3(rifleBarrel.transform.position.x, rifleBarrel.transform.position.y, rifleBarrel.transform.position.z), Quaternion.identity);
        fireRate = cooldown;
    }

    IEnumerator LaserLifeTime()
    {
        yield return new WaitForSeconds(0.1f);
        laserLineRenderer.enabled = false;
    }
}
