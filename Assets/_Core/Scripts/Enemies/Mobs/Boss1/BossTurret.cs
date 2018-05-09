using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv 

//Scriptet ser till att bossturreten beter sig som en bossturret och inte en vanlig turret.
public class BossTurret : Turret
{
    [SerializeField]
    Material[] mats;
    [SerializeField]
    Material[] myMats;
    Boss_1 boss;
    [SerializeField]
    GameObject Turret;



    protected override void Start()
    {
        BaseStart();
        myMats = Turret.GetComponent<Renderer>().materials;
        boss = transform.parent.parent.parent.GetComponent<Boss_1>();

    }
    public override void TakeDamage(float damage, int color) //En anpassad version av TakeDamage för Bossturreten som kommunicerar med Boss_1 scriptet samt ändrar färg varje gång den blir träffad
    {
        if (!dead)
        {
            if (color == this.color && health - damage <= 0)
                boss.AngryBoss(this);

            if (this.color == color)
            {
                this.color = (this.color + 1) % 3;
                ChangeMaterial(this.color);
                base.TakeDamage(damage, this.color, Turret.transform);
            }
            

        }
    }
    void ChangeMaterial(int color) //Byter material till det specificerade
    {
        myMats[myMats.Length - 1] = mats[color];
        Turret.GetComponent<Renderer>().materials = myMats;
    }



}
