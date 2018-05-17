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
    public int ActiveColor { get { return activeColor; } }

    [SerializeField]
    float laserDamage, shieldCooldown;
    float cooldown, timerCyan, offsetZ, laserLength, currentShieldCooldown;
    float[] colorCooldowns;
    [SerializeField]
    float[] cooldowns;

    LineRenderer laserLineRenderer;
    Vector3 startPosition, direction;
    [SerializeField]
    Material material;
    Color[] colors;
    [SerializeField]
    AudioClip[] shotsBlue, shotsYellow, shotsMagenta;
    AudioManager soundManager;
    PlayerStats playerStats;
    MenuScript menu;
    PoolManager[] _pools;


    void Start()
    {
        colorCooldowns = new float[3];
        soundManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        colors = new Color[] { Color.cyan, Color.yellow, Color.magenta };
        offsetZ = -0.85f;
        laserLength = 50f;
        laserLineRenderer = GetComponent<LineRenderer>();
        activeColor = (int)ColorProjectiles.Blue;
        SetMCColor();
        mcCharacter = gameObject;
        playerStats = GetComponent<PlayerStats>();
        menu = GameObject.FindGameObjectWithTag("MenuCanvas").GetComponent<MenuScript>();
        _pools = new PoolManager[2];
        for (int i = 0; i < _pools.Length; i++)
        {
            _pools[i] = GameObject.FindGameObjectWithTag("PoolManagers").transform.GetChild(i).GetComponent<PoolManager>();
        }
    }

    void Update()
    {
        if (!playerStats.Dead && !menu.Paused)
        {
            //Cooldown är olika beroende på vilken färg som är aktiv:
            cooldown = cooldowns[activeColor];

            shoulderAim.transform.position = new Vector3(shoulderAim.transform.position.x, shoulderAim.transform.position.y, offsetZ);

            if (Input.anyKey || Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                KeyPress();
            }

            for (int i = 0; i < colorCooldowns.Length; i++)
            {
                colorCooldowns[i] += Time.deltaTime;
            }
            currentShieldCooldown -= Time.deltaTime;
        }
    }

    void KeyPress() //Kollar vad vi klickat på och exikverar rätt kod
    {
        //Skjuter på vänster musklick:
        if (Input.GetMouseButton(0))
        {
            if (colorCooldowns[activeColor] >= cooldown)
            {
                laserLineRenderer.enabled = false;
                switch (activeColor)
                {
                    case 0:
                        soundManager.RandomizeSfx(shotsBlue);
                        break;
                    case 1:
                        soundManager.RandomizeSfx(shotsYellow);
                        break;
                    case 2:
                        soundManager.RandomizeSfx(shotsMagenta);
                        break;
                }

                StartCoroutine(LaserLifeTime());
                Shoot();
            }
        }
        //Ändrar till en specifik knapp
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            activeColor = 0;
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                activeColor = 0;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                activeColor = 1;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                activeColor = 2;
            }

            SetMCColor();
        }


        //Byter färg/egenskap på E och Q eller scroll:
        if (Input.GetKeyDown(KeyCode.E) || Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            activeColor = (activeColor + 1) % 3;
            SetMCColor();
        }
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            activeColor--;
            if (activeColor < 0)
            {
                activeColor = colorsBullets.Length - 1;
            }
            SetMCColor();
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
                if (raycastHit.collider.transform.gameObject.tag == "Weakpoint")
                {
                    raycastHit.collider.transform.gameObject.GetComponent<MobStats>().TakeDamage(laserDamage, activeColor);
                }
                else if (raycastHit.transform.tag == "Interactable")
                {
                    raycastHit.transform.GetComponent<SwitchInteract>().Trigger(2);
                }
            }
            laserLineRenderer.SetPosition(0, startPosition);
            laserLineRenderer.SetPosition(1, endPosition);
            laserLineRenderer.enabled = true;
            currentBullet = Instantiate(colorsBullets[(int)ColorProjectiles.Red],
        new Vector3(rifleBarrel.transform.position.x, rifleBarrel.transform.position.y, rifleBarrel.transform.position.z), Quaternion.identity);
        }
        else
        {
            currentBullet = _pools[activeColor].InstantiatePool(
                new Vector3(rifleBarrel.transform.position.x, rifleBarrel.transform.position.y, rifleBarrel.transform.position.z));
        }
        colorCooldowns[activeColor] = 0;

    }

    IEnumerator LaserLifeTime() //Stänger av line renderern efter en angiven tid
    {
        yield return new WaitForSeconds(0.1f);
        laserLineRenderer.enabled = false;
    }
    void SetMCColor() //Förrändrar material på karaktären beroende på vilken färg som är aktiv:
    {
        material.color = colors[activeColor];
        material.SetColor("_EmissionColor", colors[activeColor]);
        material.SetColor("_MKGlowColor", colors[activeColor]);
        material.SetColor("_MKGlowTexColor", colors[activeColor]);
    }
}
