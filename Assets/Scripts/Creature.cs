using System;
using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class Creature : MonoBehaviour
{
    private static readonly Quaternion ReversedRotation = Quaternion.Euler(0, 180, 0);

    [SerializeField, Min(0f)] private float _moveSpeed = 1f;
    [SerializeField, Min(0f)] private float _jumpSpeed = 1f;
    [SerializeField, Min(0f)] private float _invulnerabilityAfterHurt;
    [SerializeField, Min(0f)] private float _terrainCheckOffset = 1f;
    [SerializeField, Min(0f)] private float _animatorUpdateInterval = 0.1f;
    [SerializeField] private LayerMask _terrainLayers;

    public event Action Hurt;
    public event Action Dead;

    protected float MoveDirection;
    protected bool IsJumping;
    protected HealthComponent HealthComponent;

    private WaitForSeconds _damageInvulnerabilityDelay;
    private Coroutine _damageInvulnerabilityCoroutine;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private Animator _animator;

    public int Health => HealthComponent.Health;

    protected virtual void Awake()
    {
        HealthComponent = GetComponent<HealthComponent>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
        _damageInvulnerabilityDelay = new WaitForSeconds(_invulnerabilityAfterHurt);
    }

    private void OnEnable()
    {
        HealthComponent.Hurt += OnHurt;
        HealthComponent.Dead += OnDead;
        StartCoroutine(UpdateAnimator());
    }

    private void OnDisable()
    {
        HealthComponent.Hurt -= OnHurt;
        HealthComponent.Dead -= OnDead;
    }

    private void FixedUpdate()
    {
        if (HealthComponent.IsDead)
            return;

        Vector2 newVelocity = _rigidbody.velocity;
        newVelocity.x = _moveSpeed * MoveDirection;

        if (IsJumping && IsOnGround())
            newVelocity.y = _jumpSpeed;

        IsJumping = false;
        _rigidbody.velocity = newVelocity;
    }

    protected virtual void Update()
    {
        UpdateRotation();
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 from = transform.position;
        Vector3 to = from;
        to.y -= _terrainCheckOffset;
        Gizmos.DrawLine(from, to);
    }

    public void MoveRight()
    {
        MoveDirection = MovementConstants.RightMovementDirection;
    }

    public void MoveLeft()
    {
        MoveDirection = MovementConstants.LeftMovementDirection;
    }

    public void Damage(int damageAmount)
    {
        HealthComponent.TryChangeHealth(-damageAmount);
    }

    public void OnDisappeared()
    {
        Destroy(gameObject);
    }

    private void OnHurt()
    {
        _animator.SetTrigger(AnimatorParams.IsHurt);
        StartDamageInvulnerability();
        Hurt?.Invoke();
    }

    private void OnDead()
    {
        _collider.enabled = false;
        _rigidbody.gravityScale = 0;
        _rigidbody.velocity = Vector2.zero;
        _animator.SetBool(AnimatorParams.IsDead, true);
        Dead?.Invoke();
    }

    private void UpdateRotation()
    {
        if (MoveDirection > 0)
        {
            transform.rotation = Quaternion.identity;
        }
        else if (MoveDirection < 0)
        {
            transform.rotation = ReversedRotation;
        }
    }

    private bool IsOnGround()
    {
        return Physics2D.Raycast(transform.position, Vector3.down, _terrainCheckOffset, _terrainLayers).collider != null;
    }

    private void StartDamageInvulnerability()
    {
        if (_invulnerabilityAfterHurt == 0)
            return;

        HealthComponent.IsInvulnerable = true;

        if (_damageInvulnerabilityCoroutine != null)
            StopCoroutine(_damageInvulnerabilityCoroutine);

        _damageInvulnerabilityCoroutine = StartCoroutine(WaitDamageInvulnerability());
    }

    private IEnumerator WaitDamageInvulnerability()
    {
        yield return _damageInvulnerabilityDelay;
        HealthComponent.IsInvulnerable = false;
    }

    private IEnumerator UpdateAnimator()
    {
        var delay = new WaitForSeconds(_animatorUpdateInterval);

        while (enabled)
        {
            _animator.SetBool(AnimatorParams.IsOnGround, IsOnGround());
            _animator.SetBool(AnimatorParams.IsRunning, MoveDirection != 0);
            _animator.SetFloat(AnimatorParams.VerticalVelocity, _rigidbody.velocity.y);
            yield return delay;
        }
    }
}
