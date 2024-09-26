using UnityEngine;

[RequireComponent(typeof(ResourceBar))]
public class HealthBar : HealthIndicator
{
    private ResourceBar _resourceBar;

    private float FillCoef => HealthComponent.Current / HealthComponent.Max;

    private void Awake()
    {
        _resourceBar = GetComponent<ResourceBar>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _resourceBar.SetInitialFillCoef(FillCoef);
    }

    protected override void OnCurrentChanged(float changeAmount)
    {
        _resourceBar.SetFillCoef(FillCoef);
    }
}
