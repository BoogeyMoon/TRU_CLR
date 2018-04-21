using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv

//Ser till att gameobjectet som partikeleffekten sitter på förstörs efter en given tid.
public class ParticleKill : MonoBehaviour
{
    [SerializeField]
    float killTime;
    PoolManager _pool;
	
    void Start()
    {
        _pool = GameObject.FindGameObjectWithTag("PoolManagers").transform.GetChild(0).GetComponent<PoolManager>();
    }
    public void Kill() // Startar en Co-rutin
    {
        StartCoroutine(KillSoon());
    }

    IEnumerator KillSoon() //Förstör ett gameobject inom en given tid.
    {
        yield return new WaitForSeconds(killTime);
        _pool.DestroyPool(transform);
    }
        
}
