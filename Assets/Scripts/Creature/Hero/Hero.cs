using System;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class Hero : Creature
{
    private Inventory _inventory;

    public event Action<ItemType, int> ItemCountChanged;

    protected override void Awake()
    {
        base.Awake();
        _inventory = GetComponent<Inventory>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _inventory.ItemCountChanged += OnItemCountChanged;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _inventory.ItemCountChanged -= OnItemCountChanged;
    }

    protected override void Update()
    {
        MoveDirection = Input.GetAxisRaw(InputAxis.Horizontal);

        if (Input.GetButtonDown(InputAxis.Jump))
            IsJumping = true;

        base.Update();
    }

    public int GetItemCount(ItemType itemType)
    {
        return _inventory.GetCount(itemType);
    }

    public void RemoveItem(ItemType itemType, int removeCount)
    {
        _inventory.Remove(itemType, removeCount);
    }

    private void OnItemCountChanged(ItemType itemType, int changeAmount) => ItemCountChanged?.Invoke(itemType, changeAmount);
}
