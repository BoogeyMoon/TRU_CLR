using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTestFreitas : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    int boundary = 50, speed = 30;

    int screenWidth;

    void Start()
    {
        screenWidth = Screen.width / 2;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.position, 12f * Time.deltaTime);

        if (Input.mousePosition.x > screenWidth - boundary)
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0)); //+X Axeln     
        }
        if (Input.mousePosition.x < 0 + boundary)
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0)); //-X Axeln
        }
    }
}
