﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTurret : Turret
{
    [SerializeField]
    Material[] mats;
    [SerializeField]
    Material[] myMats;
    Boss_1 boss;

    protected override void Start()
    {
        base.Start();
        myMats = head.GetComponent<Renderer>().materials;
        boss = transform.parent.GetComponent<Boss_1>();

    }
    public override void TakeDamage(float damage, int color) //En anpassad version av TakeDamage för Bossturreten som kommunicerar med Boss_1 scriptet samt ändrar färg varje gång den blir träffad
    {
        if (!dead)
        {
            if (color == this.color && health - damage <= 0)
                boss.AngryBoss(this);

            base.TakeDamage(damage, color);

            if (this.color == color)
            {
                this.color = (this.color + 1) % 3;
            }
            ChangeMaterial(this.color);
        }
    }
    void ChangeMaterial(int color) //Byter material till den motsvarande
    {
        myMats[1] = mats[color];
        head.GetComponent<Renderer>().materials = myMats;
    }
    


}
