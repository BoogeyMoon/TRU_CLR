using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Av Timmy Alvelöv
// Vissa tillägg ang. röda projektilen av Moa Lindgren.
// Also added to by Slavko Stojnic.

//Används för att skapa projektilerna som MC skjuter.
public class MC_ShootScript : MonoBehaviour
{
    [SerializeField]
    GameObject[] colorsBullets;
    [SerializeField]
    GameObject rifleBarrel, shoulderAim, shield, colorIndicator;
    GameObject currentBullet, mcCharacter, currentShield;

    enum ColorProjectiles { Blue, Yellow, Red };
    int activeColor;
    [SerializeField]
    float laserDamage;
    float cooldown, fireRate, offsetZ, laserLength;
    [SerializeField]
    float[] cooldowns;

    LineRenderer laserLineRenderer;
    Vector3 startPosition, direction;
    ColorIndicatior colorInd;
    [SerializeField]
    Material material;
    Color[] colors;
    [SerializeField]
    AudioClip[] shots;
    SoundManager soundManager;


    void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SoundManager>();
        colors = new Color[] { Color.cyan, Color.yellow, Color.magenta };
        offsetZ = -0.85f;
        laserLength = 50f;
        laserLineRenderer = GetComponent<LineRenderer>();
        activeColor = (int)ColorProjectiles.Blue;
        colorInd = colorIndicator.GetComponent<ColorIndicatior>();
        mcCharacter = gameObject;
    }

    void Update()
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
    }

    void KeyPress()
    {
        //Skjuter på vänster musklick:
        if (Input.GetMouseButton(0))
        {
            if (fireRate <= 0)
            {
                laserLineRenderer.enabled = false;
                soundManager.RandomizeSfx(shots);
                StartCoroutine(LaserLifeTime());
                Shoot();
            }
        }


        //Byter färg/egenskap på E och Q eller scroll:
        if (Input.GetKeyDown(KeyCode.E) || Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            activeColor = (activeColor + 1) % 3;
            colorInd.SwitchColor(true);
        }
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            activeColor--;
            if (activeColor < 0)
            {
                activeColor = colorsBullets.Length - 1;
            }
            colorInd.SwitchColor(false);
        }

        //Skickar en sköld på vänster Shift:
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentShield = Instantiate(shield,
            new Vector3(rifleBarrel.transform.position.x, rifleBarrel.transform.position.y, rifleBarrel.transform.position.z), Quaternion.identity);
            currentShield.transform.rotation = rifleBarrel.transform.rotation;
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
