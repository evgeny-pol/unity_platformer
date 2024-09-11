using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Killbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Health healthComponent))
            healthComponent.Kill();
        else
            Destroy(collision.gameObject);
    }
}
