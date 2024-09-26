using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField, Min(1f)] private float _current = 1f;
    [SerializeField, Min(1f)] private float _max = 1f;

    public event Action<float> CurrentChanged;
    public event Action Dead;

    public float Current => _current;

    public float Max => _max;

    public bool IsHurt => _current < _max;

    public bool IsDead => _current == 0;

    private void OnValidate()
    {
        _current = MathF.Min(_current, _max);
    }

    public void TakeDamage(float damageAmount)
    {
        if (damageAmount <= 0 || IsDead)
            return;

        _current = Mathf.Max(_current - damageAmount, 0);
        CurrentChanged?.Invoke(-damageAmount);

        if (_current == 0)
            Dead?.Invoke();
    }

    public void TakeHealing(float healAmount)
    {
        if (IsDead)
            return;

        healAmount = Mathf.Min(healAmount, _max - _current);

        if (healAmount <= 0)
            return;

        _current += healAmount;
        CurrentChanged?.Invoke(healAmount);
    }

    public void Kill()
    {
        TakeDamage(_current);
    }
}
