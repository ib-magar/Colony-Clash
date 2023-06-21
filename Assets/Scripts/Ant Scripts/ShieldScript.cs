using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : LivingEntity
{

    public override void Start()
    {
        base.Start();
        DiedEvent += Death;
        DamageEvent += DamageEffect;
    }

    void Death()
    {
        //die effect
    }
    [Header("Damage effects")]
    public ParticleSystem _damageParticleSystem;
    void DamageEffect()
    {
        //damage effect
        _damageParticleSystem.Play();
    }
}
