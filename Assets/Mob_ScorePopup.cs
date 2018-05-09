using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Mob_ScorePopup : MonoBehaviour, IPoolable {

    public bool Active { get; set; }
    MobStats mobstats;
    PoolManager _textPool;
    float _timer;
    Animator anim;
    ParticleSystem ps;


    void Start () {

        anim = GetComponentInChildren<Animator>();
        ps = GetComponentInChildren<ParticleSystem>();
        _textPool = GameObject.FindGameObjectWithTag("PoolManagers").transform.GetChild(3).GetComponent<PoolManager>();
    }

    void Update ()
    {
        if (Active)
        {
            _timer += Time.deltaTime;
            if (_timer >=  4)
            {
                _textPool.DestroyPool(transform);
            }
        }
    }

    public void PoolStart(MobStats mob)
    {
        _timer = 0;
        mobstats = mob;
        GetComponentInChildren<Text>().text = mobstats.ScoreValue.ToString();
        ps.Clear(true);
        ps.Play(true);
        anim.SetTrigger("SpawnTrigger");
    }

}
