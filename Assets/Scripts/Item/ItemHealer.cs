using UnityEngine;

[RequireComponent(typeof(Hero))]
public class ItemHealer : MonoBehaviour
{
    [SerializeField] private ItemType _healingItemType = ItemType.Cherry;
    [SerializeField, Min(0f)] private float _healAmount = 1f;

    private Hero _hero;

    private void Awake()
    {
        _hero = GetComponent<Hero>();
    }

    private void Start()
    {
        _hero.ItemCountChanged += OnItemCountChanged;
    }

    private void OnDisable()
    {
        _hero.ItemCountChanged -= OnItemCountChanged;
    }

    private void OnItemCountChanged(ItemType itemType, int changeAmount)
    {
        if (itemType == _healingItemType && changeAmount > 0)
        {
            _hero.TakeHealing(_healAmount * changeAmount);
            _hero.RemoveItem(_healingItemType, changeAmount);
        }
    }
}
