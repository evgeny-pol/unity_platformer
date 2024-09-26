using UnityEngine;

public class ResourceBar : MonoBehaviour
{
    [SerializeField] private RectTransform _fillRect;

    public float FillCoef { get; private set; } = 1;

    public virtual void SetInitialFillCoef(float fillCoef) => SetFillCoefAndUpdate(fillCoef);

    public virtual void SetFillCoef(float fillCoef) => SetFillCoefAndUpdate(fillCoef);

    private void SetFillCoefAndUpdate(float fillCoef)
    {
        FillCoef = fillCoef;
        Vector2 anchorMax = _fillRect.anchorMax;
        anchorMax.x = fillCoef;
        _fillRect.anchorMax = anchorMax;
        _fillRect.gameObject.SetActive(fillCoef != 0);
    }
}
