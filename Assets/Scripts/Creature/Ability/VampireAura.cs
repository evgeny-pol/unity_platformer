using System.Collections;
using UnityEngine;

public class VampireAura : Ability
{
    [SerializeField] private Health _owner;
    [SerializeField, Min(0f)] private float _healthPerSecond = 1f;
    [SerializeField, Min(0f)] private float _radius = 1f;
    [SerializeField] private LayerMask _affectedCreatureLayers;
    [SerializeField] private SpriteRenderer _auraSpriteRenderer;

    private Coroutine _activeCoroutine;

    private void Start()
    {
        float auraDiameter = _radius * 2;
        _auraSpriteRenderer.transform.localScale = new Vector3(auraDiameter, auraDiameter, 1);
        _auraSpriteRenderer.enabled = false;
    }

    protected override void Activate()
    {
        _activeCoroutine = StartCoroutine(Work());
    }

    public override void Disable()
    {
        _auraSpriteRenderer.enabled = false;

        if (_activeCoroutine != null)
        {
            StopCoroutine(_activeCoroutine);
            _activeCoroutine = null;
        }

        base.Disable();
    }

    private IEnumerator Work()
    {
        _auraSpriteRenderer.enabled = true;

        while (WorkDuration.IsEnded == false && _owner.IsDead == false)
        {
            float drainedPerCreature = _healthPerSecond * Time.deltaTime;
            float drainedTotal = 0;
            Collider2D[] creatureColliders = Physics2D.OverlapCircleAll(transform.position, _radius, _affectedCreatureLayers);

            foreach (Collider2D creatureCollider in creatureColliders)
                if (creatureCollider.TryGetComponent(out Health health))
                    drainedTotal += health.TakeDamage(drainedPerCreature);

            _owner.TakeHealing(drainedTotal);
            yield return null;
        }

        _auraSpriteRenderer.enabled = false;
        StartCooldown();
        _activeCoroutine = null;
    }
}
