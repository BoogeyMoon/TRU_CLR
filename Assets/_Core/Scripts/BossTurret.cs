using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTurret : Turret
{
    [SerializeField]
    Material[] mats;
    Material[] myMats;

    protected override void Start()
    {
        base.Start();
        myMats = head.GetComponent<Renderer>().materials;

    }
    public override void TakeDamage(float damage, int color)
    {
        base.TakeDamage(damage, color);
        if (this.color == color)
        {
            this.color = (this.color + 1) % 3;
        }
        ChangeMaterial(this.color);
    }
    void ChangeMaterial(int color)
    {
        print("Color number: " + color);
        myMats[1] = mats[color];
    }


}
