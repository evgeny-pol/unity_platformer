using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField, Min(1)] private int _health = 1;
    [SerializeField, Min(1)] private int _healthMax = 1;

    public event Action Healed;
    public event Action Hurt;
    public event Action Dead;

    public int Health => _health;

    public bool IsInvulnerable { get; set; }

    public bool IsDead => _health == 0;

    private void Awake()
    {
        _health = Mathf.Min(_health, _healthMax);
    }

    public bool TryChangeHealth(int changeAmount)
    {
        if (changeAmount == 0
            || IsInvulnerable && changeAmount < 0)
            return false;

        var newHealth = Mathf.Clamp(_health + changeAmount, 0, _healthMax);

        if (newHealth == _health)
            return false;

        _health = newHealth;

        if (newHealth == 0)
            Dead?.Invoke();
        else if (changeAmount > 0)
            Healed?.Invoke();
        else if (changeAmount < 0)
            Hurt?.Invoke();

        return true;
    }

    public void Kill()
    {
        IsInvulnerable = false;
        TryChangeHealth(-_health);
    }
}
