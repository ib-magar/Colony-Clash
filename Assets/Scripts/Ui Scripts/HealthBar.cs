using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
[RequireComponent(typeof(LivingEntity))]
public class HealthBar : MonoBehaviour
{

    public Slider _healthBar;
    private float _maxhealth;
    public float _healthbarUpdateTime = .25f;

    private LivingEntity _enemy;
    private void Start()
    {
        _enemy=GetComponent<LivingEntity>();
        _maxhealth = _enemy.startingHealth;

        _healthBar.maxValue= _maxhealth;
        _healthBar.minValue = 0f;
        _healthBar.value = _maxhealth;

        _enemy.DamageEvent += UpdateHealthBar;
    }
    public void UpdateHealthBar()
    {
        _healthBar.DOValue(_enemy.ReturnHealth(),_healthbarUpdateTime);
    }

}
