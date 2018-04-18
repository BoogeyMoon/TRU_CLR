using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;
//Av Andreas de Freitas och Timmy Alvelöv.

//Håller koll på spelarens hälsa och liknande

public class PlayerStats : MonoBehaviour
{
    GameObject canvas;
    UIHealth2 uiHealth;
    [SerializeField]
    float maxHealth;
    [SerializeField]
    float health;
    public float Health
    {
        get { return health; }
    }
    Animator anim;
    bool dead;
    [SerializeField]
    PostProcessingProfile ppProfile;
    VignetteModel.Settings vignetteSettings;
    public bool Dead
    {
        get { return dead; }
    }

    void Awake()
    {
        if (PlayerPrefs.GetFloat("savedHealth") != -1)
        {
            health = maxHealth / 2;
        }
    }

    void Start()
    {
        //Återställer vignetten
        vignetteSettings = ppProfile.vignette.settings;
        vignetteSettings.intensity = 0.0f;
        vignetteSettings.color = new Color(1, 0, 0, 1);
        vignetteSettings.smoothness = 1;
        vignetteSettings.roundness = 1;
        vignetteSettings.center = new Vector2(0.5f, 0.5f);
        ppProfile.vignette.settings = vignetteSettings;
        uiHealth = GameObject.FindGameObjectWithTag("Healthbar").GetComponent<UIHealth2>();
        canvas = GameObject.FindGameObjectWithTag("MenuCanvas").gameObject;
    }


    public void ChangeHealth(float value) //Lägg till eller ta bort hälsa från spelaren
    {
        if (health > 0)
        {
            health += value;

            if (health >= maxHealth)
            {
                health = maxHealth;
            }

            if (health <= 0)
            {
                PlayerDies();
            }

            else if (value < 0 && vignetteSettings.intensity < 0.6f) //Ökar värdet på vignette effekten när man tagit skada
            {
                vignetteSettings.intensity = vignetteSettings.intensity + 0.2f;
                ppProfile.vignette.settings = vignetteSettings;
                StopCoroutine("LerpDamageEffect");
                StartCoroutine("LerpDamageEffect");
            }
            uiHealth.TakeDamage((int)health);

        }


    }

    public void PlayerDies() //Ifall spelaren dör
    {
        dead = true;
        GameObject.FindGameObjectWithTag("MCWeapon").GetComponent<Renderer>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<MC_ShootScript>().enabled = false;
        anim = GetComponent<Animator>();
        anim.SetLayerWeight(1, 1);
        StartCoroutine(StartLoseScreen());
    }
    public void ChangeLayer(int layer)
    {
        gameObject.layer = layer;
    }
    IEnumerator StartLoseScreen()
    {
        yield return new WaitForSeconds(2);
        canvas.transform.GetChild(8).gameObject.SetActive(true);
    }

    IEnumerator LerpDamageEffect() //Lerpar bort den röda effekten långsamt när man tagit skada
    {
        while (vignetteSettings.intensity > 0f)
        {
            yield return new WaitForSeconds(0.1f);
            vignetteSettings.intensity -= 0.02f;

            if (vignetteSettings.intensity < 0.02)
            {
                vignetteSettings.intensity = 0;
            }
            ppProfile.vignette.settings = vignetteSettings;
        }
    }
}
