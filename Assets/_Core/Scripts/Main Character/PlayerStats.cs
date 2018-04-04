using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Av Andreas de Freitas och Timmy Alvelöv.

//Håller koll på spelarens hälsa och checkpoints

public class PlayerStats : MonoBehaviour
{
    UIHealth2 uiHealth;
    [SerializeField]
    float maxHealth;
    float health;
    Animator anim;
    bool dead;
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

    string checkpoint;

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
            uiHealth.TakeDamage((int)health);
        }
    }

    public void PlayerDies() //Ifall spelaren dör
    {
        dead = true;
        GameObject.FindGameObjectWithTag("MCWeapon").GetComponent<Renderer>().enabled = false;
        GetComponent<testMCmovement>().enabled = false;
        GetComponent<MC_ShootScript>().enabled = false;
        anim = GetComponent<Animator>();
        anim.SetLayerWeight(2, 1);
        anim.SetLayerWeight(1, 0);
        PlayerPrefs.SetFloat("savedHealth", maxHealth);
        StartCoroutine(GoToMenu());
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //health = maxHealth;
    }

    IEnumerator GoToMenu()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }
    public void ChangeLayer(int layer)
    {
        gameObject.layer = layer;
    }
}
