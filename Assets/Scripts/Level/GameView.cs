using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField] private Hero _hero;
    [SerializeField] private ItemCountText[] _itemCountTexts;

    private void Start()
    {
        _hero.ItemCountChanged += OnItemCountChanged;
        UpdateItemCounts();
    }

    private void OnDisable()
    {
        _hero.ItemCountChanged -= OnItemCountChanged;
    }

    private void OnItemCountChanged(ItemType itemType, int changeAmount)
    {
        ItemCountText itemCountText = _itemCountTexts.FirstOrDefault(text => text.ItemType == itemType);

        if (itemCountText != null)
            itemCountText.TextField.text = _hero.GetItemCount(itemType).ToString();
    }

    private void UpdateItemCounts()
    {
        foreach (ItemCountText itemCountText in _itemCountTexts)
            itemCountText.TextField.text = _hero.GetItemCount(itemCountText.ItemType).ToString();
    }

    [Serializable]
    public class ItemCountText
    {
        [SerializeField] public ItemType ItemType;
        [SerializeField] public Text TextField;
    }
}
