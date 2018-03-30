using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1 : MonoBehaviour {

    BossTurretLaser laser;
    List<BossTurret> turrets;

    void Start()
    {
        turrets = new List<BossTurret>();
        laser = transform.GetChild(0).GetComponent<BossTurretLaser>();
        for (int i = 0; i < 2; i++)
        {
            turrets.Add(transform.GetChild(i+1).GetComponent<BossTurret>());
        }
    }
    public void AngryBoss(BossTurret bossT)
    {
        turrets.Remove(bossT);
        print(turrets.Count);
        laser.Upgrade();
        if(turrets.Count <= 0)
        {
            Die();
        }
        for (int i = 0; i < turrets.Count; i++)
        {
            turrets[i].Upgrade();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
