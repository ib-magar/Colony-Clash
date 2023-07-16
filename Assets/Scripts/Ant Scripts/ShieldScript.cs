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
    public AudioClip _dieclip;
    void Death()
    {
        //die effect
        SoundManager.Instance.PlaySoundEffect(_dieclip);
    }
    [Header("Damage effects")]
    public ParticleSystem _damageParticleSystem;
    void DamageEffect()
    {
        //damage effect
        _damageParticleSystem.Play();
    }
}
