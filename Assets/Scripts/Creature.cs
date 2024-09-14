﻿using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Creature : MonoBehaviour
{
    private static readonly Quaternion ReversedRotation = Quaternion.Euler(0, 180, 0);

    [SerializeField, Min(0f)] private float _moveSpeed = 1f;
    [SerializeField, Min(0f)] private float _jumpSpeed = 1f;
    [SerializeField, Min(0f)] private float _terrainCheckOffset = 1f;
    [SerializeField] private LayerMask _terrainLayers;

    public event Action<float> HealthChanged;
    public event Action Dead;

    protected float MoveDirection;
    protected bool IsJumping;
    protected Health HealthComponent;

    private Rigidbody2D _rigidbody;
    private Collider2D _collider;

    public float Health => HealthComponent.HealthCurrent;

    public float HealthMax => HealthComponent.HealthMax;

    public bool IsMoving => MoveDirection != 0;

    public float VerticalVelocity => _rigidbody.velocity.y;

    protected virtual void Awake()
    {
        HealthComponent = GetComponent<Health>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    protected virtual void OnEnable()
    {
        HealthComponent.HealthChanged += OnHealthChanged;
        HealthComponent.Dead += OnDead;
    }

    protected virtual void OnDisable()
    {
        HealthComponent.HealthChanged -= OnHealthChanged;
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

    public void MoveTowards(Vector3 position)
    {
        if (position.x > transform.position.x)
            MoveRight();
        else
            MoveLeft();
    }

    public void MoveRight()
    {
        MoveDirection = MovementDirection.Right;
    }

    public void MoveLeft()
    {
        MoveDirection = MovementDirection.Left;
    }

    public void StopMovement()
    {
        MoveDirection = MovementDirection.NoMovement;
    }

    public bool IsOnGround()
    {
        return Physics2D.Raycast(transform.position, Vector3.down, _terrainCheckOffset, _terrainLayers).collider != null;
    }

    public void Damage(float damageAmount)
    {
        HealthComponent.Damage(damageAmount);
    }

    public void OnDisappeared()
    {
        Destroy(gameObject);
    }

    private void OnHealthChanged(float changeAmount)
    {
        HealthChanged?.Invoke(changeAmount);
    }

    private void OnDead()
    {
        _collider.enabled = false;
        _rigidbody.gravityScale = 0;
        _rigidbody.velocity = Vector2.zero;
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
}
