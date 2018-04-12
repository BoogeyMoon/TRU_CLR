using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv

//Beskriver bossen som helhet, har koll på sina underkomponenter (två boss turrets och en laser)
public class Boss_1 : MonoBehaviour {

    BossTurretLaser laser;
    List<BossTurret> turrets;
    WinScript win;

    void Start()
    {
        turrets = new List<BossTurret>();
        laser = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<BossTurretLaser>();
        for (int i = 0; i < 2; i++)
        {
            turrets.Add(transform.GetChild(0).GetChild(i+1).GetChild(0).GetComponent<BossTurret>());
        }
        win = GameObject.FindGameObjectWithTag("Win").GetComponent<WinScript>();
    }
    public void AngryBoss(BossTurret bossT) //Ser till att uppgradera alla aktiva komponenter.
    {
        turrets.Remove(bossT);
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
    void Die() //Hanterar döden på boss 1
    {
        win.WinConFinished(transform);
        Destroy(gameObject);
    }
}
