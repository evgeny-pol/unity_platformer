using System;
using UnityEngine;

public class Hero : Creature
{
    [SerializeField, Min(0)] private int _gemsCount;

    public event Action GemsCountChanged;

    public int GemsCount => _gemsCount;

    protected override void Update()
    {
        MoveDirection = Input.GetAxisRaw(InputConstants.HorizontalAxis);

        if (Input.GetKeyDown(KeyCode.Space))
            IsJumping = true;

        base.Update();
    }

    public void CollectGem(int count)
    {
        _gemsCount += count;
        GemsCountChanged?.Invoke();
    }
}
