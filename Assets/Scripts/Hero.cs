using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class Hero : Creature
{
    [SerializeField] private ItemType _healingItemType = ItemType.Cherry;
    [SerializeField, Min(0f)] private float _healAmount = 1f;

    private Inventory _inventory;

    public Inventory Inventory => _inventory;

    protected override void Awake()
    {
        base.Awake();
        _inventory = GetComponent<Inventory>();
    }

    protected override void Update()
    {
        MoveDirection = Input.GetAxisRaw(InputAxis.Horizontal);

        if (Input.GetButtonDown(InputAxis.Jump))
            IsJumping = true;

        if (Input.GetButtonDown(InputAxis.Heal))
            Heal();

        base.Update();
    }

    private void Heal()
    {
        if (HealthComponent.IsHurt && Inventory.GetCount(_healingItemType) > 0)
        {
            HealthComponent.Heal(_healAmount);
            Inventory.Remove(_healingItemType, 1);
        }
    }
}
