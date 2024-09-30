using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ResourceBar))]
public class AbilityBar : MonoBehaviour
{
    [SerializeField] private Ability _ability;

    private Coroutine _updateFillCoroutine;
    private ResourceBar _resourceBar;

    private float CooldownCoef => _ability.CooldownDuration == 0 ? 1 : 1 - _ability.CooldownRemaining / _ability.CooldownDuration;

    private float ActiveCoef => _ability.ActiveDuration == 0 ? 0 : _ability.ActiveRemaining / _ability.ActiveDuration;

    private void Awake()
    {
        _resourceBar = GetComponent<ResourceBar>();
    }

    private void OnEnable()
    {
        _ability.StateChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        _ability.StateChanged -= OnStateChanged;
    }

    private void OnStateChanged()
    {
        _updateFillCoroutine ??= StartCoroutine(UpdateFillCoef());
    }

    private IEnumerator UpdateFillCoef()
    {
        bool isWorking = true;

        while (isWorking)
        {
            switch (_ability.State)
            {
                case AbilityState.OnCooldown:
                    _resourceBar.SetFillCoef(CooldownCoef);
                    break;

                case AbilityState.Ready:
                    _resourceBar.SetFillCoef(1);
                    isWorking = false;
                    break;

                case AbilityState.Active:
                    _resourceBar.SetFillCoef(ActiveCoef);
                    break;

                default:
                    _resourceBar.SetFillCoef(0);
                    isWorking = false;
                    break;
            }

            yield return null;
        }

        _updateFillCoroutine = null;
    }
}
