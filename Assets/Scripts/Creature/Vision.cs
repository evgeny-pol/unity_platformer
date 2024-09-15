using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Vision : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayers;

    public event Action<Collider2D> ObjectFound;
    public event Action<Collider2D> ObjectLost;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsTargetObject(collision))
            ObjectFound?.Invoke(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsTargetObject(collision))
            ObjectLost?.Invoke(collision);
    }

    private bool IsTargetObject(Collider2D collider)
    {
        return (1 << collider.gameObject.layer & _targetLayers) != 0;
    }
}
