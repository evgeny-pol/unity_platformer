using UnityEngine;

public class Mob : Creature
{
    [SerializeField, Min(0)] private int _touchDamage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DoDamage(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        DoDamage(collision);
    }

    private void DoDamage(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Hero hero))
            hero.Damage(_touchDamage);
    }
}
