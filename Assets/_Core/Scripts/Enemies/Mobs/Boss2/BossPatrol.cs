using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatrol : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    Transform patrolPoints;
    GameObject destination;
    //Transform destination;
    List<Transform> patrolPointsList;
    int patrolCounter;
    Rigidbody body;
    Transform player;
    Quaternion startRot;

    void Start()
    {
        startRot = transform.rotation;
        patrolPointsList = new List<Transform>();
        updatePatrolPoints();
        body = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        SetToPlayerPlane(transform);
        
    }
    

    protected void Patrol() //Går mot nästa patrullplats
    {
        if (destination != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination.transform.position, speed / 2 * Time.deltaTime);
        }

    }

    protected void updatePatrolPoints() //Kollar barnen på en transform och lägger till dem i en lista.
    {
        if (patrolPoints != null)
        {
            for (int i = 0; i < patrolPoints.childCount; i++)
            {
                patrolPointsList.Add(patrolPoints.transform.GetChild(i));
            }
            if (patrolPointsList.Count > 0)
                destination = patrolPointsList[0].gameObject;
        }

    }
    public void ChangeDestination(GameObject newDestination, GameObject lastDestination) //Ger en mob sitt nästa mål, om input är null går den till nästa mål i sin lista.
    {
        if (lastDestination == destination) //Ser till att vi inte krockar in i fel patrullställe
        {
            
            if (newDestination != null) //Sätter ny destination
                destination = newDestination;
            else //Går till nästa plats i listan
            {
                if (patrolPointsList.Count != 1)
                {
                    
                    patrolCounter++;
                    if (patrolCounter > patrolPointsList.Count - 1)
                    {
                        patrolCounter = 0;
                    }
                    destination = patrolPointsList[patrolCounter].gameObject;

                }
            }
        }

    }
    protected void SetToPlayerPlane(Transform Obj)
    {
        Obj.transform.position = new Vector3(Obj.transform.position.x, Obj.transform.position.y, player.transform.position.z);
    }

    
}
