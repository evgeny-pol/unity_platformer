using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField, Min(1f)] private float _health = 1f;
    [SerializeField, Min(1f)] private float _healthMax = 1f;

    public event Action<float> HealthChanged;
    public event Action Dead;

    public float HealthCurrent => _health;

    public float HealthMax => _healthMax;

    public bool IsHurt => _health < _healthMax;

    public bool IsDead => _health == 0;

    private void OnValidate()
    {
        _health = MathF.Min(_health, _healthMax);
    }

    public void Damage(float damageAmount)
    {
        if (damageAmount <= 0 || IsDead)
            return;

        _health = Mathf.Max(_health - damageAmount, 0);

        if (_health == 0)
            Dead?.Invoke();
        else
            HealthChanged?.Invoke(-damageAmount);
    }

    public void Heal(float healAmount)
    {
        if (IsDead)
            return;

        healAmount = Mathf.Min(healAmount, _healthMax - _health);

        if (healAmount <= 0)
            return;

        _health += healAmount;
        HealthChanged?.Invoke(healAmount);
    }

    public void Kill()
    {
        Damage(_health);
    }
}
