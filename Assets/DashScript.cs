using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashScript : MonoBehaviour
{

    [SerializeField]
    GameObject rifleBarrel;
    float dashCooldown;
    bool dashOnCooldown, doTheDash;
    [SerializeField]
    float lengthOfDash;

    Vector3 startPosition, direction, endDash;

    void Start()
    {
        
    }

    void Update()
    {
        startPosition = rifleBarrel.transform.position;

        //Om dash är på cooldown, räkna ner i 3 sek, och gör sedan dash aktiv igen:
        if (dashOnCooldown)
        {
            dashCooldown += Time.deltaTime;
            if (dashCooldown >= 3)
            {
                dashCooldown = 0;
                dashOnCooldown = false;
            }
        }

        if (doTheDash)
        {
            //transform.position = Vector3.Lerp(transform.position, endDash, 8 * Time.deltaTime);
        }
        if (Vector3.Distance(transform.position, endDash) <= 1.5f)
        {
            doTheDash = false;
        }

        //Dashar fram på höger musklick:
        if (Input.GetMouseButton(1) && !dashOnCooldown)
        {
            direction = rifleBarrel.transform.forward;
            Dash();
        }


    }

    void Dash()
    {

        //endDash = transform.position + (lengthOfDash * direction);

        Ray ray = new Ray(startPosition, direction);
        RaycastHit raycastHit;
        Debug.DrawRay(startPosition, direction);

        if(Physics.Raycast(ray, out raycastHit, lengthOfDash))
        {
            print(raycastHit.transform.gameObject);
        }


        //if ((Physics.Raycast(ray, out raycastHit, lengthOfDash)) && (raycastHit.distance < Vector3.Distance(transform.position, endDash)))
        //{
        //    print("EAREWERWEF");
        //    endDash = raycastHit.point - (2 * direction);
        //}

        dashOnCooldown = true;
        doTheDash = true;

    }
}
