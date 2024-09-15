using System;
using System.Collections;
using UnityEngine;

public class Mob : Creature
{
    [SerializeField] private Vision _vision;
    [SerializeField] private SpriteRenderer _alertIcon;
    [SerializeField, Min(0f)] private float _alertIconShowTime = 1f;
    [SerializeField] private FollowAI _followAI;
    [SerializeField] private PatrolAI _patrolAI;

    public event Action HeroFound;
    public event Action HeroLost;

    private WaitForSeconds _alertIconDelay;
    private Coroutine _alertIconCoroutine;

    protected override void Awake()
    {
        base.Awake();
        _alertIconDelay = new WaitForSeconds(_alertIconShowTime);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _vision.ObjectFound += OnHeroFound;
        _vision.ObjectLost += OnHeroLost;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _vision.ObjectFound -= OnHeroFound;
        _vision.ObjectLost -= OnHeroLost;
    }

    private void OnHeroFound(Collider2D obj)
    {
        StartShowAlertIcon();
        _patrolAI.StopPatrol();
        _followAI.StartFollowing(obj.transform);
        HeroFound?.Invoke();
    }

    private void OnHeroLost(Collider2D obj)
    {
        _followAI.StopFollowing();
        _patrolAI.StartPatrol();
        HeroLost?.Invoke();
    }

    private void StartShowAlertIcon()
    {
        if (_alertIconCoroutine != null)
            StopCoroutine(_alertIconCoroutine);

        _alertIconCoroutine = StartCoroutine(ShowAlertIcon());
    }

    private IEnumerator ShowAlertIcon()
    {
        _alertIcon.enabled = true;
        yield return _alertIconDelay;
        _alertIcon.enabled = false;
    }
}
