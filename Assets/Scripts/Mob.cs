using Assets.Scripts.Utils;
using UnityEngine;

public class Mob : Creature
{
    [SerializeField, Min(0)] private int _touchDamage = 1;
    [SerializeField] private Cooldown _attackCooldown;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Attack(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Attack(collision);
    }

    private void Attack(Collision2D collision)
    {
        if (_attackCooldown.IsReady && collision.gameObject.TryGetComponent(out Hero hero))
        {
            hero.Damage(_touchDamage);
            _attackCooldown.Reset();
        }
    }
}
