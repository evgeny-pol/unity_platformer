using UnityEngine;

public abstract class HealthIndicator : MonoBehaviour
{
    [SerializeField] private Health _health;

    protected Health HealthComponent => _health;

    protected virtual void OnEnable()
    {
        _health.CurrentChanged += OnCurrentChanged;
    }

    private void OnDisable()
    {
        _health.CurrentChanged -= OnCurrentChanged;
    }

    protected abstract void OnCurrentChanged(float changeAmount);
}
