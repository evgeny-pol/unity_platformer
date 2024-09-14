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

    public void Remove(ItemType itemType, int removeCount)
    {
        if (removeCount <= 0)
            return;

        if (_items.TryGetValue(itemType, out int count))
        {
            count = Mathf.Max(count - removeCount, 0);

            if (count == 0)
                _items.Remove(itemType);
            else
                _items[itemType] = count;

            ItemCountChanged?.Invoke(itemType, count);
        }
    }
}
