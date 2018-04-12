using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleDamage : MobStats {

    private int timesGotHit;
    private ParticleSystem[] damageParticles;
    float howMuchToJiggle;

    // Use this for initialization
    void Start () {
        damageParticles = GetComponentsInChildren<ParticleSystem>();
        timesGotHit = 0;
        howMuchToJiggle = 2.5f / maxHealth;
        foreach (ParticleSystem damageParticle in damageParticles)
        {
            var emission = damageParticle.emission;
            emission.enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 randomVector = new Vector2(Random.Range(-(howMuchToJiggle * timesGotHit), (howMuchToJiggle * timesGotHit)), Random.Range(-(howMuchToJiggle * timesGotHit), (howMuchToJiggle * timesGotHit)));
        transform.Translate(randomVector * Time.deltaTime * 5, Space.World); // jiggle about randomly because it's shitty if the mob stands still
        // transform.position = Vector2.Lerp(transform.position, (new Vector2(transform.position.x, transform.position.y) + randomVector), 5 * Time.deltaTime); // good effect to turn on when mob is about to die
    }

    public override void TakeDamage(float damage, int color) //Om mob:en blir träffad av en kula som korresponderar med mob:ens färg så tar den skada.
    {
        timesGotHit++;
        print(timesGotHit);
        if ((color == this.color) && (timesGotHit <= damageParticles.Length - 1))
        {
            var emission = damageParticles[timesGotHit].emission;
            emission.enabled = true;
        }
    }
}
