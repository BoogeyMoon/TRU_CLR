using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Av Andreas de Freitas. GLÖM INTE ATT TA BORT TEST TEXTURERNA SEN
public class HealthUI : MonoBehaviour
{
    [SerializeField]
    GUIStyle progressEmpty, progressFull;

    [SerializeField]
    int healthDisplay, maxHealth;

    [SerializeField]
    Texture2D emptyTexure, fullTexture;

    [SerializeField]
    Text healthProcent;

    Vector2 pos, size;

    void Start()
    {
        pos = new Vector2(10, 50);
        size = new Vector2(200, 50);
    }

    void TakeDamage(int damage)
    {
        healthDisplay -= damage;

        if (healthDisplay <= 0)
        {
            healthDisplay = 0;
        }
    }

    void GainHealth(int healthPack)
    {
        healthDisplay += healthPack;

        if (healthDisplay >= maxHealth)
        {
            healthDisplay = maxHealth;
        }
    }

    void Update() //Endast för test. Sen kör vi antagligen propertys för att få över värdena
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            TakeDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            GainHealth(10);
        }
    }

    void OnGUI()
    {
        //Rita bakgrunden för healthbaren
        GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y), emptyTexure, progressEmpty);
        GUI.Box(new Rect(pos.x, pos.y, size.x, size.y), fullTexture, progressFull);

        //Rita upp hälsan
        GUI.BeginGroup(new Rect(0, 0, size.x * healthDisplay / 100, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), fullTexture, progressFull);
        healthProcent.text = healthDisplay.ToString() + "%";

        GUI.EndGroup();
        GUI.EndGroup();
    }
}
