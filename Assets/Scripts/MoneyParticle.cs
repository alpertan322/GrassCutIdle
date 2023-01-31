using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyParticle : MonoBehaviour
{
    private HitParticle _hitParticle;
    private float rate;
    
    private void Awake()
    {
        _hitParticle = FindObjectOfType<HitParticle>();
        
    }

    private void Start()
    {
        StartCoroutine(ShowParticleLoop());
    }

    IEnumerator ShowParticleLoop()
    {
        _hitParticle.ShowHitParticle(transform , 1);
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(ShowParticleLoop());
    }
}
