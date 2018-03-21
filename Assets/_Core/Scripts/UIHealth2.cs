using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth2 : MonoBehaviour
{

    [SerializeField]
    int startLives, currentHealth;

    int maxHealthAmount = 10, fullHealth, heal;

    [SerializeField]
    Image[] healthImages;

    [SerializeField]
    Sprite[] healthSprites;

    void Start()
    {
        currentHealth = startLives * heal;
        fullHealth = maxHealthAmount * heal;
    }

    void CheckHealth()
    {
        for (int i = 0; i < maxHealthAmount; i++)
        {
            if (startLives <= i)
            {
                healthImages[i].enabled = false;
            }
            else
            {
                healthImages[i].enabled = true;
            }

        }
    }

    void TakeDamage(int damage)
    {
        currentHealth += damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, startLives * heal);
        UpdateHealth();
    }

    void AddHealth()
    {
        startLives++;
        startLives = Mathf.Clamp(startLives, 0, maxHealthAmount);

        // currentHealth = startLives * heal;
        //  fullHealth = maxHealthAmount * heal;

        CheckHealth();
    }
    void UpdateHealth()
    {
        bool empty = false;
        int i = 0;

        foreach (Image image in healthImages)
        {
            if (empty)
            {
                image.sprite = healthSprites[0];
            }
            else
            {
                i++;
                if (currentHealth >= i * heal)
                {
                    image.sprite = healthSprites[healthSprites.Length - 1];
                }

                else
                {
                    int currentLiveHealth = (int)(heal - (heal * i - currentHealth));
                    int healthPerImage = heal / (healthSprites.Length - 1);
                    int imageIndex = currentLiveHealth / healthPerImage;
                    image.sprite = healthSprites[imageIndex];
                    empty = true;
                }
            }
        }
    }
}
