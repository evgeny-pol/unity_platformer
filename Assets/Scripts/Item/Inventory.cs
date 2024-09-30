using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private readonly Dictionary<ItemType, int> _items = new();

    public event Action<ItemType, int> ItemCountChanged;

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
        ItemCountChanged?.Invoke(itemType, item.Count);
    }

    public void Remove(ItemType itemType, int removeCount)
    {
        if (removeCount <= 0
            || _items.TryGetValue(itemType, out int currentCount) == false)
            return;

        removeCount = Mathf.Min(removeCount, currentCount);

        if (removeCount == currentCount)
            _items.Remove(itemType);
        else
            _items[itemType] = currentCount - removeCount;

        ItemCountChanged?.Invoke(itemType, -removeCount);
    }
}
