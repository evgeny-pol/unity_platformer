using Assets.Scripts.Utils;
using System;
using System.Collections;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] private TimeInterval _cooldown;
    [SerializeField] private TimeInterval _workDuration;

    private WaitForSeconds _cooldownDelay;
    private Coroutine _cooldownCoroutine;

    public event Action StateChanged;

    public AbilityState State { get; private set; } = AbilityState.Ready;

    public float CooldownRemaining => _cooldown.Remaining;

    public float CooldownDuration => _cooldown.Duration;

    public float ActiveRemaining => _workDuration.Remaining;

    public float ActiveDuration => _workDuration.Duration;

    protected TimeInterval WorkDuration => _workDuration;

    private void Awake()
    {
        _cooldownDelay = new WaitForSeconds(_cooldown.Duration);
    }

    public void TryActivate()
    {
        if (State == AbilityState.Ready)
        {
            _workDuration.Start();
            SetState(AbilityState.Active);
            Activate();
        }
    }

    public virtual void Disable()
    {
        _cooldown.End();
        _workDuration.End();

        if (_cooldownCoroutine != null)
            StopCoroutine(_cooldownCoroutine);

        SetState(AbilityState.Disabled);
    }

    protected abstract void Activate();

    protected void SetState(AbilityState state)
    {
        State = state;
        StateChanged?.Invoke();
    }

    protected void StartCooldown()
    {
        _cooldown.Start();
        SetState(AbilityState.OnCooldown);
        _cooldownCoroutine = StartCoroutine(WaitCooldown());
    }

    private IEnumerator WaitCooldown()
    {
        yield return _cooldownDelay;
        SetState(AbilityState.Ready);
    }
}
