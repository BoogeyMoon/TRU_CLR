using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;
//Av Andreas de Freitas och Timmy Alvelöv.

//Håller koll på spelarens hälsa och liknande

public class PlayerStats : MonoBehaviour
{
    UIHealth2 uiHealth;
    [SerializeField]
    float maxHealth;
    [SerializeField]
    float health;
    Animator anim;
    bool dead;
    [SerializeField]
    PostProcessingProfile ppProfile;
    VignetteModel.Settings vignetteSettings;
    public bool Dead
    

    {
        get { return dead; }
    }
    public float Health
    {
        get
        {
            return health;
        }
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
        uiHealth = GameObject.FindGameObjectWithTag("Healthbar").GetComponent<UIHealth2>();
        vignetteSettings = ppProfile.vignette.settings;
        vignetteSettings.intensity = 0.0f;
        ppProfile.vignette.settings = vignetteSettings;
    }


    public void ChangeHealth(float value) //Lägg till eller ta bort hälsa från spelaren
    {
        if (health > 0 )
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
            uiHealth.TakeDamage((int)health);

        }

        if (value < 0 && vignetteSettings.intensity < 0.4f)
        {
            vignetteSettings.intensity = vignetteSettings.intensity + 0.1f;
            ppProfile.vignette.settings = vignetteSettings;
            StopCoroutine("LerpDamageEffect");
            StartCoroutine("LerpDamageEffect");
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
        PlayerPrefs.SetFloat("savedHealth", maxHealth);
        StartCoroutine(GoToMenu());
    }

    IEnumerator GoToMenu() //Väntar i tre sekunder innan den går till menyn
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }
    public void ChangeLayer(int layer)
    {
        gameObject.layer = layer;
    }

    IEnumerator LerpDamageEffect()
    {
        while (vignetteSettings.intensity > 0f)
        {
            yield return new WaitForSeconds(0.1f);
            vignetteSettings.intensity -= 0.015f;
            ppProfile.vignette.settings = vignetteSettings;
        }
    }
}
