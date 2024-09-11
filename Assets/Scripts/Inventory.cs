using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event Action<ItemType, int> ItemCountChanged;

    private readonly Dictionary<ItemType, int> _items = new();

    public int GetCount(ItemType itemType)
    {
        _items.TryGetValue(itemType, out int count);
        return count;
    }

    public void Add(CollectableItem item)
    {
        ItemType itemType = item.Type;
        _items.TryGetValue(itemType, out int count);
        count += item.Count;
        _items[itemType] = count;
        ItemCountChanged?.Invoke(itemType, count);
    }
}
