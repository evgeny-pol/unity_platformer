using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class ItemCollector : MonoBehaviour
{
    private Inventory _inventory;

    private void Awake()
    {
        _inventory = GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out CollectableItem item))
        {
            _inventory.Add(item);
            item.Collect();
        }
    }
}
