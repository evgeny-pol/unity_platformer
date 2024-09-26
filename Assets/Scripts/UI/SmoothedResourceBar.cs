using System.Collections;
using UnityEngine;

public class SmoothedResourceBar : ResourceBar
{
    [SerializeField] private AnimationCurve _fillSpeedCurve;

    private float _targetFillCoef;
    private Coroutine _updateFillCoroutine;

    private void Awake()
    {
        _targetFillCoef = FillCoef;
    }

    private void OnEnable()
    {
        if (FillCoef != _targetFillCoef)
            _updateFillCoroutine ??= StartCoroutine(UpdateFill());
    }

    private void OnDisable()
    {
        if (_updateFillCoroutine != null)
        {
            StopCoroutine(_updateFillCoroutine);
            _updateFillCoroutine = null;
        }
    }

    public override void SetInitialFillCoef(float fillCoef)
    {
        _targetFillCoef = fillCoef;
        base.SetInitialFillCoef(fillCoef);
    }

    public override void SetFillCoef(float fillCoef)
    {
        _targetFillCoef = fillCoef;
        _updateFillCoroutine ??= StartCoroutine(UpdateFill());
    }

    private IEnumerator UpdateFill()
    {
        while (FillCoef != _targetFillCoef)
        {
            float fillSpeed = _fillSpeedCurve.Evaluate(Mathf.Abs(_targetFillCoef - FillCoef));
            float newFill = Mathf.MoveTowards(FillCoef, _targetFillCoef, fillSpeed * Time.deltaTime);
            base.SetFillCoef(newFill);
            yield return null;
        }

        _updateFillCoroutine = null;
    }
}
