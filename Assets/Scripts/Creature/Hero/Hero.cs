using System;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class Hero : Creature
{
    [SerializeField] private HeroAbility[] _abilities;

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

        foreach (HeroAbility ability in _abilities)
            if (Input.GetButtonDown(ability.HotkeyAxis))
                ability.Ability.TryActivate();

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

    protected override void OnDead()
    {
        foreach (HeroAbility ability in _abilities)
            ability.Ability.Disable();

        base.OnDead();
    }

    private void OnItemCountChanged(ItemType itemType, int changeAmount) => ItemCountChanged?.Invoke(itemType, changeAmount);

    [Serializable]
    public class HeroAbility
    {
        [InputAxis][SerializeField] private string _hotkeyAxis;
        [SerializeField] private Ability _ability;

        public string HotkeyAxis => _hotkeyAxis;

        public Ability Ability => _ability;
    }
}
