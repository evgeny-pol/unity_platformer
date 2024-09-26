using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CreatureAnimator : MonoBehaviour
{
    [SerializeField] private Creature _creature;
    [SerializeField, Min(0f)] private float _animatorUpdateInterval = 0.1f;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _creature.HealthChanged += OnHealthChanged;
        _creature.Dead += OnDead;
        StartCoroutine(UpdateAnimator());
    }

    private void OnDisable()
    {
        _creature.HealthChanged -= OnHealthChanged;
        _creature.Dead -= OnDead;
    }

    protected virtual void OnHealthChanged(float changeAmount)
    {
        if (changeAmount < 0)
            _animator.SetTrigger(AnimatorParams.IsHurt);
    }

    private void OnDead()
    {
        _animator.SetBool(AnimatorParams.IsDead, true);
    }

    private IEnumerator UpdateAnimator()
    {
        var delay = new WaitForSeconds(_animatorUpdateInterval);

        while (enabled)
        {
            _animator.SetBool(AnimatorParams.IsOnGround, _creature.IsOnGround());
            _animator.SetBool(AnimatorParams.IsRunning, _creature.IsMoving);
            _animator.SetFloat(AnimatorParams.VerticalVelocity, _creature.VerticalVelocity);
            yield return delay;
        }
    }
}
