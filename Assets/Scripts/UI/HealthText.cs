using TMPro;
using UnityEngine;

public class HealthText : HealthIndicator
{
    [SerializeField] private TextMeshProUGUI _currentText;
    [SerializeField] private TextMeshProUGUI _maxText;

    protected override void OnEnable()
    {
        base.OnEnable();
        SetText(_currentText, HealthComponent.Current);
        SetText(_maxText, HealthComponent.Max);
    }

    protected override void OnCurrentChanged(float changeAmount)
    {
        SetText(_currentText, HealthComponent.Current);
    }

    private void SetText(TextMeshProUGUI textMesh, float healthValue)
    {
        textMesh.text = Mathf.CeilToInt(healthValue).ToString();
    }
}
