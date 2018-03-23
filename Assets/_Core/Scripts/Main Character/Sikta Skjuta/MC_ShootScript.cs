using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Av Timmy Alvelöv
// Vissa tillägg ang. röda projektilen av Moa Lindgren.
// Also added to by Slavko Stojnic

//Används för att skapa projektilerna som MC skjuter.
public class MC_ShootScript : MonoBehaviour
{
    [SerializeField]
    GameObject[] colorsBullets;
    [SerializeField]
    GameObject rifleBarrel, shoulderAim;
    GameObject currentBullet;

    enum ColorProjectiles { Blue, Yellow, Red };
    int activeColor;
    [SerializeField]
    float laserDamage;
    float cooldown, fireRate, offsetZ, laserLength;
    [SerializeField]
    float[] cooldowns;
    LineRenderer laserLineRenderer;
    Vector3 startPosition, direction;
    Vector3 endDash;
    [SerializeField]
    GameObject colorIndicator;
    ColorIndicatior colorInd;
    [SerializeField]
    Material material;
    Color[] colors;
    [SerializeField]
    AudioClip[] shots;
    private SoundManager soundManager;
    private GameObject mcCharacter;
    bool dashOnCooldown, doTheDash;
    float dashCooldown;

    void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SoundManager>();
        colors = new Color[] { Color.cyan, Color.yellow, Color.magenta };
        offsetZ = -0.85f;
        laserLength = 50f;
        laserLineRenderer = GetComponent<LineRenderer>();
        activeColor = (int)ColorProjectiles.Blue;
        colorInd = colorIndicator.GetComponent<ColorIndicatior>();
        mcCharacter =  gameObject;
    }

    void Update()
    {
        material.color = colors[activeColor];
        material.SetColor("_EmissionColor", colors[activeColor]);
        shoulderAim.transform.position = new Vector3(shoulderAim.transform.position.x, shoulderAim.transform.position.y, offsetZ);
        cooldown = cooldowns[activeColor];

        if (fireRate > 0)
        {
            fireRate -= Time.deltaTime;
        }
        if (Input.GetMouseButton(0))
        {
            if (fireRate <= 0)
            {
                laserLineRenderer.enabled = false;
                Shoot();

                StartCoroutine(LaserLifeTime());
                soundManager.RandomizeSfx(shots);

            }
        }
        if (dashOnCooldown)
        {
            dashCooldown += Time.deltaTime;
            if (dashCooldown >= 3)
            { dashCooldown = 0; dashOnCooldown = false; }
        }

        if (doTheDash) 
        {
            transform.position = Vector3.Lerp(transform.position, endDash, 8 * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, endDash) <= 1.5f)
        { doTheDash = false; } 
        
        if (Input.GetMouseButton(1) && !dashOnCooldown)
        {
            Dash();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            activeColor = (activeColor + 1) % 3;
            colorInd.SwitchColor(true);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            activeColor--;
            if (activeColor < 0)
            {
                activeColor = colorsBullets.Length - 1;
            }
            colorInd.SwitchColor(false);
        }
    }

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

    void Dash ()
    {
        direction = rifleBarrel.transform.forward;        
        endDash = transform.position + (13 * direction);
        /*Ray ray = new Ray(startPosition, direction);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit, 10))
        {
            endDash = raycastHit.point;
        }*/

            //transform.position = endDash;
            dashOnCooldown = true;
        doTheDash = true; 
        
    }

    IEnumerator LaserLifeTime()
    {
        yield return new WaitForSeconds(0.5f);
        laserLineRenderer.enabled = false;
    }
}
