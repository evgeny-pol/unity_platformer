using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class Hero : Creature
{
    private Inventory _inventory;

    public Inventory Inventory => _inventory;

    protected override void Awake()
    {
        base.Awake();
        _inventory = GetComponent<Inventory>();
    }

    protected override void Update()
    {
        MoveDirection = Input.GetAxisRaw(InputConstants.HorizontalAxis);

        if (Input.GetButtonDown(InputConstants.JumpAxis))
            IsJumping = true;

        base.Update();
    }
}
