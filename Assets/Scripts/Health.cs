using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField, Min(1)] private int _health = 1;
    [SerializeField, Min(1)] private int _healthMax = 1;

    public event Action<int> HealthChanged;
    public event Action Dead;

    public int HealthCurrent => _health;

    public int HealthMax => _healthMax;

    public bool IsInvulnerable { get; set; }

    public bool IsDead => _health == 0;

    private void Awake()
    {
        _health = Mathf.Min(_health, _healthMax);
    }

    public void Damage(int damageAmount)
    {
        if (damageAmount <= 0 || IsInvulnerable || IsDead)
            return;

        _health = Mathf.Max(_health - damageAmount, 0);

        if (_health == 0)
            Dead?.Invoke();
        else
            HealthChanged?.Invoke(-damageAmount);
    }

    public void Kill()
    {
        IsInvulnerable = false;
        Damage(_health);
    }
}
