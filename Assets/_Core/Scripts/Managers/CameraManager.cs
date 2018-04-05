using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Andreas de Freitas och Timmy Alvelöv.
//Ser till att huvudkameran följer efterspelaren eller andra bestämda platser.
public class CameraManager : MonoBehaviour
{
    [SerializeField]
    Transform player;
    Transform spawner;

    int screenHeight, screenWidth;

    [SerializeField]
    int boundary = 50, speed = 30;

    bool animDone;
    public bool AnimDone
    {
        set { animDone = value; }
    }

    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Spawner").transform;
        screenHeight = Screen.height;
        screenWidth = Screen.width;
    }

    void Update()
    {
        if (animDone) //Följer spelaren om startanimationen är klar
        {
            transform.position = Vector3.Lerp(transform.position, player.position, 12f * Time.deltaTime);

            if (Input.mousePosition.x > screenWidth - boundary)
            {
                transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));  //+X Axeln
            }
            if (Input.mousePosition.x < 0 + boundary)
            {
                transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0)); //-X Axeln
            }
            if (Input.mousePosition.y > screenHeight - boundary)
            {
                transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime)); //+Z Axeln
            }
            if (Input.mousePosition.y < 0 + boundary)
            {
                transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime)); //-Z Axeln
            }
        }
        else //Följer spawnpunkten
        {
            transform.position = Vector3.Lerp(transform.position, spawner.position, 12f * Time.deltaTime);
        }
    }


}
