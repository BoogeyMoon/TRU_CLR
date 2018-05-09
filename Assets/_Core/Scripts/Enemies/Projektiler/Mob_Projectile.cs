using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv

//En klass som alla skott som fiender skjuter ärver från och har gemensamt
//Scriptet har hand om var spelaren är och ser till att kulan förstörs om den missar, skada och hastighet.
public class Mob_Projectile : MonoBehaviour, IPoolable
{

    protected GameObject rotation, player;
    [SerializeField]
    protected float startVelocity, damage, startTime, lifeTime;
    [SerializeField]
    protected int color;
    protected PoolManager _pool;
    bool active;
    public bool Active { get { { return active; } } set { active = value; PoolStart(); } }

    protected void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _pool = GameObject.FindGameObjectWithTag("PoolManagers").transform.GetChild(2).GetComponent<PoolManager>();
        if (lifeTime == 0)
        {
            lifeTime = 5;
        }
    }
    void PoolStart()
    {
        startTime = 0;
    }
    protected virtual void Update()
    {
        if (active)
        {
            startTime += Time.deltaTime;
            if (startTime >= lifeTime) //Förstör kulan efter en angiven tid
            {
                _pool.DestroyPool(transform);
            }
        }

    }


}
