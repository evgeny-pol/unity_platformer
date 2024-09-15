using UnityEngine;

public class TouchAttacker : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _touchDamage = 1f;

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
        if (collision.gameObject.TryGetComponent(out Creature creature))
            creature.TakeDamage(_touchDamage * Time.fixedDeltaTime);
    }
}
