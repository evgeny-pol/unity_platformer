using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Creature))]
[RequireComponent(typeof(Animator))]
public class CreatureAnimator : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _animatorUpdateInterval = 0.1f;

    private Creature _creature;
    private Animator _animator;

    private void Awake()
    {
        _creature = GetComponent<Creature>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _creature.HealthChanged += OnHealthChanged;
        _creature.Dead += OnDead;
        StartCoroutine(UpdateAnimator());
    }

    private void OnDead()
    {
        _animator.SetBool(AnimatorParams.IsDead, true);
    }

    private void OnHealthChanged(int changeAmount)
    {
        if (changeAmount < 0)
            _animator.SetTrigger(AnimatorParams.IsHurt);
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
